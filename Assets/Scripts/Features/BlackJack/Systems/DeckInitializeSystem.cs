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
            World.GetStash<CardHolderComponent>().Add(deck);
            World.GetStash<DeckTag>().Add(deck);

            Queue<int> orders = GetRandomOrders();

            foreach (Denominations denomination in Enum.GetValues(typeof(Denominations)))
            foreach (Suits suit in Enum.GetValues(typeof(Suits)))
                _deckFactory.CreateCard(World, denomination, suit, deck, orders.Dequeue());
        }

        public void Dispose() { }

        private Queue<int> GetRandomOrders(int size = 52)
        {
            List<int> result = new List<int>(size);

            for (int i = 0; i < size; i++)
                result.Add(i);

            for (int i = 0; i < size; i++)
            {
                int randomIndex = Random.Range(0, size);
                (result[i], result[randomIndex]) = (result[randomIndex], result[i]);
            }

            return new Queue<int>(result);
        }
    }
}