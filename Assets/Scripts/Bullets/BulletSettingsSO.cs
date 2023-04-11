using UnityEngine;

[CreateAssetMenu(fileName = "BulletSettingsSO", menuName = ScriptablesPaths.BulletSettingsSO, order = 0)]
public class BulletSettingsSO : ScriptableObject
{
    public BulletType BulletType;
    public Bullet BulletPrefab;
    public GameObject MuzzlePrefab;
}