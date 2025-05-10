using System.Collections.Generic;

public class PlusLesserParser
{
    // examples
    // n,n;n,n;n,n       => NULL/NULL + NULL/NULL < NULL/NULL
    // n,2;n,n;n,n       => NULL/2    + NULL/NULL < NULL/NULL
    // 1,2;5,n;n,6       =>    1/2    +    5/NULL < NULL/6
    public bool TryParse(string genDataStr, ref ParseData parseData)
    {
        string[] fractionStrings = genDataStr.Split(ParseUtils.FRACTION_SERPARATOR);
        parseData.amountFractions = fractionStrings.Length;

        if (parseData.amountFractions < 3)
            return false;

        if (!ParseUtils.TryParseFractions(fractionStrings, ref parseData))
            return false;

        parseData.operators = new List<OperatorType>();
        for (int i = 0; i < (parseData.amountFractions - 2); i++)
            parseData.operators.Add(OperatorType.Plus);

        parseData.operators.Add(OperatorType.Less);

        return true;
    }
}
