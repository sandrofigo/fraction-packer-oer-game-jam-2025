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

        [SerializeField]
        private Vector3 _animationStartPosition;
        
        [SerializeField]
        private Vector3 _animationStartRotation;

        [SerializeField]
        private Vector3 _spawnStartPosition;

        [SerializeField]
        private Vector3 _maxRandomOffset = new(0.25f, 0.25f, 0f);

        [SerializeField]
        private float _maxRandomRotationAngle = 30f;

        [SerializeField]
        private float _gap = 0.5f;

        [SerializeField]
        private float _animationDelay = 0.1f;

        private int _spawnAmount;

        public FullFractionBlock CreateFullFraction(int numerator, int denominator)
        {
            FullFractionBlock block = _container.InstantiatePrefabForComponent<FullFractionBlock>(_fullFractionPrefab);

            block.NumeratorFractionComponent.SetValue(numerator);
            block.DenominatorFractionComponent.SetValue(denominator);

            SetupFraction(block);

            return block;
        }

        public PartialFractionBlock CreatePartialFraction(int value)
        {
            PartialFractionBlock block = _container.InstantiatePrefabForComponent<PartialFractionBlock>(_partialFractionPrefab);

            block.FractionComponent.SetValue(value);

            SetupFraction(block);

            return block;
        }

        public void CreateSlot(Vector3 position)
        {
            FractionSlot slot = _container.InstantiatePrefabForComponent<FractionSlot>(_fractionSlotPrefab);

            slot.transform.position = position;
        }

        private void SetupFraction(AFractionBlock block)
        {
            block.transform.position = _animationStartPosition;
            block.transform.rotation = Quaternion.Euler(_animationStartRotation);

            Vector3 targetRotation = Vector3.up * Random.Range(-_maxRandomRotationAngle, _maxRandomRotationAngle);
            block.MoveAndRotateTo(GetTargetPosition(), targetRotation, _spawnAmount * _animationDelay);

            _spawnAmount++;
        }

        private Vector3 GetTargetPosition()
        {
            float width = _partialFractionPrefab.GetComponentInChildren<MeshRenderer>().bounds.size.x;

            Vector3 centerTargetPosition = _spawnStartPosition + Vector3.right * (width * _spawnAmount + _gap * _spawnAmount);

            Vector3 randomOffset = new Vector3(Random.Range(0, _maxRandomOffset.x), 0, Random.Range(-_maxRandomOffset.z, _maxRandomOffset.z));

            return centerTargetPosition + randomOffset;
        }
    }
}