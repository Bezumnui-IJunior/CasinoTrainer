using Progress;
using UnityEngine;
using VContainer;

namespace View.Services
{
    public class SoundFXService : ISoundFXService
    {
        private readonly ISettings _settings;
        private AudioSource _audioSource;

        public bool CanPlaySound => _audioSource != null;

        [Inject]
        public SoundFXService(ISettings settings)
        {
            _settings = settings;
        }

        public void SetSource(AudioSource source)
        {
            _audioSource = source;

            if (source != null)
                _audioSource.volume = _settings.SoundFXVolume;
        }

        public void PlayClip(AudioClip clip)
        {
            if (CanPlaySound && clip != null)
                _audioSource.PlayOneShot(clip);
        }

        public bool IsSourceMatch(AudioSource source) =>
            _audioSource == source;

        public void SetVolume(float volume)
        {
            if (CanPlaySound)
                _audioSource.volume = volume;
        }
    }
}