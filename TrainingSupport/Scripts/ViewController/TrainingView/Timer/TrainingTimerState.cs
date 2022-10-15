using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrainingView
{
    // タイマーステータス
    class TrainingTimerState
    {
        // ステータス
        public enum TimerState
        {
            Exec = 1,
            Interval = 2,
            Next = 3,
            Complete = 4,
        }

        readonly public TrainingViewData Training;
        readonly public TimerState State;

        public TrainingTimerState(TrainingViewData training, TimerState state)
        {
            this.Training = training;
            this.State = state;
        }
    }
}

