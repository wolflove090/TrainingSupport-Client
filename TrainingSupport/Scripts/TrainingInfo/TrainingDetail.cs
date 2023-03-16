using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingDetail
{
    readonly public string Name;
    readonly public Sprite Icon;
    readonly public int CoinNum;

    public TrainingDetail(string name, string iconPath, int coin)
    {
        this.Name = name;
        this.Icon = Resources.Load<Sprite>(iconPath);
        this.CoinNum = coin;
    }
}