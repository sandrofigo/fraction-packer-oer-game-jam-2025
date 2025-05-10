using TMPro;
using UnityEngine;

namespace UI
{
    public class SlotComponent : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _valueTop;
        [SerializeField] private TextMeshProUGUI _valueBottom;

        public void SetValueTop(int value)
        {
            _valueTop.text = value.ToString();
        }

        public void SetValueBottom(int value)
        {
            _valueBottom.text = value.ToString();
        }
    }
}