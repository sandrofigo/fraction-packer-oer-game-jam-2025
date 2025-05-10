using System;
using Fractions;
using UnityEngine;

namespace Box
{
    public abstract class AFractionBlock : MonoBehaviour
    {
        [SerializeField]
        private FractionBlockAnimationController _animationController;

        [SerializeField]
        private BlockType _blockType;

        public BlockType BlockType => _blockType;

        public FractionSlot Slot { get; set; }

        private Vector3 _initialPosition;

        private void Start()
        {
            _initialPosition = transform.position;
        }

        public void ResetPosition()
        {
            MoveTo(_initialPosition, true);
        }

        public void MoveTo(Vector3 position, bool animate)
        {
            if (animate)
                _animationController.MoveTo(position);
            else
                transform.position = position;
        }

        public void SetHover(bool isHovered)
        {
            _animationController.SetHover(isHovered);
        }

        public void Select()
        {
            _animationController.Select();
        }

        public void DoInvalidShake()
        {
            _animationController.DoInvalidShakeAnimation();
        }
    }
}