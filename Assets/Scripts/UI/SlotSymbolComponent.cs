using TMPro;
using UnityEngine;

namespace UI
{
    public class SlotSymbolComponent : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        public void SetText(string text)
        {
            _text.text = text;
        }
    }
}