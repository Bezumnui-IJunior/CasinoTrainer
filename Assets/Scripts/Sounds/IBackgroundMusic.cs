using UnityEngine;

namespace Sounds
{
    public interface IBackgroundMusic
    {
        void SetAudioSource(AudioSource source);
        AudioSource AudioSource { get; }
    }
}