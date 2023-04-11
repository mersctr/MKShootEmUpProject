using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
   [SerializeField] private LevelSection[] _levelSections;

    public void ActivateLevels()
    {
        foreach (var section in _levelSections)
        {
            section.EnableSection(true);
        }
    }
}