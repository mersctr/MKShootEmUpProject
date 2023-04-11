using System;
using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    private bool _entered;
    public event Action OnLevelEnteredEvent;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (_entered)
            return;

        _entered = true;
        OnLevelEnteredEvent?.Invoke();
    }

}