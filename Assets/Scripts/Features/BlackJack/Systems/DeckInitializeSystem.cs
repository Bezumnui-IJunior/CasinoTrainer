using System;
using System.Collections.Generic;
using Features.BlackJack.Components;
using Features.Card.Components;
using Features.Card.Services;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using VContainer;
using Random = UnityEngine.Random;

namespace Features.BlackJack.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class DeckInitializeSystem : IInitializer
    {
        private readonly IDeckFactory _deckFactory;

        [Inject]
        public DeckInitializeSystem(IDeckFactory deckFactory)
        {
            _deckFactory = deckFactory;
        }

        public World World { get; set; }

        public void OnAwake()
        {
            Entity deck = World.CreateEntity();
            World.GetStash<CardHolderTag>().Add(deck);
            World.GetStash<DeckTag>().Add(deck);

            int orderIndex = 0;

            List<Entity> cards = new List<Entity>(128);

            foreach (Denominations denomination in Enum.GetValues(typeof(Denominations)))
            foreach (Suits suit in Enum.GetValues(typeof(Suits)))
                cards.Add(_deckFactory.CreateCard(World, denomination, suit, deck, orderIndex++));

            Shuffle(cards);
        }

        public void Dispose() { }

        private void Shuffle(List<Entity> cards)
        {
            Stash<OrderComponent> order = World.GetStash<OrderComponent>();

            foreach (Entity card in cards)
            {
                int randomIndex = Random.Range(0, cards.Count);
                ref OrderComponent order1 = ref order.Get(card);
                ref OrderComponent order2 = ref order.Get(cards[randomIndex]);

                (order2.Value, order1.Value) = (order1.Value, order2.Value);
            }
        }
    }
}