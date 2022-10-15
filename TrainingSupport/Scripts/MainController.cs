using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainController : ControllerBase<MainViewModel>
{
    // 各画面をインターフェースで保持
    Dictionary<ViewChangeManager.SceneType, IView> _ViewDic;
    IView _CurrentView;

    // Start
    protected override void _OnStart()
    {
        // 画面遷移用オブジェクト作成
         var changeView = new ViewChangeManager((type, linker) => 
         {
             this._ChangeView(type, linker);
         });

        // スタート画面
        this._ViewModel.StartView.ExternalStart(new StartLinker()
        {
            ViewChangeManager = changeView,
        });

        // トレーニング画面
        this._ViewModel.TrainingView.ExternalStart(new TrainingLinker()
        {
            ViewChangeManager = changeView,
        });

        // コンプリート画面
        this._ViewModel.CompleteView.ExternalStart(new CompleteLinker()
        {
            ViewChangeManager = changeView,
        });

        // 画面をDictionaryに格納
        this._ViewDic = new Dictionary<ViewChangeManager.SceneType, IView>();
        this._ViewDic.Add(ViewChangeManager.SceneType.Start, this._ViewModel.StartView);
        this._ViewDic.Add(ViewChangeManager.SceneType.Training, this._ViewModel.TrainingView);
        this._ViewDic.Add(ViewChangeManager.SceneType.Complete, this._ViewModel.CompleteView);

        this._CurrentView = this._ViewDic[ViewChangeManager.SceneType.Start];
        this._CurrentView.Active(null);

        // 初期画面以外を非表示
        foreach(IView iView in this._ViewDic.Values)
        {
            if(iView != this._CurrentView)
                iView.Disable();
        }

        // テスト
        ApiManager.SendPostRequest<TrainingSupportPost>((result) => {
            Debug.Log(result.hoge);
        }, new TrainingSupportPostRequest());
    }

    void _ChangeView(ViewChangeManager.SceneType type, ViewActiveLinker linker)
    {
        if(this._ViewDic == null)
            return;

        if(!this._ViewDic.ContainsKey(type))
            return;

        // 表示中の画面を非表示
        if(this._CurrentView != null)
            this._CurrentView.Disable();

        // 変更画面を表示
        IView iView = this._ViewDic[type];
        iView.Active(linker);
        this._CurrentView = iView;
    }
}

// 画面変更オブジェクト
public class ViewChangeManager
{
    public enum SceneType
    {
        Start = 1,
        Training = 2,
        Complete = 3,
    }

    System.Action<SceneType, ViewActiveLinker> _ChangeViewAction;

    public ViewChangeManager(System.Action<SceneType, ViewActiveLinker> action)
    {
        this._ChangeViewAction = action;
    }

    public void ChangeView(SceneType type)
    {
        Debug.Log("ChangeScene");
        this._ChangeViewAction(type, null);
    }

    // 腕のトレーニング開始
    public void ChangeArmTraining()
    {
        var linker = new TrainingView.TrainingViewActiveLinker();
        // 腕トレーニングリストを作成
        linker.TrainingInfos = TrainingList.CreateTrainingList(TrainingList.TrainingType.Arm);

        this._ChangeViewAction(SceneType.Training, linker);
    }

    // 腹筋のトレーニング開始
    public void ChangeSitUpTraining()
    {
        var linker = new TrainingView.TrainingViewActiveLinker();
        // 腹筋トレーニングリストを作成
        linker.TrainingInfos = TrainingList.CreateTrainingList(TrainingList.TrainingType.SitUp);

        this._ChangeViewAction(SceneType.Training, linker);
    }

    // ウォーミングアップ開始
    public void ChangeWarmingUpTraining()
    {
        var linker = new TrainingView.TrainingViewActiveLinker();
        // 腹筋トレーニングリストを作成
        linker.TrainingInfos = TrainingList.CreateTrainingList(TrainingList.TrainingType.WarmingUp);

        this._ChangeViewAction(SceneType.Training, linker);
    }

}

// 画面用のインターフェース
interface IView
{
    void Disable();
    void Active(ViewActiveLinker linker);

}

public class ViewActiveLinker
{

}