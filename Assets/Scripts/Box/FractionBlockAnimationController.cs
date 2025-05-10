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
        
        private Sequence _moveSequence;

        public void MoveTo(Vector3 position)
        {
            _moveSequence?.Kill();
            
            _moveSequence = DOTween.Sequence()
                .Append(transform.DOMove(position, _moveDuration).SetEase(_moveEase));
        }
    }
}