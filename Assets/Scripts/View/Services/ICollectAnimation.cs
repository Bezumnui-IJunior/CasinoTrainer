using UnityEngine;

namespace View.Services
{
    public interface ICollectAnimation
    {
        public void OnCollect(Transform card, float cardsCount, bool isFaceUp);
    }
}