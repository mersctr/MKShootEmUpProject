using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class VisualDamageBehaviour : MonoBehaviour
{
    [SerializeField] private Slider _lifeSlider;
    [InjectOptional] private Vitals _vitals;
    private bool _hitAnimationActive;

    private void Awake()
    {
        if (_vitals == null)
            return;
        
        InitEventListeners();
    }

    private void OnEnable()
    {
        _hitAnimationActive = false;
        UpdateVisuals();
    }

    private void OnDestroy()
    {
        if (_vitals == null)
            return;

        DeInitEventListeners();
    }

    private void Vitals_OnChange()
    {
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        if (_vitals == null)
            return;
        
        var sliderValue = _vitals.CurrentLife / (float)_vitals.DefaultLife;
        _lifeSlider.value = sliderValue;
    }

    public void SetVitalsToObserve(Vitals bossControllerVitals)
    {
        _vitals = bossControllerVitals;
        InitEventListeners();
    }

    private void InitEventListeners()
    {
        _vitals.OnChange += Vitals_OnChange;
    }
    
    private void DeInitEventListeners()
    {
        _vitals.OnChange -= Vitals_OnChange;
    }
}