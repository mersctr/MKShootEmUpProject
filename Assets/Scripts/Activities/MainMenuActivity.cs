using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MainMenuActivity : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [Inject] private Activity _activity;

    [Inject] private ActivityManager _activityManager;
    [Inject] private GameMode _gameMode;

    private void Start()
    {
        _startButton.onClick.AddListener(OnStartClick);
    }

    private void OnDestroy()
    {
        _startButton.onClick.RemoveAllListeners();
    }

    private void OnStartClick()
    {
        Debug.Log("On Start Click");
        _gameMode.StartGame();
        _activity.Back();
    }
}