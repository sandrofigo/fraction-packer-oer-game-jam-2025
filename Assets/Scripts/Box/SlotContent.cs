using System;
using Fractions;

namespace Box
{
    [Serializable]
    public struct SlotContent
    {
        public PartialFractionBlock Numerator;
        public PartialFractionBlock Denominator;
        public FullFractionBlock FullFraction;
        
        public bool HasContent => Numerator || Denominator || FullFraction;

        public AFractionBlock ForType(BlockType type)
        {
            switch (type)
            {
                case BlockType.Numerator:
                    return Numerator;
                case BlockType.Denominator:
                    return Denominator;
                case BlockType.Full:
                    return FullFraction;
                default:
                    return null;
            }
        }

        public void Set(AFractionBlock block, BlockType blockType)
        {
            switch (blockType)
            {
                case BlockType.Numerator:
                    Numerator = (PartialFractionBlock) block;
                    break;
                case BlockType.Denominator:
                    Denominator = (PartialFractionBlock) block;
                    break;
                case BlockType.Full:
                    FullFraction = (FullFractionBlock) block;
                    break;
            }
        }
    }
}