using Sounds.Configs;
using UnityEngine;

namespace Sounds
{
    public class BackgroundMusic : IBackgroundMusic
    {
        private readonly IMusicConfig _musicConfig;
        public AudioSource AudioSource { get; private set; }

        public BackgroundMusic(IMusicConfig musicConfig)
        {
            _musicConfig = musicConfig;
        }

        public void SetAudioSource(AudioSource source)
        {
            AudioSource = source;
            AudioSource.clip = _musicConfig.BackgroundMusic;
            AudioSource.loop = true;
        }
        
    }
}