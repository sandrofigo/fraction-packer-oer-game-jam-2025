using DG.Tweening;
using Interaction;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class MainCanvasController : MonoBehaviour
    {
        [Inject]
        private readonly ControlsManager _controlsManager;

        [Inject]
        private readonly GameManager _gameManager;

        [Inject]
        private readonly AudioManager _audioManager;

        [SerializeField] private CanvasGroup _titleScreenCanvasGroup;
        [SerializeField] private CanvasGroup _mainScreenCanvasGroup;

        [SerializeField] private Button _restartButton;


        private void Awake()
        {
            _controlsManager.AnyKey += OnAnyKey;

            _mainScreenCanvasGroup.alpha = 0;
            _mainScreenCanvasGroup.interactable = false;
            _mainScreenCanvasGroup.blocksRaycasts = false;

            _restartButton.onClick.AddListener(() =>
            {
                _audioManager.PlayClip("knock", 1, Random.Range(0.9f, 1.1f));
                _gameManager.RestartGame();
            });
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
            _titleScreenCanvasGroup.DOFade(0, 0.35f)
                .OnComplete(() =>
                {
                    _titleScreenCanvasGroup.gameObject.SetActive(false);
                    _gameManager.OnGameStart();
                }).SetLink(gameObject);

            _mainScreenCanvasGroup.interactable = true;
            _mainScreenCanvasGroup.blocksRaycasts = true;
            _mainScreenCanvasGroup.DOFade(1, 0.35f)
                .SetDelay(1)
                .SetLink(gameObject);
        }
    }
}