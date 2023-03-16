using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrainingView
{
    // トレーニングの表示用データ
    class TrainingViewData
    {
        public static TrainingViewData EMPTY = new TrainingViewData("", null, false);
        public static TrainingViewData COMPLETE = new TrainingViewData("Complete", null, false);
        public static TrainingViewData INTERVAL = new TrainingViewData("Interval", null, true);

        readonly public string Name;
        readonly public Sprite Icon;
        readonly public int CoinNum;
        readonly public bool IsInterval;
        readonly public int IntervalTimeSeconds = 10;

        TrainingViewData(string name, Sprite icon, bool isInterval)
        {
            this.Name = name;
            this.Icon = icon;
            this.IsInterval = isInterval;
        }

        // トレーニング項目生成
        public TrainingViewData(TrainingDetail info, int intervalTimeSec)
        {
            this.Name = info.Name;
            this.Icon = info.Icon;
            this.CoinNum = info.CoinNum;

            this.IsInterval = false;
            this.IntervalTimeSeconds = intervalTimeSec;
        }

        public TrainingViewData(TrainingViewData viewData, bool isInterval)
        {
            this.Name = viewData.Name;
            this.Icon = viewData.Icon;
            this.CoinNum = viewData.CoinNum;

            this.IsInterval = isInterval;
        }
    }
}
