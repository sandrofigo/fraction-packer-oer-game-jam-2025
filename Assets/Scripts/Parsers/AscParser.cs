using System.Collections.Generic;

public class AscParser : IParser
{
    private const char FRACTION_SEPARATOR = ';';
    private const char INNER_FRACTION_SERPARATOR = ',';
    // examples
    // n,n;n,n;n,n       => NULL/NULL < NULL/NULL < NULL/NULL
    // n,2;n,n;n,n       => NULL/2    < NULL/NULL < NULL/NULL
    // 1,2;5,n;n,6       =>    1/2    <    5/NULL < NULL/6
    public bool TryParse(string genDataStr, ref ParseData parseData)
    {
        string[] fractionStrings = genDataStr.Split(FRACTION_SEPARATOR);
        parseData.amountFractions = fractionStrings.Length;

        if (parseData.amountFractions < 2)
            return false;

        parseData.presetFractions = new Dictionary<int, Fraction>();

        for(int i = 0; i < fractionStrings.Length; i++)
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

            if(!fractionIsEmpty)
            {
                parseData.presetFractions.Add(i, fraction);
            }
        }

        // add operators: Less Less => < <
        parseData.operators = new List<OperatorType>();
        parseData.operators.Add(OperatorType.Less);
        parseData.operators.Add(OperatorType.Less);

        return true;
    }
}
