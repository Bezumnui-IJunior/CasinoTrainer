using Features.BlackJack.Components;
using Features.Dealer.Components;
using Features.GameOver.Components;
using Scellecs.Morpeh;
using Sounds.Configs;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using View;

namespace Features.GameOver.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class GameOverPlaySoundSystem : ISystem
    {
        private readonly ISoundFXService _soundFXService;
        private readonly IMusicConfig _musicConfig;
        private Filter _dealerFilter;
        private Stash<SoundPlayedTag> _tag;
        private Filter _filter;
        private Filter _playerFilter;
        private Stash<WinnerComponent> _winner;

        public GameOverPlaySoundSystem(ISoundFXService soundFXService, IMusicConfig musicConfig)
        {
            _soundFXService = soundFXService;
            _musicConfig = musicConfig;
        }

        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter
                .With<GameOverTag>()
                .Without<SoundPlayedTag>()
                .Build();

            _playerFilter = World.Filter.With<PlayerTag>().Build();
            _dealerFilter = World.Filter.With<DealerTag>().Build();

            _winner = World.GetStash<WinnerComponent>();
            _tag = World.GetStash<SoundPlayedTag>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity winnerEntity in _filter)
            {
                _tag.Add(winnerEntity);
                if (_winner.Has(winnerEntity) == false)
                {
                    _soundFXService.PlayClip(_musicConfig.GameDrawSound);

                    continue;
                }

                ref WinnerComponent winner = ref _winner.Get(winnerEntity);

                foreach (Entity player in _playerFilter)
                    PlayIfWinner(winner, player, _musicConfig.GameWinSound);

                foreach (Entity dealer in _dealerFilter)
                    PlayIfWinner(winner, dealer, _musicConfig.GameLostSound);
            }
        }

        public void Dispose() { }

        private void PlayIfWinner(WinnerComponent winner, Entity entity, AudioClip clip)
        {
            if (winner.Value == entity.Id)
                _soundFXService.PlayClip(clip);
        }
    }
}