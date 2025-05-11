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
    }
}