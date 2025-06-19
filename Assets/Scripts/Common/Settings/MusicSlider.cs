using Progress;
using Sounds;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.UI;
using VContainer;

namespace Common.Settings
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class MusicSlider
    {
        private readonly IBackgroundMusic _backgroundMusic;
        private readonly ISettings _settings;
        private readonly Slider _slider;

        [Inject]
        public MusicSlider(Slider slider, ISettings settings, IBackgroundMusic backgroundMusic)
        {
            _slider = slider;
            _settings = settings;
            _backgroundMusic = backgroundMusic;
        }

        public void Update()
        {
            _backgroundMusic.AudioSource.volume = _slider.value;
        }

        public void Enable()
        {
            _slider.value = _backgroundMusic.AudioSource.volume;
            _slider.onValueChanged.AddListener(OnValueChanged);
        }

        public void Disable()
        {
            _slider.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(float value)
        {
            _settings.MusicVolume = value;
        }
    }
}