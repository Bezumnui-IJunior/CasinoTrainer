using UnityEngine;

namespace View
{
    public interface ISoundFXService
    {
        bool CanPlaySound { get; }
        void PlayClip(AudioClip clip);
        void SetSource(AudioSource source);
        bool IsSourceMatch(AudioSource source);
        void SetVolume(float volume);
    }
}