using DG.Tweening;
using Interaction;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace UI
{
    public class MainCanvasController : MonoBehaviour
    {
        [Inject]
        private readonly ControlsManager _controlsManager;

        [Inject]
        private readonly GameManager _gameManager;

        [SerializeField] private CanvasGroup _titleScreenCanvasGroup;

        private void Awake()
        {
            _controlsManager.AnyKey += OnAnyKey;
        }

        private void OnAnyKey(InputAction.CallbackContext context)
        {
            if (!context.started)
                return;

            _controlsManager.AnyKey -= OnAnyKey;
            HideTitleScreen();
        }

        private void HideTitleScreen()
        {
            _titleScreenCanvasGroup.DOFade(0, 1f)
                .OnComplete(() =>
                {
                    _titleScreenCanvasGroup.gameObject.SetActive(false);
                    _gameManager.OnGameStart();
                }).SetLink(gameObject);
        }
    }
}