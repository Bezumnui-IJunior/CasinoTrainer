using System;
using System.Collections.Generic;
using UnityEngine;

namespace Features.Card.Services
{
    public class CardsView : MonoBehaviour, ICardsView
    {
        [SerializeField] private List<Texture2D> _diamonds;
        [SerializeField] private List<Texture2D> _hearts;
        [SerializeField] private List<Texture2D> _spades;
        [SerializeField] private List<Texture2D> _clubs;

        private List<List<Texture2D>> _suits;

        private void Awake()
        {
            _suits = new List<List<Texture2D>> { _diamonds, _hearts, _spades, _clubs };

            int suitsCount = Enum.GetValues(typeof(Suits)).Length;
            int cardsPerSuitCount = Enum.GetValues(typeof(Denominations)).Length;
            ;

            if (_suits.Count != suitsCount)
                throw new ArgumentOutOfRangeException($"Suits are {_suits.Count}. Must be {suitsCount}");

            foreach (List<Texture2D> suit in _suits)
            {
                if (suit.Count != cardsPerSuitCount)
                    throw new ArgumentOutOfRangeException($"There are {suit.Count} cards in a deck. Must be {cardsPerSuitCount}");
            }
        }

        public Texture2D GetCardTexture(Denominations denomination, Suits suit) =>
            _suits[(int)suit][(int)denomination];
    }
}