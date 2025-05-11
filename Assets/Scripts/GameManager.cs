using System;
using Interaction;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [Inject]
    private readonly ControlsManager _controlsManager;

    [Inject]
    private readonly AudioManager _audioManager;

    public event Action GameStartEvent;

    private bool _isGameStarted;
    private bool _isFirstGame = true;

    private void Awake()
    {
        _controlsManager.Restart += OnRestartGame;
    }

    private void OnRestartGame(InputAction.CallbackContext context)
    {
        if (!context.started)
            return;

        RestartGame();
    }

    public void OnGameStart()
    {
        if (_isGameStarted)
            return;

        Debug.Log("Game started!");
        _isGameStarted = true;

        if (_isFirstGame)
            _audioManager.PlayClip("bells", 1, Random.Range(0.9f, 1.1f));

        _isFirstGame = false;
        
        GameStartEvent?.Invoke();
    }

    public void RestartGame()
    {
        if (!_isGameStarted)
            return;

        _isGameStarted = false;
        OnGameStart();
    }
}