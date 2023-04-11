using System;
using System.Collections;
using Cinemachine;
using UnityEngine;
using Zenject;

public class BossLevelController : MonoBehaviour
{
    [SerializeField] private GameObject _cinemachineCamera;
    [SerializeField] private GameObject _wall;
    [SerializeField] private EnemyBossController _bossController;
    [SerializeField] private LevelTrigger _levelTrigger;
     private SpawnPoint[] _spawnPoints;
    [SerializeField] private FightStage[] _stages;
    private bool _allStagesCompleted;
    [Inject] private EnemyManager _enemyManager;
    private bool _levelActive;
    [Inject] private ActivityManager _activityManager;
    [Inject] private CinemachineTargetGroup _targetGroup;
    [Inject] private GameMode _gameMode;
    
    private void Awake()
    {
        _spawnPoints = GetComponentsInChildren<SpawnPoint>(true);
        _levelTrigger.OnLevelEnteredEvent += LevelTrigger_OnLevelEntered;
        _bossController.Vitals.OnDeath += EnemyBossController_OnDeath;
    }

    private void OnDestroy()
    {
        _levelTrigger.OnLevelEnteredEvent -= LevelTrigger_OnLevelEntered;
        _bossController.Vitals.OnDeath -= EnemyBossController_OnDeath;

    }

    private void EnemyBossController_OnDeath()
    {
        StopAllCoroutines();;
        _levelActive = false;
        _gameMode.OnVictory();
    }

    private void LevelTrigger_OnLevelEntered()
    {
        var cinemachineCameraOffset=_cinemachineCamera.GetComponent<CinemachineCameraOffset>();
        cinemachineCameraOffset.enabled = false;
        _targetGroup.AddMember(_bossController.transform, 2, 10);
        ActivateLevel();
    }

    private void ActivateLevel()
    {
        if (_levelActive)
            return;
        
        CreateBossView();
        ActivateWall();
        ActivateBoss();
        StartFight();

        _levelActive = true;
    }

    private void CreateBossView()
    {
        _activityManager.Create<MainMenuActivity>(ActivityNames.BossViewActivity);
    }

    private void OnBossDeath()
    {
        _gameMode.OnVictory();
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
        yield return new WaitForEndOfFrame();
        var stage = _stages[stageIndex];

        while (!stage.CanStageBeActivated(_bossController.Vitals.GetLifePercentage())) yield return null;
        
        yield return new WaitForEndOfFrame();
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