using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Coin
{
    const string FILE_NAME = "coin_data.txt";

    // ------------------------------
    // static
    // ------------------------------

    // シングルトン
    static Coin _ActualSingleton;
    static Coin _Singleton
    {
        get
        {
            if(_ActualSingleton == null)
            {
                _ActualSingleton = new Coin();
            }

            return _ActualSingleton;
        }
    }

    public static int GetNum()
    {
        return _Singleton._Num;
    }

    public static int AddNum(int amount)
    {
        if(amount < 0)
            throw new System.Exception("加算値が負");

        Debug.Log("コイン増加 = " + amount);
        _Singleton._Num += amount;
        // TODO 一旦セーブポイントを配置 ゲーム終了時にセーブするようにしたい
        _Singleton._Save();
        return _Singleton._Num;
    }

    public static int DelNum(int amount)
    {
        if(amount < 0)
            throw new System.Exception("加算値が負");

        Debug.Log("コイン減少 = " + amount);
        _Singleton._Num = Mathf.Max(_Singleton._Num - amount, 0);
        // TODO 一旦セーブポイントを配置 ゲーム終了時にセーブするようにしたい
        _Singleton._Save();
        return _Singleton._Num;
    }

    // ------------------------------
    // member
    // ------------------------------

    string _Path
    {
        get
        {
            return Application.persistentDataPath + "/" + FILE_NAME;
        }
    }

    int _Num;

    Coin()
    {
        this._Load();
    }

    // ローカルに保存
    void _Save()
    {
        Debug.Log("コインの保存 = " + this._Path);

        // データを保持するためのJsonに変換
        var coinJson = new CoinJson()
        {
            Num = this._Num,
        };
        var json = JsonUtility.ToJson(coinJson);

        StreamWriter writer = new StreamWriter(this._Path);
        writer.Write(json);
        writer.Close();
    }

    // ローカルから取得
    void _Load()
    {
        Debug.Log("コインの読み取り = " + this._Path);

        // ファイルがなければ初期化
        if(!File.Exists(this._Path))
        {
            return;
        }

        StreamReader reader = new StreamReader(this._Path);
        string json = reader.ReadToEnd();
        reader.Close();

        // jsonをクラスに変換して保存
        var coinJson = JsonUtility.FromJson<CoinJson>(json);
        this._Num = coinJson.Num;
    }
}

// コイン情報を保持するためのクラス
class CoinJson
{
    public int Num;
}
