namespace Fractions
{
    public interface IFraction
    {
        public bool IsValid { get; }
        public int Numerator { get; }
        public int Denominator { get; }
        public float GetFractionAsFloat();
    }
}