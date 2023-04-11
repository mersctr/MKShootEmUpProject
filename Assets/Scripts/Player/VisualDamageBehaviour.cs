using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class VisualDamageBehaviour : MonoBehaviour
{
    [SerializeField] private Slider _lifeSlider;
    private bool _hitAnimationActive;
    [Inject] private Vitals _vitals;

    private void Awake()
    {
        _vitals.OnChange += Vitals_OnChange;
    }

    private void OnEnable()
    {
        _hitAnimationActive = false;
    }

    private void OnDestroy()
    {
        _vitals.OnChange -= Vitals_OnChange;
    }

    private void Enable()
    {
        UpdateVisuals();
    }

    private void Vitals_OnChange()
    {
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        var sliderValue = _vitals.CurrentLife / (float)_vitals.DefaultLife;
        _lifeSlider.value = sliderValue;
    }
}