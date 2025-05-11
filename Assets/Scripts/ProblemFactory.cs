using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public enum ProblemType
{
    None,
    ASC,            // frac < frac < frac ...
    DESC,           // frac > frac > frac ...
    PlusLesser,     // frac + frac < frac
    PlusGreater,    // frac + frac > frac
    Equals,         // frac = frac = frac ...
    PlusEquals,     // frac + frac = frac
}

public enum OperatorType
{
    Less,
    LessOrEqual,
    Equal,
    GreaterOrEqual,
    Greater,
    Plus
}

public struct ParseData
{
    public int amountFractions;
    public Dictionary<int, Fraction> presetFractions; // idx, fraction
    public List<OperatorType> operators;
    public List<Fraction> userFractions;
}


public class ProblemFactory : MonoBehaviour
{
    public static ProblemFactory Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private Dictionary<char, ProblemType> problemTypeTokens = new Dictionary<char, ProblemType>()
    {
        { 'A', ProblemType.ASC },
        { 'D', ProblemType.DESC },
        { 'L', ProblemType.PlusLesser },
        { 'G', ProblemType.PlusGreater },
        { 'E', ProblemType.Equals },
        { 'P', ProblemType.PlusEquals },
    };

    public List<char> ProblemTypeTokens => problemTypeTokens.Keys.ToList();

    private string genDataString;

    public Problem CreateProblem(string data)
    {
        genDataString = data;
        ProblemType type = ExtractProblemType();
        IParser parser = null;
        ParseData parseData = new ParseData();
        ISolver solver = null;
        
        switch (type)
        {
            case ProblemType.ASC:
                parser = new AscParser();
                solver = new AscSolver();
                break;
            default:
                break;
        }
        
        if(parser == null)
        {
            Debug.LogWarning("No parser seleceted, aborting...");
            return null;
        }
        else if(!parser.TryParse(genDataString, ref parseData))
        {
            Debug.LogWarning("Parser failed, aborting...");
            return null;
        }
        
        return new Problem(type, parseData, solver);
    }

    private ProblemType ExtractProblemType()
    {
        ProblemType type = ProblemType.None;

        try
        {
            char token = genDataString[0];

            if (problemTypeTokens.ContainsKey(token))
            {
                type = problemTypeTokens[token];
            }

            genDataString = genDataString.Substring(1);
        }
        catch (IndexOutOfRangeException ex)
        {
            Debug.LogError(ex.Message);
        }

        return type;
    }
}
