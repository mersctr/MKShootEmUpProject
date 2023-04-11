using UnityEngine;

[CreateAssetMenu(fileName = "WeponSettingsSO", menuName = ScriptablesPaths.WeponSettingsSO, order = 0)]
public class WeponSettingsSO : ScriptableObject
{
    public float FireRate = 0.5f;
    public int DamagePerBullet = 10;
    public BulletType BulletType;
}