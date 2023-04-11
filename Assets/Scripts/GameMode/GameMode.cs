using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameMode : MonoBehaviour
{
    [Inject] private ActivityManager _activityManager;
    [Inject] private EnemyManager _enemyManager;
    [Inject] private LevelManager _levelManager;
    [Inject] private PlayerController _playerController;

    private void Start()
    {
        _playerController.OnPlayerDied += PlayerController_OnPlayerDied;
        StartCoroutine(StartSequence());
    }
    
    private void OnDestroy()
    {
        _playerController.OnPlayerDied -= PlayerController_OnPlayerDied;
    }
    
    // Currently i don't need this to be a sequence but i want to develop this game further so normally i would  have some kind of 
   // starting sequence that makes sure everything is activate in proper order in the game
    private IEnumerator StartSequence()
    {
        yield return new WaitForEndOfFrame();
        _playerController.gameObject.SetActive(false);
        ShowStartUI();
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
    
    private IEnumerator DeathSequence()
    {
        yield return new WaitForEndOfFrame();
        SetTimeScale(0.2f);
        yield return new WaitForSecondsRealtime(4);
        ShowDeathUI();
    }

    public void OnVictory()
    {
        StartCoroutine(BossDeathSequence());
    }
    
    private IEnumerator BossDeathSequence()
    {  
        yield return new WaitForEndOfFrame();
       SetTimeScale(0.2f);
       yield return new WaitForSecondsRealtime(4);
       ShowVictoryUI();
    }

    private void ShowDeathUI()
    {
        _activityManager.Create<DeathActivity>(ActivityNames.DeathUIActivity);
    }

    private void ShowStartUI()
    {
        _activityManager.Create<MainMenuActivity>(ActivityNames.MainMenuActivity);
    }

    private void ShowVictoryUI()
    {
        //At this point  DeathActivity and VictoryActivity are basically are the same so no need 
        // for new script
        _activityManager.Create<DeathActivity>(ActivityNames.VictoryActivity);
    }

    private void SetTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;
    }

    public void StartGame()
    {
        _levelManager.ActivateLevels();
        _playerController.gameObject.SetActive(true);
        SetTimeScale(1);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

}