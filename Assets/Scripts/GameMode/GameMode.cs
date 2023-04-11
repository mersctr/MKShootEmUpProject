using System.Collections;
using UnityEngine;
using Zenject;

public class GameMode : MonoBehaviour
{
    [Inject] private ActivityManager _activityManager;
    private bool _boosFight;
    [Inject] private EnemyManager _enemyManager;
    [Inject] private LevelManager _levelManager;
    [Inject] private PlayerController _playerController;
    [Inject] private TargetGroupController _targetGroupController;

    private void Start()
    {
        _playerController.OnPlayerDied += PlayerController_OnPlayerDied;
        StartCoroutine(StartSequence());
    }

    private void OnDestroy()
    {
    }

    private void LevelManager_OnPlayerEnterdBossLevel()
    {
    }


    private void PlayerController_OnPlayerDied()
    {
        StartDeathSequence();
    }

    private void StartDeathSequence()
    {
        StopAllCoroutines();
        StartCoroutine(DeathSequence());
    }


    private IEnumerator StartSequence()
    {
        yield return new WaitForEndOfFrame();
        SetTimeScale(1);
        ShowUI();
    }

    private IEnumerator DeathSequence()
    {
        yield return new WaitForEndOfFrame();
        SetTimeScale(0.2f);
        yield return new WaitForSecondsRealtime(3);
        ShowUI();
    }

    private void StarBossFightSequence()
    {
        StopAllCoroutines();
        StartCoroutine(BossFightSequence());
        _boosFight = true;
    }

    private IEnumerator BossFightSequence()
    {
        yield return new WaitForEndOfFrame();
        CloseGates();
    }

    private void CloseGates()
    {
        //  _levelManager.BossLevvel
    }


    private void ShowUI()
    {
        _activityManager.Create<MainMenuActivity>(ActivityNames.MainMenuActivity);
    }

    private void SetTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;
    }

    public void StartGame()
    {
    }
}