using System;
using Interaction;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class GameManager : MonoBehaviour
{
    [Inject]
    private readonly ControlsManager _controlsManager;

    public event Action GameStartEvent;

    private bool _isGameStarted;

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