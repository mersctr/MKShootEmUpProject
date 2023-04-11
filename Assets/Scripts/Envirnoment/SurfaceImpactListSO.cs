using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SurfaceImpactListSO", menuName = ScriptablesPaths.SurfaceImpactListSO, order = 0)]
public class SurfaceImpactListSO : ScriptableObject
{
    public List<ImpactSettings> ImapctSettings;

    [Serializable]
    public class ImpactSettings
    {
        public SurfaceType Surface;
        public GameObject ImpactEffct;
    }
}