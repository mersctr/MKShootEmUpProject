using System;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

[RequireComponent(typeof(Canvas))]
public class Activity : MonoBehaviour
{
    [Inject] private ActivityManager _activityManager;
    [Inject] private ActivityDepth _activityDepth;
    [Inject] private IInstantiator _instantiator;

    [Tooltip("Showing translucent screen will keep previous one visible. Good for dialogue windows.")]
    [SerializeField]
    private bool _isTranslucent;

    [Tooltip("Should window close on back button?")]
    [SerializeField]
    private bool _closeOnBack;

    public event Action OnFinish;
    public event Action OnFadedOutComplete;

    public bool IsTranslucent => _isTranslucent;
    public bool CloseOnBack { get => _closeOnBack; set => _closeOnBack = value; }
    public bool DestroyOnClose { get; set; } = true;
    public ActivityState State { get; private set; } = ActivityState.Created;

    private CanvasGroup _canvasGroup;
    public CanvasGroup CanvasGroup
    {
        get
        {
            if (_canvasGroup == null)
            {
                _canvasGroup = GetComponent<CanvasGroup>();
                Assert.IsNull(_canvasGroup, "Activity should not have CanvasGroup in prefab. It will be added automatically. " + gameObject.name);

                if (_canvasGroup == null)
                    _canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }

            return _canvasGroup;
        }
    }

    private ActivityTween _activityTween;
    public ActivityTween ActivityTween
    {
        get
        {
            if (_activityTween == null)
            {
                _activityTween = GetComponent<ActivityTween>();

                if (_activityTween == null)
                    _activityTween = _instantiator.InstantiateComponent<ActivityFadeTween>(gameObject);
            }

            return _activityTween;
        }
    }

    private void Start()
    {
        if (State != ActivityState.Finished && _activityManager.Contains(this) == false)
            Debug.LogError("Activity not created using ActivityManager! Use ActivityManager.Show instead.", this);

        SetCanvas();
    }

    private void OnDestroy()
    {
        ReleaseActivity();
    }

    /// <summary>
    /// Show this screen if pre created screen.
    /// </summary>
    public void Show()
    {
        _activityManager.StartCreatedActivity(this);
    }

    /// <summary>
    /// If we call finish, screen will be hidden and removed form backstack.
    /// </summary>
    public void Finish()
    {
        if (State == ActivityState.Finished)
            return;

        _activityManager.FinishCreatedActivity(this);
    }

    /// <summary>
    /// Back requested. Valid only in resumed state.
    /// </summary>
    public void Back()
    {
        if (State != ActivityState.Resumed)
            return;

        OnBackCallbacks();

        if (_closeOnBack)
            Finish();
    }

    /// <summary>
    /// Should be called only by AppUI!
    /// </summary>
    public void ActStart()
    {
        Assert.IsTrue(State == ActivityState.Created || State == ActivityState.Finished);

        // Make window interactable
        CanvasGroup.interactable = true; // Enable UI interactions
        CanvasGroup.blocksRaycasts = true;

        // Make window visible
        gameObject.SetActive(true);

        State = ActivityState.Started;
        OnStartCallbacks();

        ActivityTween.KillTween();
        ActivityTween.FadeIn(FadeInFinish);
    }

    /// <summary>
    /// Should be called only by AppUI!
    /// </summary>
    public void ActResume()
    {
        Assert.IsTrue(State == ActivityState.Started || State == ActivityState.Paused);

        // Make window interactable - in case it was hidden
        CanvasGroup.interactable = true; // Enable UI interactions

        // Make window visible - in case it was hidden
        gameObject.SetActive(true); // Make visible if was hidden.

        State = ActivityState.Resumed;
        OnResumeCallbacks();
    }

    /// <summary>
    /// Should be called only by AppUI!
    /// </summary>
    public void ActPause()
    {
        Assert.IsTrue(State == ActivityState.Resumed);

        // Make it non interactable
        CanvasGroup.interactable = false; // Disable UI interactions

        State = ActivityState.Paused;
        OnPauseCallbacks();
    }

    /// <summary>
    /// Hide this instance when next activity is not translucent.
    /// </summary>
    public void ActHide()
    {
        gameObject.SetActive(false); // Make invisible.
    }

    /// <summary>
    /// Should be called only by AppUI!
    /// </summary>
    public void ActFinish()
    {
        Assert.IsTrue(State == ActivityState.Paused);

        CanvasGroup.blocksRaycasts = false;
        State = ActivityState.Finished;
        OnFinishCallbacks();

        ActivityTween.KillTween();
        ActivityTween.FadeOut(FadeOutFinish);
    }

    private void FadeInFinish()
    {
        _activityManager.UpdateFadedActivityTranslucency(this);
        OnFadeInCompleteCallbacks();
    }

    private void FadeOutFinish()
    {
        gameObject.SetActive(false);
        OnFadedOutCompleteCallbacks();

        if (DestroyOnClose)
            Destroy(gameObject);
    }

    private void SetCanvas()
    {
        var canvas = GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = _activityDepth.Request();
    }

    private void ReleaseActivity()
    {
        var canvas = GetComponent<Canvas>();
        _activityDepth.Release(canvas.sortingOrder);
    }

    public void OnFadeInCompleteCallbacks() => NotifyInterfaces<IActivityFadeEvents>((x) => x.OnFadeInComplete);
    public void OnFadedOutCompleteCallbacks()
    {
        OnFadedOutComplete?.Invoke();
        NotifyInterfaces<IActivityFadeEvents>((x) => x.OnFadedOutComplete);
    }

    public void OnStartCallbacks() => NotifyInterfaces<IActivityEvents>((x) => x.OnStart);
    public void OnResumeCallbacks() => NotifyInterfaces<IActivityEvents>((x) => x.OnResume);
    public void OnBackCallbacks() => NotifyInterfaces<IActivityBackEvents>((x) => x.OnBack);
    public void OnPauseCallbacks() => NotifyInterfaces<IActivityEvents>((x) => x.OnPause);
    public void OnFinishCallbacks()
    {
        OnFinish?.Invoke();
        NotifyInterfaces<IActivityEvents>((x) => x.OnFinish);
    }

    private void NotifyInterfaces<T>(Func<T, Action> callback)
    {
        foreach (var component in GetComponents<T>())
            callback(component).Invoke();
    }
}