namespace Fractions
{
    public interface IFraction
    {
        public bool IsValid { get; }
        public int GetNumerator();
        public int GetDenominator();
        public float GetFractionAsFloat();
    }
}