using DG.Tweening;
using UnityEngine;

namespace Box
{
    public class FractionBlockAnimationController : MonoBehaviour
    {
        [Header("Move Animation")]
        [SerializeField]
        private float _moveDuration = 0.3f;

        [SerializeField]
        private Ease _moveEase = Ease.OutSine;

        [SerializeField]
        private float _moveAndRotateDuration = 0.5f;

        [Header("Hover Animation")]
        [SerializeField]
        private float _hoverDuration = 0.1f;

        [SerializeField]
        private Vector3 _hoverScale = Vector3.one * 1.1f;

        [SerializeField]
        private Ease _hoverEase = Ease.OutSine;

        [Header("Select Animation")]
        [SerializeField]
        private float _selectJiggleDuration = 0.5f;

        [SerializeField]
        private float _selectJiggleAmplitude = 1.5f;

        [SerializeField]
        private float _selectJigglePeriod = 0.2f;

        [Header("Invalid Shake")]
        [SerializeField]
        private Vector3 _invalidShakePunch = new(0f, 2f, 0f);

        [SerializeField]
        private float _invalidShakeDuration = 0.15f;

        private Sequence _moveSequence;
        private Sequence _scaleSequence;
        private Sequence _rotateSequence;

        private void OnDestroy()
        {
            _moveSequence.Kill();
            _scaleSequence.Kill();
            _rotateSequence.Kill();
        }

        public void MoveTo(Vector3 position)
        {
            _moveSequence?.Kill();

            _moveSequence = DOTween.Sequence()
                .Append(transform.DOMove(position, _moveDuration).SetEase(_moveEase));
        }
        
        public void MoveTo(Vector3 position, Vector3 rotation)
        {
            _moveSequence?.Kill();
            _rotateSequence.Kill();

            _moveSequence = DOTween.Sequence()
                .Append(transform.DOMove(position, _moveDuration).SetEase(_moveEase));

            _rotateSequence = DOTween.Sequence()
                .Append(transform.DORotate(rotation, _moveDuration).SetEase(_moveEase));
        }

        public void SetHover(bool isHovered)
        {
            _scaleSequence?.Kill();
            _scaleSequence = DOTween.Sequence()
                .Append(transform.DOScale(isHovered ? _hoverScale : Vector3.one, _hoverDuration).SetEase(_hoverEase));
        }

        public void Select()
        {
            _scaleSequence.Kill();
            _rotateSequence.Kill();

            transform.localScale = Vector3.one * 0.7f;

            _scaleSequence = DOTween.Sequence()
                .Append(transform.DOScale(Vector3.one, _selectJiggleDuration))
                .SetEase(Ease.OutElastic, _selectJiggleAmplitude, _selectJigglePeriod);
            
            _rotateSequence = DOTween.Sequence()
                .Append(transform.DORotate(new Vector3(0f, 0f, 0f), _selectJiggleDuration));
        }

        public void DoInvalidShakeAnimation()
        {
            _rotateSequence.Kill();

            _rotateSequence = DOTween.Sequence()
                .Append(transform.DOPunchRotation(_invalidShakePunch, _invalidShakeDuration / 2f))
                .Append(transform.DOPunchRotation(-_invalidShakePunch, _invalidShakeDuration / 2f));
        }

        public void MoveAndRotateTo(Vector3 position, Vector3 rotation, float delay)
        {
            _moveSequence?.Kill();
            _rotateSequence.Kill();

            _moveSequence = DOTween.Sequence()
                .Append(transform.DOMove(position, _moveAndRotateDuration).SetEase(_moveEase))
                .SetDelay(delay);
            
            _rotateSequence = DOTween.Sequence()
                .Append(transform.DORotate(rotation, _moveAndRotateDuration).SetEase(_moveEase))
                .SetDelay(delay);
        }
    }
}