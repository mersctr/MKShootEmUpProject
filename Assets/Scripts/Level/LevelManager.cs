using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelSection _bossLevel;
    public LevelSection BossLevel => _bossLevel;
}