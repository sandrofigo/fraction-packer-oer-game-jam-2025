using Fractions;
using UnityEngine;

namespace Box
{
    public class PartialFractionBlock : AFractionBlock
    {
        [SerializeField]
        private FractionComponent _fractionComponent;

        public FractionComponent FractionComponent => _fractionComponent;

        public int Value => _fractionComponent.Value;
    }
}