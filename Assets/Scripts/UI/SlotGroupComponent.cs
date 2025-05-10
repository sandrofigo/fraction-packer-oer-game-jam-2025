using UnityEngine;
using Zenject;

namespace UI
{
    public class SlotGroupComponent : MonoBehaviour
    {
        [Inject]
        private readonly DiContainer _diContainer;

        [SerializeField] private SlotComponent[] _slots;

        [SerializeField] private RectTransform _container;
        [SerializeField] private GameObject _slotPrefab;
        [SerializeField] private GameObject _slotSymbolPrefab;

        public void ClearSlots()
        {
            foreach (Transform child in _container.transform)
            {
                Destroy(child.gameObject);
            }
        }

        public SlotComponent SpawnSlot(int topValue, int bottomValue)
        {
            var component = _diContainer.InstantiatePrefabForComponent<SlotComponent>(_slotPrefab, _container);
            component.SetValueTop(topValue);
            component.SetValueTop(bottomValue);
            return component;
        }

        public SlotSymbolComponent SpawnSlotSymbol(string symbol)
        {
            var component = _diContainer.InstantiatePrefabForComponent<SlotSymbolComponent>(_slotSymbolPrefab, _container);
            component.SetText(symbol);
            return component;
        }
    }
}