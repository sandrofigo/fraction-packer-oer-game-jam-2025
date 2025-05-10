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

        private Vector3 _initialPosition;

        private void Start()
        {
            _initialPosition = transform.position;
        }

        public void PutIntoSlot(FractionSlot slot, SlotPlacementType placementType)
        {
            slot.Place(this, placementType, out var position);
            MoveTo(position);
        }

        public void ResetPosition()
        {
            MoveTo(_initialPosition);
        }

        public void MoveTo(Vector3 position)
        {
            _animationController.MoveTo(position);
        }
    }
}