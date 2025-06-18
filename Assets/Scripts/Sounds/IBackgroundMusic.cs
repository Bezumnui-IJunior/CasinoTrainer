using UnityEngine;

namespace Sounds
{
    public interface IBackgroundMusic
    {
        AudioSource AudioSource { get; }
        void SetAudioSource(AudioSource source);
    }
}