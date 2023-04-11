using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class DeathActivity : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [Inject] private Activity _activity;
    [Inject] private ActivityManager _activityManager;
    [Inject] private GameMode _gameMode;

    private void Start()
    {
        _startButton.onClick.AddListener(OnRestartClick);
    }

    private void OnDestroy()
    {
        _startButton.onClick.RemoveAllListeners();
    }

    private void OnRestartClick()
    {
        _gameMode.RestartGame();
    }
}
