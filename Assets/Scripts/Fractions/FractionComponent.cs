using TMPro;
using UnityEngine;

namespace Fractions
{
    public class FractionComponent : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _valueText;
        
        public int Value { get; private set; }
        
        public void SetValue(int value)
        {
            Value = value;
            _valueText.text = Value.ToString();
        }
    }
}