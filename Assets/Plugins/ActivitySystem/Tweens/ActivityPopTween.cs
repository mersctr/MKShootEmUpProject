using System;
using UnityEngine;
using DG.Tweening;

public class ActivityPopTween : ActivityTween
{
    [SerializeField] private RectTransform _contentRect = null;

    private Sequence _sequence;

    private void OnDestroy()
    {
        KillTween();
    }

    public override void FadeIn(Action onDone)
    {
        KillTween();

        CanvasGroup.alpha = 0f;
        _contentRect.localScale = Vector3.one * 0.25f;

        _sequence = DOTween.Sequence();
        _sequence.Join(CanvasGroup.DOFade(1f, 0.2f).SetEase(Ease.OutSine));
        _sequence.Join(_contentRect.DOScale(1f, 0.2f).SetEase(Ease.OutBack));
        
        _sequence.SetUpdate(true)
            .OnComplete(() => onDone?.Invoke())
            .OnKill(() => _sequence = null);
    }

    public override void FadeOut(Action onDone)
    {
        KillTween();

        // Instant content hide.
        _contentRect.localScale = Vector3.zero;

        _sequence = DOTween.Sequence();
        _sequence.Join(CanvasGroup.DOFade(0f, 0.2f).SetEase(Ease.OutSine));

        _sequence.SetUpdate(true)
            .OnComplete(() => onDone?.Invoke())
            .OnKill(() => _sequence = null);
    }

    public override void KillTween(bool complete = false)
    {
        if (_sequence != null)
            _sequence.Kill(complete);
    }
}
