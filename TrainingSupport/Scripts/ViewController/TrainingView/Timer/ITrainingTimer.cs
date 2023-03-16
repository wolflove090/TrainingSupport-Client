using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrainingView
{
    // タイマーのインターフェース
    interface ITrainingTimer
    {
        float GetPlayTime();
        TrainingViewData GetTrainingViewData();
    }

    // 実行タイマー
    class PlayTimer : ITrainingTimer
    {
        readonly TrainingViewData _Model;
        public PlayTimer(TrainingViewData model)
        {
            this._Model = model;
        }

        float ITrainingTimer.GetPlayTime()
        {
            return 20f;
        }

        TrainingViewData ITrainingTimer.GetTrainingViewData()
        {
            return this._Model;
        }
    }

    // インターバルタイマー
    class IntervalTimer : ITrainingTimer
    {
        readonly TrainingViewData _Next;
        readonly int _IntervalTimeSeconds;

        public IntervalTimer(TrainingViewData model)
        {
            this._Next = new TrainingViewData(model, true);
            this._IntervalTimeSeconds = model.IntervalTimeSeconds;
        }

        float ITrainingTimer.GetPlayTime()
        {
            return this._IntervalTimeSeconds;
        }

        TrainingViewData ITrainingTimer.GetTrainingViewData()
        {
            return this._Next;
        }
    }
}
