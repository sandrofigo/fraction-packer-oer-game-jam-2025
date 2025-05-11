using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Fractions;
using UI;
using UnityEngine;
using Zenject;

public class LevelBuilder : MonoBehaviour
{
    [Inject]
    private readonly FractionBuilder _fractionBuilder;

    [Inject]
    private readonly SlotGroupComponent _slotGroup;
    
    [Inject]
    private readonly GameManager _gameManager;

    private readonly ProblemFactory _factory = new();

    private readonly List<(Fraction, SlotComponent)> _fractionSlotPairs = new();

    private void Awake()
    {
        _gameManager.GameStartEvent += SpawnFirstProblem;
    }

    private void SpawnFirstProblem()
    {
        BuilderProblem("An,n;3,n;n,7");
    }

    public void BuilderProblem(string problemString)
    {
        Problem problem = _factory.CreateProblem(problemString);

        _slotGroup.ClearSlots();
        _fractionSlotPairs.Clear();

        for (var i = 0; i < problem.Fractions.Length; i++)
        {
            Fraction fraction = problem.Fractions[i];

            // slot
            SlotComponent slotComponent = _slotGroup.SpawnSlot(fraction.Numerator == 0 ? null : fraction.Numerator, fraction.Denominator == 0 ? null : fraction.Denominator);
            _fractionSlotPairs.Add((fraction, slotComponent));

            // fraction block
            if (fraction is { Numerator: 0, Denominator: 0 })
            {
                _fractionBuilder.CreateFullFraction(fraction.Numerator, fraction.Denominator);
            }
            else
            {
                _fractionBuilder.CreatePartialFraction(fraction.Numerator == 0 ? fraction.Numerator : fraction.Denominator);
            }

            // operator
            if (i < problem.Operators.Length)
            {
                _slotGroup.SpawnSlotSymbol(OperatorDisplayValue.GetDisplayValue(problem.Operators[i]));
            }
        }

        StopAllCoroutines();
        
        StartCoroutine(SpawnSlots());
    }

    private IEnumerator SpawnSlots()
    {
        yield return null;
        
        // slots
        foreach ((Fraction, SlotComponent) pair in _fractionSlotPairs)
        {
            _fractionBuilder.CreateSlot(pair.Item2.GetComponent<RectTransform>().position, pair.Item1.Numerator == 0, pair.Item1.Denominator == 0);
        }
    }

    // private void Update()
    // {
    //     if (fractionSlotPairs.Count > 0)
    //         Debug.Log(fractionSlotPairs.First().Item2.GetComponent<RectTransform>().position);
    // }
}