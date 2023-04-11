using System.Collections.Generic;
using UnityEngine;

public class TargetGroupListener : MonoBehaviour
{
    [SerializeField] private int _maxObservablePlayers = 10;
    private readonly List<Transform> EnemiesInsideRange = new();
    private List<Transform> EnemiesOnFocus = new();


    private void OnTriggerEnter(Collider other)
    {
        EnemiesInsideRange.Add(other.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        if (EnemiesInsideRange.Contains(other.transform)) EnemiesInsideRange.Remove(other.transform);
    }
}