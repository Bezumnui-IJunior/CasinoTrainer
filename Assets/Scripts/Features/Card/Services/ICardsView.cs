using UnityEngine;

namespace Features.Card.Services
{
    public interface ICardsView
    {
        Texture2D GetCardTexture(Denominations denomination, Suits suit);
    }
}