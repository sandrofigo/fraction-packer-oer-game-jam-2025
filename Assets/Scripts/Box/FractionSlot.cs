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

        public void Put(AFractionBlock block, Transform slot, SlotPlacementType placementType)
        {
            BlockType blockType = block.IsFull ? BlockType.Full : slot == _numeratorSlot ? BlockType.Numerator : BlockType.Denominator;

            if (_content.ForType(blockType) != block && !HasRoom(blockType))
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

            _content.Set(block, blockType);

            if (placementType == SlotPlacementType.Place)
            {
                Vector3 position = blockType switch
                {
                    BlockType.Numerator => _numeratorSlot.position,
                    BlockType.Denominator => _denominatorSlot.position,
                    _ => _fullSlot.position
                };

                block.MoveTo(position, true);

                _lastContent = new SlotContent();
            }
        }

        public void SetToLastContent()
        {
            _content = _lastContent;
            _lastContent = new SlotContent();

            if (_content.Numerator) Put(_content.Numerator, _numeratorSlot, SlotPlacementType.Place);
            if (_content.Denominator) Put(_content.Denominator, _denominatorSlot, SlotPlacementType.Place);
            if (_content.FullFraction) Put(_content.FullFraction, _fullSlot, SlotPlacementType.Place);
        }

        public void Remove(AFractionBlock block, Transform slot)
        {
            BlockType blockType = block.IsFull ? BlockType.Full : slot == _numeratorSlot ? BlockType.Numerator : BlockType.Denominator;

            switch (blockType)
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
            if (_content.FullFraction) return false;

            if (blockType == BlockType.Full) return !_content.Numerator && !_content.Denominator;

            return !_content.ForType(blockType);
        }
    }
}