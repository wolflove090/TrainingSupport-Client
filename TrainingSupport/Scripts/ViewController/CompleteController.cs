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
        var completeLinker = linker as CompleteViewActiveLinker;
        if(completeLinker == null)
            throw new System.Exception("リンカーを変換できなかった");

        SoundManager.PlaySe(SoundManager.Se.Fanfare);

        Debug.Log("コンプリート画面");
        this.gameObject.SetActive(true);
        this._ViewModel.ApplaudLabel.text = completeLinker.ApplaudMessage;
    }
}

// アクティブ時の引数を受け取る
public class CompleteViewActiveLinker : ViewActiveLinker
{
    public string ApplaudMessage;
}
