using System;
using UnityEngine;
using Zenject;

public abstract class ActivityTween : MonoBehaviour
{
    [Inject] protected Activity _activity;

    protected CanvasGroup CanvasGroup => _activity.CanvasGroup;

    public abstract void FadeIn(Action onDone);
    public abstract void FadeOut(Action onDone);
    public abstract void KillTween(bool complete = false);
}