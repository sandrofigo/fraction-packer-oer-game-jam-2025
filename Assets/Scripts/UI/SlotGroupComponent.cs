using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class SlotGroupComponent : MonoBehaviour
    {
        [Inject]
        private readonly DiContainer _diContainer;

        private List<SlotComponent> _slots = new();

        [SerializeField] private RectTransform _container;
        [SerializeField] private GameObject _slotPrefab;
        [SerializeField] private GameObject _slotSymbolPrefab;
        [SerializeField] private CanvasGroup _canvasGroup;

        private Tween _fadeTween;

        private void Awake()
        {
            _canvasGroup.alpha = 0;
            ClearSlots();
        }

        public void ClearSlots()
        {
            foreach (Transform child in _container.transform)
            {
                Destroy(child.gameObject);
            }

            _slots.Clear();
        }

        public SlotComponent SpawnSlot(int? topValue, int? bottomValue)
        {
            _fadeTween.Kill();
            _fadeTween = _canvasGroup.DOFade(1, 0.35f).SetUpdate(true).SetLink(gameObject);

            var component = _diContainer.InstantiatePrefabForComponent<SlotComponent>(_slotPrefab, _container);
            component.SetValueTop(topValue.HasValue ? topValue.Value.ToString() : "");
            component.SetValueBottom(bottomValue.HasValue ? bottomValue.Value.ToString() : "");

            LayoutRebuilder.ForceRebuildLayoutImmediate(_container);

            return component;
        }

        public SlotSymbolComponent SpawnSlotSymbol(string symbol)
        {
            var component = _diContainer.InstantiatePrefabForComponent<SlotSymbolComponent>(_slotSymbolPrefab, _container);
            component.SetText(symbol);
            return component;
        }

        public SlotComponent[] GetSlots()
        {
            return _slots.ToArray();
        }
    }
}