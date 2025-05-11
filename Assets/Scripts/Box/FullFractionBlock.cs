using Fractions;
using UnityEngine;

namespace Box
{
    public class FullFractionBlock : AFractionBlock
    {
        [SerializeField]
        private FractionComponent _numeratorFractionComponent;

        public FractionComponent NumeratorFractionComponent => _numeratorFractionComponent;

        [SerializeField]
        private FractionComponent _denominatorFractionComponent;

        public FractionComponent DenominatorFractionComponent => _denominatorFractionComponent;

        public int Numerator => _numeratorFractionComponent.Value;
        public int Denominator => _denominatorFractionComponent.Value;
    }
}