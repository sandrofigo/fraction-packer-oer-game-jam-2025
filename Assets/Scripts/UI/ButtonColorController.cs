using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class ButtonColorController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Image _border;
        [SerializeField] private TextMeshProUGUI _text;

        [SerializeField] private Color _normalColor;
        [SerializeField] private Color _highlightedColor;

        private Tween _borderColorTween;
        private Tween _textColorTween;
        private Tween _scaleTween;

        private const float FadeDuration = 0.1f;

        private void Awake()
        {
            Highlight(false, false);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Highlight(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Highlight(false);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _scaleTween.Kill();
            _scaleTween = transform.DOScale(Vector3.one * 0.9f, FadeDuration).SetEase(Ease.OutBack).SetUpdate(true).SetLink(gameObject);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _scaleTween.Kill();
            _scaleTween = transform.DOScale(Vector3.one, FadeDuration).SetEase(Ease.OutBack).SetUpdate(true).SetLink(gameObject);
        }

        private void Highlight(bool shouldHighlight, bool animate = true)
        {
            _borderColorTween.Kill();
            _textColorTween.Kill();

            Color targetColor = shouldHighlight ? _highlightedColor : _normalColor;

            if (animate)
                _borderColorTween = _border.DOColor(targetColor, FadeDuration).SetUpdate(true).SetLink(gameObject);
            else
                _border.color = targetColor;

            if (animate)
                _textColorTween = _text.DOColor(targetColor, FadeDuration).SetUpdate(true).SetLink(gameObject);
            else
                _text.color = targetColor;
        }
    }
}