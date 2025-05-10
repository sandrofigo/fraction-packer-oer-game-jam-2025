using Fractions;

namespace Box
{
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

        public void Set(AFractionBlock block)
        {
            switch (block.BlockType)
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