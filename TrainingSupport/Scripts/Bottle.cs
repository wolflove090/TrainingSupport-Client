using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Bottle
{
    const string FILE_NAME = "bottle_data.txt";
    const int MAX_MEDAL = 500;

    static Bottle _ActualSingleton;
    static Bottle _Singleton
    {
        get
        {
            if(_ActualSingleton == null)
                _ActualSingleton = new Bottle();

            return _ActualSingleton;
        }
    }

    // コインの加算
    public static void AddMedalNum(int amount)
    {
        if(amount < 0)
            throw new System.Exception("加算値が負");

        _Singleton._ActiveMedalNum += amount;
        _Singleton._Save();
    }

    // ボトルが最大になっているか
    public static bool IsBottleMax()
    {
        return _Singleton._ActiveMedalNum >= MAX_MEDAL;
    }

    // 新しいボトルに切り替え
    public static void ExchangeNextBottle()
    {
        _Singleton._ExchangeNextBottle();
    }

    // アクティブメダル数
    public static int GetActiveMedalNum()
    {
        return _Singleton._ActiveMedalNum;
    }

    // 現在のボトル数
    public static int GetBottleNum()
    {
        return _Singleton._MedalNums.Length;
    }

    public static void Save()
    {
        _Singleton._Save();
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

    int[] _MedalNums = new int[]{0};
    int _ActiveMedalNum
    {
        get
        {
            int index = this._MedalNums.Length - 1;
            return this._MedalNums[index];
        }
        set
        {
            int index = this._MedalNums.Length - 1;
            this._MedalNums[index] = value;
        }
    }

    Bottle()
    {
        this._Load();
        Debug.Log("メダル = " + this._ActiveMedalNum);
    }

    // アクティブボトルの作成
    void _ExchangeNextBottle()
    {
        var list = this._MedalNums.ToList();
        list.Add(0);

        this._MedalNums = list.ToArray();
    }

    void _Save()
    {
        var bottle = new BottleJson()
        {
            MedalNums = this._MedalNums,
        };

        JsonDataUtil.Save<BottleJson>(bottle, this._Path);
    }

    void _Load()
    {
        var bottle = JsonDataUtil.Load<BottleJson>(this._Path);
        if(bottle == null)
            return;

        if(bottle.MedalNums == null)
            return;

        this._MedalNums = bottle.MedalNums;
    }

    // データ保存用クラス
    // TODO 配列にする
    class BottleJson
    {
        public int[] MedalNums;
    }
}