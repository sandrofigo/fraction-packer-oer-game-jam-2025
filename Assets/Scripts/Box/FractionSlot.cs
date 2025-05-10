using System;
using Fractions;
using JetBrains.Annotations;
using UnityEngine;

namespace Box
{
    public enum SlotPlacementType
    {
        None,
        Hover,
        Place,
    }

    public class FractionSlot : MonoBehaviour, IFraction
    {
        [SerializeField]
        private Transform _numeratorSlot;

        [SerializeField]
        private Transform _denominatorSlot;

        [SerializeField]
        private Transform _fullSlot;

        private SlotContent _content;
        private SlotContent _lastContent;

        public bool IsValid => _content.FullFraction || (_content.Numerator && _content.Denominator);

        public int GetNumerator()
        {
            if (!IsValid)
            {
                return -1;
            }

            return _content.Numerator ? _content.Numerator.Value : _content.FullFraction.Numerator;
        }

        public int GetDenominator()
        {
            if (!IsValid)
            {
                return -1;
            }

            return _content.Denominator ? _content.Denominator.Value : _content.FullFraction.Denominator;
        }

        public float GetFractionAsFloat()
        {
            return IsValid ? GetNumerator() / (float)GetDenominator() : -1;
        }

        public void Place(AFractionBlock block, SlotPlacementType placementType, out Vector3 position)
        {
            position = transform.TransformPoint(block.BlockType switch
            {
                BlockType.Numerator => _numeratorSlot.position,
                BlockType.Denominator => _denominatorSlot.position,
                _ => _fullSlot.position
            });

            if (!HasRoom(block.BlockType))
            {
                _content.Numerator?.ResetPosition();
                _content.Denominator?.ResetPosition();
                _content.FullFraction?.ResetPosition();

                if (placementType == SlotPlacementType.Hover)
                {
                    _lastContent = _content;
                }

                _content = new SlotContent();
            }

            _content.Set(block);
        }

        private bool HasRoom(BlockType blockType)
        {
            return !_content.FullFraction && !_content.ForType(blockType);
        }
    }
}