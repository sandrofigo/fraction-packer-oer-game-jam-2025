using System.Collections.Generic;

public interface IParser
{
    public bool TryParse(string genDataStr, ref ParseData parseData);
}

public static class ParseUtils
{
    public const char FRACTION_SERPARATOR = ';';
    private const char INNER_FRACTION_SERPARATOR = ',';
    public static bool TryParseFractions(string[] fractionStrings, ref ParseData parseData)
    {
        parseData.presetFractions = new Dictionary<int, Fraction>();

        for (int i = 0; i < fractionStrings.Length; i++)
        {
            string fractionString = fractionStrings[i];

            if (fractionString.IndexOf(INNER_FRACTION_SERPARATOR) != 1 || fractionString.Length != 3)
                return false;

            Fraction fraction = new Fraction();
            bool fractionIsEmpty = true;

            if (int.TryParse(fractionString[0].ToString(), out int numerator))
            {
                fractionIsEmpty = false;
                fraction.Numerator = numerator;
            }

            if (int.TryParse(fractionString[2].ToString(), out int denominator))
            {
                fractionIsEmpty = false;
                fraction.Denominator = denominator;
            }

            if (!fractionIsEmpty)
            {
                parseData.presetFractions.Add(i, fraction);
            }
        }

        return true;
    }
}
