using Features.BlackJack.Components;
using Features.Card.Components;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Features.BlackJack.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class CalculateHiddenCardsSystem : ISystem
    {
        private Filter _cards;
        private Stash<HiddenCardsComponent> _faceUpStash;
        private Stash<OwnerComponent> _owner;
        private Filter _owners;

        public World World { get; set; }

        public void OnAwake()
        {
            _owners = World.Filter
                .With<HiddenCardsComponent>()
                .With<CardHolderComponent>()
                .Build();

            _cards = World.Filter
                .Without<FaceUpTag>()
                .With<OwnerComponent>()
                .Build();

            _faceUpStash = World.GetStash<HiddenCardsComponent>();
            _owner = World.GetStash<OwnerComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity owner in _owners)
            {
                ref int hiddenCardsCount = ref _faceUpStash.Get(owner).Value;
                hiddenCardsCount = 0;

                foreach (Entity card in _cards)
                {
                    if (_owner.Get(card).Value.Id == owner.Id)
                        hiddenCardsCount++;
                }
            }
        }

        public void Dispose() { }
    }
}