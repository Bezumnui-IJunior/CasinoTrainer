using TriInspector;
using UnityEngine;

namespace Sounds.Configs
{
    [CreateAssetMenu(fileName = "MusicConfig", menuName = "Configs / MusicConfig", order = 51)]
    public class MusicConfig : ScriptableObject, IMusicConfig
    {
        [ShowInInspector] public AudioClip BackgroundMusic { get; private set; }
        [ShowInInspector] public AudioClip CardTakenSound { get; private set; }
        [ShowInInspector] public AudioClip GameWinSound { get; private set; }
        [ShowInInspector] public AudioClip GameLostSound { get; private set; }
        [ShowInInspector] public AudioClip GameDrawSound { get; private set; }
        [ShowInInspector] public AudioClip InfoSound { get; private set; }
    }
}