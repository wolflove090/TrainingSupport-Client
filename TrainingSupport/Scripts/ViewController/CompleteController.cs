using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteController : ChildController<CompleteViewModel, CompleteLinker>, IView
{
    // Start
    protected override void _OnStart()
    {
        this._ViewModel.ScreenTap.OnClick = () => 
        {
            this._Linker.ViewChangeManager.ChangeView(ViewChangeManager.SceneType.Start);
        };
    }

    void IView.Disable()
    {
        Debug.Log("Disable");
        this.gameObject.SetActive(false);
    }

    void IView.Active(ViewActiveLinker linker)
    {
        SoundManager.PlaySe(SoundManager.Se.Fanfare);

        Debug.Log("コンプリート画面");
        this.gameObject.SetActive(true);
    }
}
