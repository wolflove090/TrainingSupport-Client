using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MedalGameController : ControllerBase<MedalGameViewModel>
{
    const int COST_MEDAL = 10;

    MedalComponent _Component;

    // Start
    protected override void _OnStart()
    {
        this._Component = this.GetComponent<MedalComponent>();

        // 初期表示
        this._ViewModel.CoinNumLabel.text = StringUtil.GetCommaSeparation(Coin.GetNum());
        this._ViewModel.BottleNum.text = Bottle.GetBottleNum().ToString();

        // 初期メダル投下
        StartCoroutine("_OccurMedal");

        // メダルの投下
        this._Component.Medal.SetActive(false);
        this._ViewModel.TapButton.OnClick = () => 
        {
            // 必要数メダルがなければ抜ける
            if(Coin.GetNum() < COST_MEDAL)
                return;

            var medalObj = GameObject.Instantiate(this._Component.Medal, this._Component.MedalSpot.transform.position, Quaternion.identity, this._Component.MedalSpot.transform);
            medalObj.SetActive(true);

            Coin.DelNum(COST_MEDAL);
            Bottle.AddMedalNum(1);
         
            this._ViewModel.CoinNumLabel.text = StringUtil.GetCommaSeparation(Coin.GetNum());

            // ボトルがいっぱいになったら切り替える
            if(Bottle.IsBottleMax())
            {
                Bottle.ExchangeNextBottle();
                this._ViewModel.BottleNum.text = Bottle.GetBottleNum().ToString();
            } 
        };

        // 戻るボタン
        this._ViewModel.BackButton.OnClick = () => 
        {
            // シーン遷移前に追加した分を追加
            Bottle.Save();
            SceneManager.LoadScene("Main", LoadSceneMode.Single);
        };

        SoundManager.PlayBgm(SoundManager.Bgm.AshokaGoldRush);
    }

    // 保持されたメダルの生成
    IEnumerator _OccurMedal()
    {
        for(int i = 0; i < Bottle.GetActiveMedalNum(); i++)
        {
            var medalObj = GameObject.Instantiate(this._Component.Medal, this._Component.OccurMedalSpot.transform.position, Quaternion.identity, this._Component.OccurMedalSpot.transform);
            medalObj.SetActive(true);
            yield return null;
        }
    }

    // Update
    protected override void _OnUpdate()
    {
        if(Application.isEditor)
            return;

        // 加速度センサーの取得
        var dir = Vector3.zero;
        dir.x = Input.acceleration.x;
        dir.y = Input.acceleration.y;

        // 取得した値に合わせて重力方向を変更
        Physics.gravity = dir.normalized * 9.8f;
    }
}