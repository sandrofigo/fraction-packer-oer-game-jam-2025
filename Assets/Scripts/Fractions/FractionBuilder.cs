using System.Collections.Generic;
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
        private Vector3 _spawnCenterPosition = new Vector3(0, 0.13f, 0);

        [SerializeField]
        private Vector3 _maxRandomOffset = new(0.25f, 0.25f, 0f);

        [SerializeField]
        private float _maxRandomRotationAngle = 30f;

        [SerializeField]
        private float _gap = 0.5f;

        [SerializeField]
        private float _animationDelay = 0.1f;

        private int _spawnAmount;

        private List<AFractionBlock> _spawnedBlocks = new();
        private List<FractionSlot> _spawnedSlots = new();
        
        public IReadOnlyList<FractionSlot> SpawnedSlots => _spawnedSlots;

        public void Clear()
        {
            foreach (var block in _spawnedBlocks)
            {
                Destroy(block.gameObject);
            }

            foreach (var slot in _spawnedSlots)
            {
                Destroy(slot.gameObject);
            }
            
            _spawnedBlocks.Clear();
            _spawnedSlots.Clear();
            _spawnAmount = 0;
        }

        public FullFractionBlock CreateFullFraction(int numerator, int denominator, int problemFractionAmount)
        {
            FullFractionBlock block = _container.InstantiatePrefabForComponent<FullFractionBlock>(_fullFractionPrefab);

            block.NumeratorFractionComponent.SetValue(numerator);
            block.DenominatorFractionComponent.SetValue(denominator);

            SetupFraction(block, problemFractionAmount);

            return block;
        }

        public PartialFractionBlock CreatePartialFraction(int value, int problemFractionAmount)
        {
            PartialFractionBlock block = _container.InstantiatePrefabForComponent<PartialFractionBlock>(_partialFractionPrefab);

            block.FractionComponent.SetValue(value);

            SetupFraction(block, problemFractionAmount);

            return block;
        }

        public FractionSlot CreateSlot(Vector3 position, bool isNumeratorAvailable, bool isDenominatorAvailable)
        {
            FractionSlot slot = _container.InstantiatePrefabForComponent<FractionSlot>(_fractionSlotPrefab);

            slot.Setup(isNumeratorAvailable, isDenominatorAvailable);

            slot.transform.position = position;
            
            _spawnedSlots.Add(slot);

            return slot;
        }

        private void SetupFraction(AFractionBlock block, int fractionAmount)
        {
            block.transform.position = _animationStartPosition;
            block.transform.rotation = Quaternion.Euler(_animationStartRotation);

            Vector3 targetRotation = Vector3.up * Random.Range(-_maxRandomRotationAngle, _maxRandomRotationAngle);
            Vector3 targetPosition = GetTargetPosition(fractionAmount);
            block.MoveAndRotateTo(targetPosition, targetRotation, _spawnAmount * _animationDelay);

            block.SetInitialPosition(targetPosition, targetRotation);

            _spawnAmount++;
            _spawnedBlocks.Add(block);
        }

        private Vector3 GetTargetPosition(int fractionAmount)
        {
            float width = _partialFractionPrefab.GetComponentInChildren<MeshRenderer>().bounds.size.x;

            Vector3 centerTargetPosition = GetSpawnStartPosition(fractionAmount) + Vector3.right * (width * _spawnAmount + GetGapSize(fractionAmount) * _spawnAmount);

            Vector3 randomOffset = new Vector3(Random.Range(0, _maxRandomOffset.x), 0, Random.Range(-_maxRandomOffset.z, _maxRandomOffset.z));

            return centerTargetPosition + randomOffset;
        }

        private Vector3 GetSpawnStartPosition(int fractionAmount)
        {
            float width = _partialFractionPrefab.GetComponentInChildren<MeshRenderer>().bounds.size.x;
            float offset = fractionAmount / 2f - 0.5f;
            return _spawnCenterPosition + Vector3.left * (offset * width + offset * GetGapSize(fractionAmount));
        }

        private float GetGapSize(int fractionAmount)
        {
            return fractionAmount < 6 ? _gap : Mathf.Lerp(_gap, 0, (fractionAmount - 5) / 5f);
        }
    }
}