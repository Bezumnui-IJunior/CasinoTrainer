using System;
using System.Globalization;
using TMPro;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace View.UI.Buttons
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [RequireComponent(typeof(Button))]
    public class DoubleInput : MonoBehaviour
    {
        private const double Multiplier = 2;
        [SerializeField] private TMP_InputField _inputField;
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            if (_inputField.text.Length == 0)
                _inputField.text = "0";

            if (double.TryParse(_inputField.text, out double value) == false)
                throw new InvalidOperationException("Invalid input");

            _inputField.text = (value * Multiplier).ToString(CultureInfo.InvariantCulture);
        }
    }
}