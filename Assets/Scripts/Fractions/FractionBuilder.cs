using Box;
using UnityEngine;
using Zenject;

namespace Fractions
{
    public class FractionBuilder : MonoBehaviour
    {
        [Inject]
        private readonly DiContainer _container;

        [SerializeField]
        private FullFractionBlock _fullFractionPrefab;

        [SerializeField]
        private PartialFractionBlock _partialFractionPrefab;

        [SerializeField]
        private FractionSlot _fractionSlotPrefab;

        public FullFractionBlock CreateFullFraction(int numerator, int denominator)
        {
            FullFractionBlock block = _container.InstantiatePrefabForComponent<FullFractionBlock>(_fullFractionPrefab);

            block.NumeratorFractionComponent.SetValue(numerator);
            block.DenominatorFractionComponent.SetValue(denominator);

            return block;
        }

        public PartialFractionBlock CreatePartialFraction(int value)
        {
            PartialFractionBlock block = _container.InstantiatePrefabForComponent<PartialFractionBlock>(_partialFractionPrefab);

            block.FractionComponent.SetValue(value);

            return block;
        }

        public void CreateSlot(Vector3 position)
        {
            FractionSlot slot = _container.InstantiatePrefabForComponent<FractionSlot>(_fractionSlotPrefab);
            
            slot.transform.position = position;
        }
    }
}