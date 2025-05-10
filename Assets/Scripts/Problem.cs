using System.Collections.Generic;
using UnityEngine;

public class Fraction
{

}

public class Problem
{
    private ProblemType type;
    private int amountFractions;
    private int amountOperations;
    private Fraction[] fractions; // parsed Data
    private OperatorType[] operators; // parsed Data

    private ISolver solver;

    public Problem(ProblemType type, ParseData data, ISolver solver)
    {
        this.type = type;
        this.solver = solver;

        // parsed data
        amountFractions = data.amountFractions;
        fractions = new Fraction[amountFractions];
        foreach(var fraction in data.presetFractions)
        {
            SetFraction(fraction.Value, fraction.Key);
        }

        amountOperations = data.operators.Count;
        operators = new OperatorType[amountOperations];
        for(int i = 0; i < amountOperations; i++)
        {
            operators.SetValue(data.operators[i], i);
        }
    }

    public void SetAllFractions(List<Fraction> newFractions)
    {
        fractions = new Fraction[amountFractions];

        if(newFractions.Count == amountFractions)
        {
            for(int i = 0; i < amountFractions; i++)
            {
                fractions[i] = newFractions[i];
            }
        }
        else
        {
            Debug.LogWarning("Expected <" + amountFractions + "> fractions. Got <" + newFractions.Count + "> instead." );
        }
    }
    public void SetFraction(Fraction fraction, int idx)
    {
        if (idx >= 0 && idx < amountFractions)
        {
            fractions[idx] = fraction;
        }
        else
        {
            Debug.LogWarning("Index <" + idx + "> is out of bounds <0," + amountFractions + ">");
        }
    }

    #region Solver
    protected bool TryGetSolution(ref bool outResult)
    {
        outResult = false;

        if (!PreSolutionDataValidation())
            return false;

        outResult = solver.GetSolution();
        return true;
    }

    private bool PreSolutionDataValidation()
    {
        if (fractions.Length != amountFractions)
        {
            Debug.LogError("PreSolutionCheck FAILED: wrong amount of <Fractions>");
            return false;
        }

        if (operators.Length != (amountFractions - 1))
        {
            Debug.LogError("PreSolutionCheck FAILED: wrong amount of <Operators>");
            return false;
        }

        return true;
    }
    #endregion
}
