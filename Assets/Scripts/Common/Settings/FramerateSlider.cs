using Progress;
using TMPro;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Common.Settings
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class FramerateSlider : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshProUGUI _textMeshPro;
        private ISettings _settings;

        private void Awake()
        {
            if (_slider == null)
                _slider = GetComponent<Slider>();
        }

        private void OnEnable()
        {
            _slider.value = _settings.TargetFrameRate;
            _slider.onValueChanged.AddListener(OnValueChanged);
        }

        private void Update()
        {
            _textMeshPro.text = $"FPS: {_slider.value}";
        }

        private void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(OnValueChanged);
        }

        [Inject]
        private void Constructor(ISettings settings)
        {
            _settings = settings;
        }

        private void OnValueChanged(float value)
        {
            _settings.TargetFrameRate = (int)_slider.value;
            Application.targetFrameRate = _settings.TargetFrameRate;
        }
    }
}