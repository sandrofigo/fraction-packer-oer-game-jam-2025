using UnityEngine;

public class EqualsSolver : ISolver
{
    public bool GetSolution(Fraction[] fractions)
    {
        for (int i = 0; i < (fractions.Length - 1); i++)
            if (!(Mathf.Approximately(fractions[i].Value, fractions[i + 1].Value)))
                return false;

        return true;
    }
}
