using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JsonDataUtil
{
    // 保存
    public static void Save<T>(T obj, string path)
    {
        Debug.Log("Save");

        // データを保持するためのJsonに変換
        var json = JsonUtility.ToJson(obj);

        StreamWriter writer = new StreamWriter(path);
        writer.Write(json);
        writer.Close();
    }

    // 読み込み
    // 読み込み失敗したらnull
    public static T Load<T>(string path)
    where T : class
    {
        Debug.Log("Load");

        // ファイルがなければ初期化
        if(!File.Exists(path))
            return null;

        StreamReader reader = new StreamReader(path);
        string json = reader.ReadToEnd();
        reader.Close();

        // jsonをクラスに変換して保存
        var obj = JsonUtility.FromJson<T>(json);

        return obj;
    }

}
