using TMPro;
using UnityEngine;

namespace UI
{
    public class SlotComponent : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _valueTop;
        [SerializeField] private TextMeshProUGUI _valueBottom;

        public void SetValueTop(string value)
        {
            _valueTop.text = value;
        }

        public void SetValueBottom(string value)
        {
            _valueBottom.text = value;
        }

        private void OnDrawGizmos()
        {
            var rectTransform = GetComponent<RectTransform>();
            if (!rectTransform)
                return;
            Vector3[] corners = new Vector3[4];

            rectTransform.GetWorldCorners(corners);

            for (int i = 0; i < 4; i++)
                Gizmos.DrawSphere(corners[i], 0.1f);

            Vector3 center = (corners[0] + corners[2]) * 0.5f;
            Gizmos.DrawSphere(center, 0.1f);
        }
    }
}