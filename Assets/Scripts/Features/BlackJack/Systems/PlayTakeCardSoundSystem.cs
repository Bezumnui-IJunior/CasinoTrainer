using Features.BlackJack.Components;
using Features.Card.Components;
using Features.View.Components;
using Scellecs.Morpeh;
using Sounds.Configs;
using Unity.IL2CPP.CompilerServices;
using VContainer;
using View;

namespace Features.BlackJack.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class PlayTakeCardSoundSystem : ISystem
    {
        private readonly ISoundFXService _soundFXService;
        private readonly IMusicConfig _musicConfig;
        private Filter _cardHolderFilter;

        public World World { get; set; }

        public void Dispose() { }

        public PlayTakeCardSoundSystem(ISoundFXService soundFXService, IMusicConfig musicConfig)
        {
            _soundFXService = soundFXService;
            _musicConfig = musicConfig;
        }

        public void OnAwake()
        {
            _cardHolderFilter = World.Filter
                .With<TakeCardRequestTag>()
                .With<CardHolderComponent>()
                .Build();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity _ in _cardHolderFilter)
                _soundFXService.PlayClip(_musicConfig.CardTakenSound);
        }

    }
}