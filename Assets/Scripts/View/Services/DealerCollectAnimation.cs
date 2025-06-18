using DG.Tweening;
using UnityEngine;

namespace View.Services
{
    public class DealerCollectAnimation : IDealerCollectAnimation
    {
        private const float CardOffsetY = 0.001f;

        private readonly Quaternion _cardFaceDownAngle;
        private readonly Quaternion _cardFaceDownRotation;
        private readonly Quaternion _cardFaceUpAngle;

        private readonly IDealerViewConfig _dealerViewConfig;

        public DealerCollectAnimation(IDealerViewConfig dealerViewConfig)
        {
            _dealerViewConfig = dealerViewConfig;
            _cardFaceUpAngle = Quaternion.Euler(_dealerViewConfig.CardFaceUpAngle);
            _cardFaceDownAngle = Quaternion.Euler(_dealerViewConfig.CardFaceDownAngle);
            _cardFaceDownRotation = Quaternion.Euler(_dealerViewConfig.CardFaceDownAngle);
        }

        public void OnCollect(Transform card, float cardsCount, bool isFaceUp)
        {
            card.rotation = _cardFaceDownRotation;
            Vector3 destination = _dealerViewConfig.HandsPosition;

            destination.x += _dealerViewConfig.FirstCardOffsetX + _dealerViewConfig.CardOffsetX * cardsCount;
            destination.y = CardOffsetY * cardsCount;
            
            DOTween.Sequence()
                .Append(card.DOMove(card.position + _dealerViewConfig.CardPathCenter, _dealerViewConfig.CardGetUpDuration).OnComplete(() => RotateCard(card, isFaceUp)))
                .Append(card.DOMove(destination, _dealerViewConfig.CardDealDuration));
        }

        private void RotateCard(Transform card, bool isFaceUp)
        {
            if (isFaceUp)
                card.DORotateQuaternion(_cardFaceUpAngle, _dealerViewConfig.CarRotateDuration);
        }
    }
}