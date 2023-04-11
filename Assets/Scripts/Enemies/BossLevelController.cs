using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class BossLevelController : MonoBehaviour
{
    [SerializeField] private GameObject _wall;
    [SerializeField] private EnemyBossController _bossController;
    [SerializeField] private LevelTrigger _levelTrigger;
    [SerializeField] private SpawnPoint[] _spawnPoints;
    [SerializeField] private FightStage[] _stages;
    private bool _allStagesCompleted;
    [Inject] private EnemyManager _enemyManager;
    private bool _levelActive;
    [Inject] private TargetGroupController _targetGroup;

    private void Awake()
    {
        _levelTrigger.OnLevelEnteredEvent += LevelTrigger_OnLevelEntered;
    }

    private void OnDestroy()
    {
        _levelTrigger.OnLevelEnteredEvent -= LevelTrigger_OnLevelEntered;
    }

    private void LevelTrigger_OnLevelEntered()
    {
        _targetGroup.AddTargetToGroup(_bossController.transform, 1, 10);
        ActivateLevel();
    }

    private void ActivateLevel()
    {
        if (_levelActive)
            return;

        ActivateWall();
        ActivateBoss();
        StartFight();

        _levelActive = true;
    }

    private void StartFight()
    {
        StartCoroutine(FightSequence());
    }

    private void ActivateWall()
    {
        _wall.gameObject.SetActive(true);
    }

    private void ActivateBoss()
    {
        _bossController.gameObject.SetActive(true);
        _bossController.ActivateBoss();
    }

    // I want to keep it simple and not over do it at this stage.
    // Normally for enemies i  would want to use some sort of state machine (behaviour tree)
    // so the fights would be more exiting
    private IEnumerator FightSequence()
    {
        yield return new WaitForSeconds(5);
        yield return FightStageCoroutine(0);
        yield return FightStageCoroutine(1);
        yield return FightStageCoroutine(2);
    }

    private IEnumerator FightStageCoroutine(int stageIndex)
    {
        Debug.Log($"<color=yellow>FightStageCoroutine - >stageIndex {stageIndex} </color>");
        yield return new WaitForEndOfFrame();
        var stage = _stages[stageIndex];

        while (!stage.CanStageBeActivated(_bossController.Vitals.GetLifePercentage())) yield return null;

        foreach (var rule in stage.Rules) _enemyManager.SpawnEnemy(rule.EnemiesCount, rule.EnemyToSpawn, _spawnPoints);
    }


    //Since these are classes useAble in this script only i am keeping them in one file
    [Serializable]
    private class FightStage
    {
        [Range(0, 1)] public float LifeActivationStage;
        public Rules[] Rules;

        public bool CanStageBeActivated(float getLifePercentage)
        {
            return getLifePercentage <= LifeActivationStage;
        }
    }


    [Serializable]
    private class Rules
    {
        public GameObject EnemyToSpawn;
        public int EnemiesCount;
    }
}