using System;

public static class OperatorDisplayValue
{
    public static string GetDisplayValue(OperatorType type)
    {
        switch (type)
        {
            case OperatorType.Greater:
                return ">";
            case OperatorType.Less:
                return "<";
            case OperatorType.Equal:
                return "=";
            case OperatorType.GreaterOrEqual:
                return ">=";
            case OperatorType.LessOrEqual:
                return "<=";
            case OperatorType.Plus:
                return "+";
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}