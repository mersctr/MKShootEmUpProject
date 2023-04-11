using System;
using UnityEngine;
using DG.Tweening;

public class ActivityFadeTween : ActivityTween
{
    private Tween _tween;

    public override void FadeIn(Action onDone)
    {
        KillTween();

        CanvasGroup.alpha = 0;
        _tween = CanvasGroup.DOFade(1, 0.15f)
            .SetEase(Ease.OutSine)
            .SetUpdate(true)
            .OnComplete(() => onDone?.Invoke())
            .OnKill(() => _tween = null);
    }

    public override void FadeOut(Action onDone)
    {
        KillTween();

        _tween = CanvasGroup.DOFade(0, 0.15f)
            .SetEase(Ease.OutSine)
            .SetUpdate(true)
            .OnComplete(() => onDone?.Invoke())
            .OnKill(() => _tween = null);
    }

    public override void KillTween(bool complete = false)
    {
        if (_tween != null)
            _tween.Kill(complete);
    }

    private void OnDestroy()
    {
        KillTween();
    }
}
