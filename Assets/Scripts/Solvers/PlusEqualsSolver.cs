using UnityEngine;

public class PlusEqualsSolver : ISolver
{
    public bool GetSolution(Fraction[] fractions)
    {
        float valueSum = 0;
        for (int i = 0; i < fractions.Length - 1; i++)
        {
            valueSum += fractions[i].Value;
        }
        return Mathf.Approximately(valueSum, fractions[fractions.Length - 1].Value);
    }
}

