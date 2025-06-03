using Features.BlackJack.Components;
using Features.BlackJack.Configs;
using Features.BlackJack.Services;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Features.BlackJack.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class DealerInitializeSystem : IInitializer
    {
        private readonly IDealerConfig _dealerConfig;
        private readonly IDealerFactory _dealerFactory;
        private Stash<CardHolderTag> _cardHolderTag;
        private Stash<DealerTag> _dealerTag;
        private Stash<ScoreComponent> _score;

        public DealerInitializeSystem(IDealerFactory dealerFactory, IDealerConfig dealerConfig)
        {
            _dealerFactory = dealerFactory;
            _dealerConfig = dealerConfig;
        }

        public World World { get; set; }

        public void OnAwake()
        {
            Entity entity = _dealerFactory.CreateDealer();
            World.GetStash<TakeCardTimerComponent>().Add(entity).Value = _dealerConfig.FirstCardTimeout;
        }

        public void Dispose() { }
    }
}