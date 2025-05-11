using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Interaction
{
    public class ControlsManager : Controls.IInteractionActions, IDisposable
    {
        public Vector3 PointerPosition => Mouse.current.position.ReadValue();
        
        public event Action<InputAction.CallbackContext> Interact;
        public event Action<InputAction.CallbackContext> AnyKey;
        public event Action<InputAction.CallbackContext> Restart;

        private readonly Controls _controls;

        public ControlsManager()
        {
            _controls = new Controls();

            _controls.Interaction.SetCallbacks(this);

            _controls.Enable();
        }

        public void Dispose()
        {
            _controls?.Dispose();
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            Interact?.Invoke(context);
        }

        public void OnAnyKey(InputAction.CallbackContext context)
        {
            AnyKey?.Invoke(context);
        }

        public void OnRestart(InputAction.CallbackContext context)
        {
            Restart?.Invoke(context);
        }
    }
}