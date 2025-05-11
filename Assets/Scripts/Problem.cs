using System.Collections.Generic;
using UnityEngine;

public struct Fraction
{
    public int Numerator;
    public int Denominator;
    public float Value => (Numerator / (float) Denominator);
    public bool IsValid => (Numerator > 0 && Denominator > 0);
    public bool IsEmpty => (Numerator == 0 && Denominator == 0);
}

public class Problem
{
    private ProblemType type;
    private int amountFractions;
    private int amountOperations;
    public Fraction[] Fractions { get; private set; } // parsed Data
    public OperatorType[] Operators { get; private set; } // parsed Data

    private ISolver solver;
    
    public Problem(ProblemType type, ParseData data, ISolver solver)
    {
        this.type = type;
        this.solver = solver;

        // parsed data
        amountFractions = data.amountFractions;
        Fractions = new Fraction[amountFractions];
        foreach(var fraction in data.presetFractions)
        {
            SetFraction(fraction.Value, fraction.Key);
        }

        amountOperations = data.operators.Count;
        Operators = new OperatorType[amountOperations];
        for(int i = 0; i < amountOperations; i++)
        {
            Operators.SetValue(data.operators[i], i);
        }
    }

    public void SetAllFractions(List<Fraction> newFractions)
    {
        Fractions = new Fraction[amountFractions];

        if(newFractions.Count == amountFractions)
        {
            for(int i = 0; i < amountFractions; i++)
            {
                Fractions[i] = newFractions[i];
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
            Fractions[idx] = fraction;
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

        outResult = solver.GetSolution(Fractions);
        return true;
    }

    private bool PreSolutionDataValidation()
    {
        if(Fractions.Length != amountFractions)
        {
            Debug.LogError("PreSolutionCheck FAILED: wrong amount of <Fractions>");
            return false;
        }

        if(Operators.Length != (amountFractions - 1))
        {
            Debug.LogError("PreSolutionCheck FAILED: wrong amount of <Operators>");
            return false;
        }

        foreach(var fraction in Fractions)
            if(!fraction.IsValid)
                return false;

        return true;
    }
    #endregion
}
