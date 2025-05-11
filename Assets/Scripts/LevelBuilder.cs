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

    readonly ProblemFactory _factory = new();

    private void Start()
    {
        BuilderProblem("An,n;3,n;n,7");
    }

    public void BuilderProblem(string problemString)
    {
        Problem problem = _factory.CreateProblem(problemString);
        
        _slotGroup.ClearSlots();

        for (var i = 0; i < problem.Fractions.Length; i++)
        {
            var fraction = problem.Fractions[i];

            // slot
            var slotComponent = _slotGroup.SpawnSlot(fraction.Numerator == 0 ? null : fraction.Numerator, fraction.Denominator == 0 ? null : fraction.Denominator);
            _fractionBuilder.CreateSlot(slotComponent.transform.position);

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
    }
}