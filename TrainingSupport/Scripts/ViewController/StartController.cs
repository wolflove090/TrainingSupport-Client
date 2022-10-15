using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartController : ChildController<StartViewModel, StartLinker>, IView
{
    TrainingView.ITimer _CurrentTimer;
    // Start
    protected override void _OnStart()
    {
        this._ViewModel.TrainingA.OnClick = () => 
        {
            this._Linker.ViewChangeManager.ChangeArmTraining();
        };

        this._ViewModel.TrainingB.OnClick = () => 
        {
            this._Linker.ViewChangeManager.ChangeSitUpTraining();
        };

        this._ViewModel.TrainingC.OnClick = () => 
        {
            this._Linker.ViewChangeManager.ChangeWarmingUpTraining();
        };


        // ボトルシーン
        this._ViewModel.BottleButton.OnClick = () => 
        {
            SceneManager.LoadScene("MedalGame", LoadSceneMode.Single);
        };
    }

    void IView.Disable()
    {
        Debug.Log("Disable");
        this.gameObject.SetActive(false);
    }

    void IView.Active(ViewActiveLinker linker)
    {
        Debug.Log("Active");

        SoundManager.PlayBgm(SoundManager.Bgm.KawaiiTankenTai);

        // コイン表示
        this._ViewModel.CoinNumLabel.text = StringUtil.GetCommaSeparation(Coin.GetNum());

        this.gameObject.SetActive(true);
    }
}