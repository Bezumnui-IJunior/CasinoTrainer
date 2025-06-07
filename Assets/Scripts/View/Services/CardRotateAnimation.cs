using DG.Tweening;
using UnityEngine;

namespace View.Services
{
    public class CardRotateAnimation : MonoBehaviour, ICardRotateAnimation
    {
        public void RotateUp(Transform card, ICardViewConfig config)
        {
            DOTween.Sequence()
                .Append(card.DOMoveY(card.transform.position.y + config.CardRotatePositionY, config.CardRotateSecondsUp))
                .Append(card.DORotate(config.CardRotation, config.CardRotateSecondsRotation))
                .Append(card.DOMoveY(card.transform.position.y, config.CardRotateSecondsDown));
        }
    }
}