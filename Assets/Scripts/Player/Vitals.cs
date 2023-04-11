using System;
using UnityEngine;

public class Vitals : MonoBehaviour, IHittable
{
    [SerializeField] private int _defaultLife;
    [SerializeField] private bool _canReceiveDamage=true;
    public bool IsAlive => CurrentLife > 0;
    public int CurrentLife { get; private set; }
    public int DefaultLife => _defaultLife;

    private void Awake()
    {
        UpdateHeath(_defaultLife);
    }

    private void OnEnable()
    {
        UpdateHeath(_defaultLife);
    }

    public void Hit(int Damage)
    {
        if (!_canReceiveDamage)
            return;
        
        if (CurrentLife == 0)
            return;

        var newLife = CurrentLife;
        newLife -= Damage;

        UpdateHeath(newLife);

        OnHit?.Invoke();

        if (CurrentLife <= 0)
            Die();
    }

    public void Die()
    {
        CurrentLife = 0;
        OnDeath?.Invoke();
    }

    public event Action OnHit;
    public event Action OnDeath;
    public event Action OnChange;

    public void AddLife(int life)
    {
        var newLife = CurrentLife;
        newLife += life;

        UpdateHeath(newLife);
    }

    private void UpdateHeath(int newValue)
    {
        CurrentLife = newValue;

        CurrentLife = Mathf.Max(0, CurrentLife);
        CurrentLife = Mathf.Min(_defaultLife, CurrentLife);

        OnChange?.Invoke();
    }

    public float GetLifePercentage()
    {
        return CurrentLife / (float)DefaultLife;
    }

    public bool LifIsBelow(float percentage)
    {
        return CurrentLife / (float)DefaultLife < percentage;
    }
}