using Features.BlackJack.Components;
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
        private readonly IDealerFactory _dealerFactory;
        private Stash<CardHolderTag> _cardHolderTag;
        private Stash<DealerTag> _dealerTag;
        private Stash<ScoreComponent> _score;
        public World World { get; set; }


        public DealerInitializeSystem(IDealerFactory dealerFactory)
        {
            _dealerFactory = dealerFactory;
        }

    
        public void OnAwake()
        {
            _dealerFactory.CreateDealer();
        }

        public void Dispose() { }
    }
}