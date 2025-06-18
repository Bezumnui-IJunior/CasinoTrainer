using UnityEngine;

namespace Sounds.Configs
{
    [CreateAssetMenu(fileName = "MusicConfig", menuName = "Configs / MusicConfig", order = 51)]
    public class MusicConfig : ScriptableObject, IMusicConfig
    {
        [SerializeField] private AudioClip _backgroundMusic;

        public AudioClip BackgroundMusic => _backgroundMusic;
    }
}