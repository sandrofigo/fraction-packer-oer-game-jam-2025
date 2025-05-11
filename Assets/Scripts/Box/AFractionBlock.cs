using UnityEngine;

namespace Box
{
    public abstract class AFractionBlock : MonoBehaviour
    {
        [SerializeField]
        private FractionBlockAnimationController _animationController;

        [SerializeField]
        private bool _isFull;

        public bool IsFull => _isFull;

        public (FractionSlot, Transform) Slot { get; set; }

        private Vector3 _initialPosition;
        private Vector3 _initialRotation;

        public void SetInitialPosition(Vector3 position, Vector3 rotation)
        {
            _initialPosition = position;
            _initialRotation = rotation;
        }

        public void ResetPosition()
        {
            MoveTo(_initialPosition, _initialRotation, true);
        }

        public void MoveTo(Vector3 position, bool animate)
        {
            if (animate)
            {
                _animationController.MoveTo(position);
            }
            else
            {
                transform.position = position;
            }
        }
        
        public void MoveTo(Vector3 position, Vector3 rotation, bool animate)
        {
            if (animate)
            {
                _animationController.MoveTo(position, rotation);
            }
            else
            {
                transform.position = position;
                transform.rotation = Quaternion.Euler(rotation);
            }
        }

        public void MoveAndRotateTo(Vector3 position, Vector3 rotation, float delay)
        {
            _animationController.MoveAndRotateTo(position, rotation, delay);
        }

        public void SetHover(bool isHovered)
        {
            _animationController.SetHover(isHovered);
        }

        public void Select()
        {
            _animationController.Select();
        }

        public void DoInvalidShake()
        {
            _animationController.DoInvalidShakeAnimation();
        }

        public void DisableColliders()
        {
            foreach (var c in GetComponentsInChildren<Collider>())
            {
                c.enabled = false;
            }
        }
    }
}