using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ListOfBulletsSO", menuName = ScriptablesPaths.ListOfBulletsSO, order = 0)]
public class ListOfBulletsSO : ScriptableObject
{
    public List<BulletSettings> Bullets;

    [Serializable]
    public class BulletSettings
    {
        public BulletType BulletType;
        public Bullet BulletPrefab;
        public GameObject MuzzlePrefab;
        public GameObject ShelssPrefab;
        public GameObject HitPointPrefab;
    }
}