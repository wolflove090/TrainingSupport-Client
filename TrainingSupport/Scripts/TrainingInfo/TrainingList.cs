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

/* ------------------------------ 
 * 二の腕トレーニング
 * ------------------------------ */
public class ArmTrainingList : TrainingList
{
    public override TrainingInfo[] CreateList()
    {
        var result = new List<TrainingInfo>();

        // 必要なトレーニングを追加
        result.Add(new TrainingInfo("クローズスタンス\nプッシュアップ", "TrainingIcon/Arm/CloseStansPushUp", 5)); // 5
        result.Add(new TrainingInfo("アームカール", "TrainingIcon/Arm/ArmeCurl", 2)); // 2
        result.Add(new TrainingInfo("リバースプッシュアップ", "TrainingIcon/Arm/ReversePushUp", 5)); // 5
        result.Add(new TrainingInfo("レッグプルアームカール", "TrainingIcon/Arm/LegPullArmCurl", 1)); // 1
        result.Add(new TrainingInfo("キックバック", "TrainingIcon/Arm/KickBuck", 2)); // 2
        result.Add(new TrainingInfo("レッグプル\nコンセントレーションカール（右）", "TrainingIcon/Arm/LegPullConcentrationCurl", 2)); // 2
        result.Add(new TrainingInfo("レッグプル\nコンセントレーションカール（左）", "TrainingIcon/Arm/LegPullConcentrationCurlRight",2)); // 2
        result.Add(new TrainingInfo("カール&プレス（右）", "TrainingIcon/Arm/CurlPress",1)); // 1
        result.Add(new TrainingInfo("カール&プレス（左）", "TrainingIcon/Arm/CurlPressReverse",1)); // 1
        
        return result.ToArray();
    }
}

/* ------------------------------ 
 * 腹筋トレーニング
 * ------------------------------ */
public class SitUpTrainingList : TrainingList
{
    public override TrainingInfo[] CreateList()
    {
        var result = new List<TrainingInfo>();

        // 必要なトレーニングを追加
        result.Add(new TrainingInfo("シットアップ", "TrainingIcon/SitUp/SitUp", 3));
        result.Add(new TrainingInfo("クランチ", "TrainingIcon/SitUp/Crunch", 3));
        result.Add(new TrainingInfo("ニートゥーチェスト", "TrainingIcon/SitUp/NeatToChest", 3));
        result.Add(new TrainingInfo("ツイストレッグ\nライズ", "TrainingIcon/SitUp/TwistLegRaise", 2));
        result.Add(new TrainingInfo("バイシクルクランチ", "TrainingIcon/SitUp/BicycleCrunch", 5));
        result.Add(new TrainingInfo("プランク", "TrainingIcon/SitUp/Plank", 4));
        result.Add(new TrainingInfo("マウンテンクライマー", "TrainingIcon/SitUp/MountainClimber", 5));
        
        return result.ToArray();
    }
}

/* ------------------------------ 
 * ウォーミングアップ
 * ------------------------------ */
public class WarmingUpTrainingList : TrainingList
{
    public override TrainingInfo[] CreateList()
    {
        var result = new List<TrainingInfo>();

        // 必要なトレーニングを追加
        result.Add(new TrainingInfo("首の運動", "TrainingIcon/WarmingUp/00001", 1));
        result.Add(new TrainingInfo("肩の運動", "TrainingIcon/WarmingUp/00002", 1));
        result.Add(new TrainingInfo("肩回し", "TrainingIcon/WarmingUp/00003", 1));
        result.Add(new TrainingInfo("フードの動き", "TrainingIcon/WarmingUp/00004", 1));
        result.Add(new TrainingInfo("腕を上げる", "TrainingIcon/WarmingUp/00005", 1));
        result.Add(new TrainingInfo("腕を伸ばす", "TrainingIcon/WarmingUp/00006", 1));
        result.Add(new TrainingInfo("腕を後ろに伸ばす", "TrainingIcon/WarmingUp/00007", 1));

        result.Add(new TrainingInfo("アームカールの動き", "TrainingIcon/WarmingUp/00008", 1));
        result.Add(new TrainingInfo("シットアップの動き", "TrainingIcon/WarmingUp/00009", 1));
        result.Add(new TrainingInfo("腕を回す", "TrainingIcon/WarmingUp/00010", 1));
        result.Add(new TrainingInfo("腿上げ", "TrainingIcon/WarmingUp/00011", 1));
        result.Add(new TrainingInfo("腰下げ", "TrainingIcon/WarmingUp/00012", 1));

        result.Add(new TrainingInfo("つま先タッチ", "TrainingIcon/WarmingUp/00013", 1));
        result.Add(new TrainingInfo("屈伸\n膝回し", "TrainingIcon/WarmingUp/00014", 1));
        result.Add(new TrainingInfo("屈伸(強)", "TrainingIcon/WarmingUp/00015", 1));
        result.Add(new TrainingInfo("肩入れ", "TrainingIcon/WarmingUp/00016", 1));
        result.Add(new TrainingInfo("五郎丸スクワット", "TrainingIcon/WarmingUp/00017", 1));
        result.Add(new TrainingInfo("レッグランジ(右)", "TrainingIcon/WarmingUp/00018", 1));
        result.Add(new TrainingInfo("レッグランジ(左)", "TrainingIcon/WarmingUp/00019", 1));

        result.Add(new TrainingInfo("つま先タッチ(強)", "TrainingIcon/WarmingUp/00020", 1));
        result.Add(new TrainingInfo("アキレス腱(右)", "TrainingIcon/WarmingUp/00021", 1));
        result.Add(new TrainingInfo("アキレス腱(左)", "TrainingIcon/WarmingUp/00022", 1));
        
        return result.ToArray();
    }
}