using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ------------------------------ 
 * 腹筋トレーニング
 * ------------------------------ */
public class SitUpTrainingList : TrainingList
{
    // トレーニングメニューの作成
    public override TrainingInfo[] CreateList()
    {
        var result = new List<TrainingInfo>();
        var menuData = CsvToStruct.LoadAll<SitUpMenuStruct>();

        foreach(var data in menuData)
        {
            result.Add(new TrainingInfo(data.Name, data.IconPath, data.CoinNum));
        }

        return result.ToArray();
    }
}

/* ------------------------------ 
 * メニューデータ構造体
 * ------------------------------ */
[CsvFilePathAtrribute("TrainingIcon/SitUp/Menu")]
public class SitUpMenuStruct
{
    [CsvColumnAtrribute("Name")]
    public string Name;

    [CsvColumnAtrribute("IconPath")]
    public string IconPath;

    [CsvColumnAtrribute("CoinNum")]
    public int CoinNum;
}
