using Features.BlackJack.Components;
using Features.Card.Components;
using Features.Dealer.Components;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Features.Dealer.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class DealerRotateCardSystem : ISystem
    {
        private Filter _cardsFilter;
        private Filter _dealerFilter;
        private Stash<OwnerComponent> _owner;
        private Stash<RotateTag> _rotate;

        public World World { get; set; }

        public void OnAwake()
        {
            _dealerFilter = World.Filter
                .With<RotatingRequestTag>()
                .With<TurnHolderTag>()
                .With<DealerTag>()
                .Without<TakeCardCooldownComponent>()
                .Build();

            _cardsFilter = World.Filter
                .Without<RotateTag>()
                .Without<FaceUpTag>()
                .With<DenominalComponent>()
                .With<OwnerComponent>()
                .Build();

            _owner = World.GetStash<OwnerComponent>();
            _rotate = World.GetStash<RotateTag>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity dealer in _dealerFilter)
            foreach (Entity card in _cardsFilter)
            {
                if (_owner.Get(card).Value.Id == dealer.Id)
                    _rotate.Add(card);
                
            }
        }

        public void Dispose() { }
    }
}