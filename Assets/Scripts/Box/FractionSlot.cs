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

        public void Put(AFractionBlock block, SlotPlacementType placementType)
        {
            Vector3 position = transform.TransformPoint(block.BlockType switch
            {
                BlockType.Numerator => _numeratorSlot.position,
                BlockType.Denominator => _denominatorSlot.position,
                _ => _fullSlot.position
            });
            
            block.MoveTo(position, true);

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

        public void SetToLastContent()
        {
            _content.Numerator?.ResetPosition();
            _content.Denominator?.ResetPosition();
            _content.FullFraction?.ResetPosition();
            
            _content = _lastContent;
            _lastContent = new SlotContent();
            
            if (_content.Numerator) Put(_content.Numerator, SlotPlacementType.Place);
            if (_content.Denominator) Put(_content.Denominator, SlotPlacementType.Place);
            if (_content.FullFraction) Put(_content.FullFraction, SlotPlacementType.Place);
        }

        public void Remove(AFractionBlock block)
        {
            switch (block.BlockType)
            {
                case BlockType.Numerator:
                    _content.Numerator = null;
                    break;
                case BlockType.Denominator:
                    _content.Denominator = null;
                    break;
                case BlockType.Full:
                    _content.FullFraction = null;
                    break;
            }
        }

        private bool HasRoom(BlockType blockType)
        {
            return !_content.FullFraction && !_content.ForType(blockType);
        }
    }
}