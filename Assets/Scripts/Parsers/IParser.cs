using System.Collections.Generic;

public interface IParser
{
    public bool TryParse(string genDataStr, ref ParseData parseData);
}

public static class ParseUtils
{
    public const char FRACTION_GROUP_SEPARATOR = '-';
    public const char FRACTION_SERPARATOR = ';';
    private const char INNER_FRACTION_SERPARATOR = ',';

    public static bool TryParsePresetFractions(string[] fractionStrings, ref ParseData parseData)
    {
        parseData.presetFractions = new Dictionary<int, Fraction>();

        for (int i = 0; i < fractionStrings.Length; i++)
        {
            Fraction fraction = new Fraction();

            if (!TryParseSingleFraction(fractionStrings[i], ref fraction))
                return false;

            // empty fractions do not get added to preset fractions
            if (!fraction.IsEmpty)
                parseData.presetFractions.Add(i, fraction);
        }

        return true;
    }

    public static bool TryParseUserFractions(string[] fractionStrings, ref ParseData parseData)
    {
        parseData.userFractions = new List<Fraction>();

        for (int i = 0; i < fractionStrings.Length; i++)
        {
            Fraction fraction = new Fraction();

            if (!TryParseSingleFraction(fractionStrings[i], ref fraction))
                return false;

            // at least 1 value must be set for user fractions
            if (fraction.IsEmpty)
                return false;

            parseData.userFractions.Add(fraction);
        }

        return true;
    }

    private static bool TryParseSingleFraction(string fractionString, ref Fraction fraction)
    {
        if (fractionString.IndexOf(INNER_FRACTION_SERPARATOR) != 1 || fractionString.Length != 3)
            return false;

        if (int.TryParse(fractionString[0].ToString(), out int numerator))
            fraction.Numerator = numerator;

        if (int.TryParse(fractionString[2].ToString(), out int denominator))
            fraction.Denominator = denominator;

        return true;
    }
}
