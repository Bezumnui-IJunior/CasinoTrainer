using System;
using Progress;
using Sounds;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Common.Settings
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class MusicSlider : MonoBehaviour
    {
        private const float PercentMultiplier = 100f;

        [SerializeField] private Slider _slider;
        private IBackgroundMusic _backgroundMusic;
        private ISettings _settings;

        private void Awake()
        {
            if (_slider == null)
                _slider = GetComponent<Slider>();
        }

        private void Update()
        {
            _backgroundMusic.AudioSource.volume = _slider.value;
        }

        private void OnEnable()
        {
            _slider.value = _backgroundMusic.AudioSource.volume;
            _slider.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(OnValueChanged);
        }

        [Inject]
        private void Constructor(ISettings settings, IBackgroundMusic backgroundMusic)
        {
            _settings = settings;
            _backgroundMusic = backgroundMusic;
        }

        private void OnValueChanged(float value)
        {
            _settings.MusicVolume = value;
        }
    }
}