using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ------------------------------ 
 *トレーニング作成用抽象クラス
 * ------------------------------ */
public abstract class TrainingList
{
    public enum TrainingType
    {
        WarmingUp,
        Arm,
        SitUp,
    }

    /* ------------------------------ 
     * トレーニングリストの作成
     * ------------------------------ */
    public static TrainingInfo[] CreateTrainingList(TrainingType type)
    {
        TrainingList listObj = null;

        switch(type)
        {
            case TrainingType.Arm:
                listObj = new ArmTrainingList();
                break;
            case TrainingType.SitUp:
                listObj = new SitUpTrainingList();
                break;
            case TrainingType.WarmingUp:
                listObj = new WarmingUpTrainingList();
                break;
            default:
                throw new System.Exception("未定義のタイプです");

        }

        return listObj.CreateList();
    }

    /* ------------------------------ 
     * リスト作成の抽象メソッド
     * ------------------------------ */
    public abstract TrainingInfo[] CreateList();
}
