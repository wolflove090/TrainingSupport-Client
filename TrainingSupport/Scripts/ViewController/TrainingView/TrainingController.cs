using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using UnityEngine;

namespace TrainingView
{
    public class TrainingController : ChildController<TrainingViewModel, TrainingLinker>, IView
    {
        List<TrainingViewData> _TrainingList = new List<TrainingViewData>();

        Queue<ITimer> _TimerQueue;
        ITimer _CurrentTimer;
        bool _IsPouse;

        string _ApplaudMessage;

        // Start
        protected override void _OnStart()
        {
            // ポーズボタン
            this._ViewModel.PouseTap.OnClick = () =>
            {
                this._IsPouse = !this._IsPouse;
                this._ViewModel.Pouse.SetActive(this._IsPouse);
                this._ViewModel.BackButton_Obj.SetActive(this._IsPouse);
                
                // 効果音
                var se = this._IsPouse ? SoundManager.Se.Pause : SoundManager.Se.Cancel;
                SoundManager.PlaySe(se);
            };

            // トップに戻る
            this._ViewModel.BackButton.OnClick = () => 
            {
                this._Linker.ViewChangeManager.ChangeView(ViewChangeManager.SceneType.Start);
            };
        }

        protected override void _OnUpdate()
        {
            // TODO 多分バグる
            if(this._TimerQueue.Count <= 0)
                return;

            if(this._CurrentTimer == null)
                return;

            if(this._IsPouse)
              return;

            var state = this._CurrentTimer.Update(() => 
            {
                // 次のタイマーを起動
                this._CurrentTimer = this._TimerQueue.Dequeue();
            });

            // 終了したらnullになる
            if(this._CurrentTimer == null)
            {
                this._Linker.ViewChangeManager.ChangeView(ViewChangeManager.SceneType.Complete, new CompleteViewActiveLinker()
                {
                    ApplaudMessage = this._ApplaudMessage,
                });
                return;
            }

            // 「NEXT表示」
            this._ViewModel.NextIcon.SetActive(false);
            if(state.Training.IsInterval)
                this._ViewModel.NextIcon.SetActive(true);

            // UI表示
            this._ViewModel.TimerLabel.text =  this._CurrentTimer.GetCurrentTime();
            this._ViewModel.TrainingName.text = state.Training.Name;
            this._ViewModel.TrainingIcon.sprite = state.Training.Icon;
        }

        void IView.Disable()
        {
            this.gameObject.SetActive(false);
        }

        void IView.Active(ViewActiveLinker linker)
        {
            var trainingLinker = linker as TrainingViewActiveLinker;
            if(trainingLinker == null)
                throw new System.Exception("リンカーを変換できなかった");

            // トレーニング項目の作成
            this._TrainingList = new List<TrainingViewData>();
            foreach(var info in trainingLinker.TrainingInfos)
            {
                this._TrainingList.Add(new TrainingViewData(info));
            }

            // 各タイマーの作成
            this._TimerQueue = new Queue<ITimer>();
            this._TimerQueue.Enqueue(new CountDown());
            this._TimerQueue.Enqueue(new TrainingTimerManager(this._TrainingList.ToArray()));
            this._TimerQueue.Enqueue(null); // 終了

            // 最初のタイマー
            this._CurrentTimer = this._TimerQueue.Dequeue();
            this._IsPouse = false;

            this.gameObject.SetActive(true);
            this._ViewModel.Pouse.SetActive(this._IsPouse);
            this._ViewModel.BackButton_Obj.SetActive(this._IsPouse);

            // ChatGPTから完了メッセージを取得
            ChatGPTSender.Send("トレーニングした後にかけるべき労いの言葉を簡潔に。女子マネージャーのように", (result) => {
                this._ApplaudMessage = result;
            });

            // BGMの再生
            SoundManager.PlayBgm(SoundManager.Bgm.Training);
        }
    }

    // アクティブ時の引数を受け取る
    public class TrainingViewActiveLinker : ViewActiveLinker
    {
        public TrainingInfo[] TrainingInfos;
    }
}


