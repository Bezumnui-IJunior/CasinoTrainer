using Features.BlackJack.Components;
using Features.BlackJack.Configs;
using Features.BlackJack.Services;
using Features.Common.Components;
using Features.Dealer.Components;
using Features.Dealer.Services;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Features.Dealer.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class DealerInitializeSystem : IInitializer
    {
        private readonly IDealerConfig _dealerConfig;
        private readonly IDealerFactory _dealerFactory;
        private Stash<CardHolderComponent> _cardHolderTag;
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
            Entity dealer = _dealerFactory.CreateDealer();
            World.GetStash<TakeCardCooldownComponent>().Add(dealer).Value = _dealerConfig.FirstCardTimeout;
            World.GetStash<TurnHolderTag>().Add(dealer);
        }
        
        public void Dispose() { }
    }
}