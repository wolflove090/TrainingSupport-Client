using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrainingView
{
    // タイムカウントインターフェース
    interface ITimer
    {
        TrainingTimerState Update(System.Action onComplete);
        string GetCurrentTime();
    }

    // カウントダウン
    class CountDown : ITimer
    {
        Queue<string> _Count = new Queue<string>();
        public string _Current{private set; get;}
        float _TimeCount;

        public CountDown()
        {
            this._Count = new Queue<string>();
            this._Count.Enqueue("3");
            this._Count.Enqueue("2");
            this._Count.Enqueue("1");
        }

        // 終了したらtrue
        TrainingTimerState ITimer.Update(System.Action onComplete)
        {
            // 最初
            if(string.IsNullOrEmpty(this._Current))
            {
                SoundManager.PlaySe(SoundManager.Se.CountDown);

                this._Current = this._Count.Dequeue();
                this._TimeCount = 0;
            }   

            this._TimeCount += Time.deltaTime;
            float time = 1f - this._TimeCount;
            if(time <= 0)
            {
                if(this._Count.Count <= 0)
                {
                    onComplete?.Invoke();
                    return new TrainingTimerState(TrainingViewData.COMPLETE, TrainingTimerState.TimerState.Complete);
                }

                SoundManager.PlaySe(SoundManager.Se.CountDown);

                this._Current = this._Count.Dequeue();
                this._TimeCount = 0;
            }

            return new TrainingTimerState(TrainingViewData.EMPTY, TrainingTimerState.TimerState.Exec);
        }

        string ITimer.GetCurrentTime()
        {
            return this._Current;
        }
    }

    // トレーニングタイマー
    class TrainingTimerManager : ITimer
    {
        const int COUNT_DOWN_MINITS = 3;

        Queue<ITrainingTimer> _TimerQueue = new Queue<ITrainingTimer>();
        public ITrainingTimer _CurrentTimer{private set; get;}
        public float _CurrentTime;
        float _TimeCount;
        int _CountDownMinits;

        public TrainingTimerManager(TrainingViewData[] trainings)
        {
            // 各タイマーオブジェクトを受け取ったトレーニング項目で作成
            this._TimerQueue = new Queue<ITrainingTimer>();
            for(int i = 0; i < trainings.Length; i++)
            {
                var training = trainings[i];

                // 初回以外はインターバルを作成
                if(i != 0)
                {   
                    this._TimerQueue.Enqueue(new IntervalTimer(training));
                }
                this._TimerQueue.Enqueue(new PlayTimer(training));
            }
        }

        // タイマーの更新
        TrainingTimerState ITimer.Update(System.Action onComplete)
        {
            // 実行タイマーを取得
            if(this._CurrentTimer == null)
            {
                SoundManager.PlaySe(SoundManager.Se.Prefix);

                // TODO 初期化処理をまとめたほうがいいかも
                 this._CurrentTimer = this._TimerQueue.Dequeue();
                 this._TimeCount = 0;
                 this._CountDownMinits = COUNT_DOWN_MINITS;
            }   

            // 経過時間を計測
            this._TimeCount += Time.deltaTime;
            this._CurrentTime = this._CurrentTimer.GetPlayTime() - this._TimeCount;

            // ラスト3秒でSEを再生
            if(this._CurrentTime <= this._CountDownMinits)
            {
                this._CountDownMinits--;
                SoundManager.PlaySe(SoundManager.Se.CountDown);
            }

            if(this._CurrentTime <= 0)
            {
                // コインの加算
                Coin.AddNum(this._CurrentTimer.GetTrainingViewData().CoinNum);

                // 次のトレーニングが無ければ終了
                if(this._TimerQueue.Count <= 0)
                {
                    onComplete?.Invoke();
                    return new TrainingTimerState(TrainingViewData.COMPLETE, TrainingTimerState.TimerState.Complete);
                }

                // タイマーのリセット
                SoundManager.PlaySe(SoundManager.Se.Prefix);
                this._CurrentTimer = this._TimerQueue.Dequeue();
                this._TimeCount = 0;
                 this._CountDownMinits = COUNT_DOWN_MINITS;
                return new TrainingTimerState(this._CurrentTimer.GetTrainingViewData(), TrainingTimerState.TimerState.Next);
            } 

            return new TrainingTimerState(this._CurrentTimer.GetTrainingViewData(), TrainingTimerState.TimerState.Exec);
        }

        string ITimer.GetCurrentTime()
        {
            return this._CurrentTime.ToString("F2");
        }
    }
}