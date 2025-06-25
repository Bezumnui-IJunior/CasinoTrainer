using UnityEngine;

namespace Sounds.Configs
{
    public interface IMusicConfig
    {
        AudioClip BackgroundMusic { get; }
        AudioClip CardTakenSound { get; }
        AudioClip GameWinSound { get; }
        AudioClip GameLostSound { get; }
        AudioClip GameDrawSound { get; }
        AudioClip InfoSound { get; }
    }
}