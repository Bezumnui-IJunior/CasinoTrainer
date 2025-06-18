using Infrastructure;
using Progress;
using Sounds;
using Sounds.Configs;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using VContainer;

namespace GameStates
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class SetupMusicState : State
    {
        private readonly IBackgroundMusic _backgroundMusic;
        private readonly ISettings _settings;

        [Inject]
        public SetupMusicState(IStateMachine stateMachine, IBackgroundMusic backgroundMusic, ISettings settings) : base(stateMachine)
        {
            _backgroundMusic = backgroundMusic;
            _settings = settings;
        }

        public override void Enter()
        {
            Object.DontDestroyOnLoad(StartPlayMusic());
            ChangeState<MainMenuState>();
        }

        private AudioSource StartPlayMusic()
        {
            AudioSource music = CreateAudioSource();

            music.gameObject.name = "BackgroundMusic";
            music.volume = _settings.MusicVolume;
            _backgroundMusic.SetAudioSource(music);
            _backgroundMusic.AudioSource.Play();

            return music;
        }

        private AudioSource CreateAudioSource() =>
            new GameObject().AddComponent<AudioSource>();
    }
}