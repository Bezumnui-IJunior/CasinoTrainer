using System;
using Progress;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using View;

namespace Common.Settings
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class SoundFXSlider : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        private ISettings _settings;
        private ISoundFXService _soundFXService;

        public void OnEnable()
        {
            _slider.value = _settings.SoundFXVolume;
            _slider.onValueChanged.AddListener(OnValueChanged);
        }

        public void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void Update()
        {
            _soundFXService.SetVolume(_slider.value);
        }

        [Inject]
        private void Constructor(ISettings settings, ISoundFXService soundFXService)
        {
            _settings = settings;
            _soundFXService = soundFXService;
        }

        private void OnValueChanged(float value)
        {
            _settings.SoundFXVolume = value;
        }
    }
}