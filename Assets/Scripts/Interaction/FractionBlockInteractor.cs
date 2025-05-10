using Box;
using Fractions;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Interaction
{
    public class FractionBlockInteractor : MonoBehaviour
    {
        [SerializeField]
        private LayerMask _partialSlotLayer;

        [SerializeField]
        private LayerMask _fullSlotLayer;

        [SerializeField]
        private LayerMask _fractionBlockLayer;

        [Inject]
        private readonly ControlsManager _controlsManager;

        private AFractionBlock _hoveredBlock;
        private AFractionBlock _selectedBlock;

        private FractionSlot _hoveredSlot;
        private Transform _hoveredSlotPart;

        private Camera _camera;

        private Plane _groundPlane = new Plane(Vector3.up, Vector3.zero);

        private void Awake()
        {
            _camera = Camera.main;
            _controlsManager.Interact += OnInteract;
        }

        private void OnDestroy()
        {
            _controlsManager.Interact -= OnInteract;
        }

        private void Update()
        {
            if (!_selectedBlock)
            {
                UpdateHover();
            }
            else
            {
                _selectedBlock.MoveTo(GetMousePositionOnGroundPlane(), false);
                UpdateSlotSelection();
            }
        }

        private void OnInteract(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                if (!_hoveredBlock) return;

                _selectedBlock = _hoveredBlock;
                _hoveredBlock.Select();
            }
            else if (context.canceled)
            {
                if (!_selectedBlock) return;

                if (_hoveredSlot)
                {
                    _hoveredSlot.Put(_selectedBlock, _hoveredSlotPart, SlotPlacementType.Place);
                }
                else
                {
                    _selectedBlock.Slot.Item1?.Remove(_selectedBlock, _selectedBlock.Slot.Item2);
                    _selectedBlock.ResetPosition();
                }

                _selectedBlock = null;
                _hoveredSlot = null;
                _hoveredSlotPart = null;
            }
        }

        private void UpdateHover()
        {
            if (Physics.Raycast(_camera.ScreenPointToRay(_controlsManager.PointerPosition), out var hitInfo, Mathf.Infinity, _fractionBlockLayer))
            {
                var hoveredBlock = hitInfo.collider.GetComponentInParent<AFractionBlock>();

                if (_hoveredBlock != hoveredBlock)
                {
                    _hoveredBlock?.SetHover(false);
                    _hoveredBlock = hoveredBlock;
                    _hoveredBlock.SetHover(true);
                }
            }
            else if (_hoveredBlock)
            {
                _hoveredBlock.SetHover(false);
                _hoveredBlock = null;
            }
        }

        private void UpdateSlotSelection()
        {
            var layer = _selectedBlock.IsFull ? _fullSlotLayer : _partialSlotLayer;

            if (Physics.Raycast(_camera.ScreenPointToRay(_controlsManager.PointerPosition), out var hitInfo, Mathf.Infinity, layer))
            {
                if (hitInfo.collider.transform != _hoveredSlotPart)
                {
                    _hoveredSlot?.Remove(_selectedBlock, _hoveredSlotPart);
                }

                _hoveredSlotPart = hitInfo.collider.transform;

                _hoveredSlot = hitInfo.collider.GetComponentInParent<FractionSlot>();
                _hoveredSlot.Put(_selectedBlock, _hoveredSlotPart, SlotPlacementType.Hover);
            }
            else if (_hoveredSlot)
            {
                _hoveredSlot.SetToLastContent();
                _hoveredSlot = null;
                _hoveredSlotPart = null;
            }
        }

        private Vector3 GetMousePositionOnGroundPlane()
        {
            Ray ray = _camera.ScreenPointToRay(_controlsManager.PointerPosition);

            _groundPlane.Raycast(_camera.ScreenPointToRay(_controlsManager.PointerPosition), out var hitDistance);

            return ray.GetPoint(hitDistance);
        }
    }
}