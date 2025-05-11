using System.Collections.Generic;

public class DescParser : IParser
{
    // examples
    // n,n;n,n;n,n       => NULL/NULL > NULL/NULL > NULL/NULL
    // n,2;n,n;n,n       => NULL/2    > NULL/NULL > NULL/NULL
    // 1,2;5,n;n,6       =>    1/2    >    5/NULL > NULL/6
    public bool TryParse(string genDataStr, ref ParseData parseData)
    {
        string[] fractionGroups = genDataStr.Split(ParseUtils.FRACTION_GROUP_SEPARATOR);
        if (fractionGroups.Length != 2)
            return false;

        // preset fractions for problem
        string[] presetFractionStrings = fractionGroups[0].Split(ParseUtils.FRACTION_SERPARATOR);
        parseData.amountFractions = presetFractionStrings.Length;

        if (parseData.amountFractions < 2)
            return false;

        if (!ParseUtils.TryParsePresetFractions(presetFractionStrings, ref parseData))
            return false;

        // fractions available to user
        string[] userFractionStrings = fractionGroups[1].Split(ParseUtils.FRACTION_SERPARATOR);
        if (!ParseUtils.TryParseUserFractions(userFractionStrings, ref parseData))
            return false;

        // operators
        parseData.operators = new List<OperatorType>();
        for (int i = 0; i < (parseData.amountFractions - 1); i++)
            parseData.operators.Add(OperatorType.Greater);

        return true;
    }
}
