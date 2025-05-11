using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Box;
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

    private readonly List<(Fraction, SlotComponent)> _fractionSlotPairs = new();

    private Problem _activeProblem;
    private List<Fraction> _startFractions = new();

    private void Awake()
    {
        _gameManager.GameStartEvent += Restart;
    }

    private void Restart()
    {
        foreach (var slot in _fractionBuilder.SpawnedSlots)
        {
            slot.FractionBlockPlaced -= SlotOnFractionBlockPlaced;
        }

        _slotGroup.ClearSlots();
        _fractionBuilder.Clear();

        BuilderProblem(ProblemDataGenerator.Instance.GetNextProblem());
    }

    public void BuilderProblem(string problemString)
    {
        _activeProblem = ProblemFactory.Instance.CreateProblem(problemString);
        _startFractions = _activeProblem.Fractions.ToList();

        _slotGroup.ClearSlots();
        _fractionSlotPairs.Clear();

        for (var i = 0; i < _activeProblem.Fractions.Length; i++)
        {
            Fraction fraction = _activeProblem.Fractions[i];

            // slot
            SlotComponent slotComponent = _slotGroup.SpawnSlot(fraction.Numerator == 0 ? null : fraction.Numerator, fraction.Denominator == 0 ? null : fraction.Denominator);
            _fractionSlotPairs.Add((fraction, slotComponent));

            // operator
            if (i < _activeProblem.Operators.Length)
            {
                _slotGroup.SpawnSlotSymbol(OperatorDisplayValue.GetDisplayValue(_activeProblem.Operators[i]));
            }
        }

        for (int i = 0; i < _activeProblem.UserFractions.Count; i++)
        {
            Fraction fraction = _activeProblem.UserFractions[i];

            // fraction block
            if (fraction.Numerator != 0 && fraction.Denominator != 0)
            {
                _fractionBuilder.CreateFullFraction(fraction.Numerator, fraction.Denominator, _activeProblem.UserFractions.Count);
            }
            else
            {
                _fractionBuilder.CreatePartialFraction(fraction.Numerator != 0 ? fraction.Numerator : fraction.Denominator, _activeProblem.UserFractions.Count);
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
            var slot = _fractionBuilder.CreateSlot(pair.Item2.GetComponent<RectTransform>().position, pair.Item1.Numerator == 0, pair.Item1.Denominator == 0);
            slot.FractionBlockPlaced += SlotOnFractionBlockPlaced;
        }
    }

    private void SlotOnFractionBlockPlaced()
    {
        List<Fraction> fractions = new();

        for (var i = 0; i < _fractionBuilder.SpawnedSlots.Count; i++)
        {
            Fraction fraction = _startFractions[i];

            var slot = _fractionBuilder.SpawnedSlots[i];

            if (slot.GetNumerator() != 0)
            {
                fraction.Numerator = slot.GetNumerator();
            }

            if (slot.GetDenominator() != 0)
            {
                fraction.Denominator = slot.GetDenominator();
            }

            fractions.Add(fraction);
        }

        _activeProblem.SetAllFractions(fractions);

        bool result = false;
        bool isValid = _activeProblem.TryGetSolution(ref result);

        if (!isValid)
        {
            return;
        }

        if (!result)
        {
            foreach (var slot in _fractionBuilder.SpawnedSlots)
            {
                slot.DoInvalidShakeAnimation();
            }
        }
        else
        {
            _gameManager.RestartGame();
        }
    }

    // private void Update()
    // {
    //     if (fractionSlotPairs.Count > 0)
    //         Debug.Log(fractionSlotPairs.First().Item2.GetComponent<RectTransform>().position);
    // }
}