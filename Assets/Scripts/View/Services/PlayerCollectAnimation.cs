using DG.Tweening;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using VContainer;

namespace View.Services
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class PlayerCollectAnimation : IPlayerCollectAnimation
    {
        private const float CardOffsetY = 0.001f;
        private readonly Quaternion _faceDownRotation;

        private readonly Quaternion _faceUpRotation;

        private readonly IPlayerViewConfig _playerViewConfig;
        private readonly Quaternion _showRotation;

        [Inject]
        public PlayerCollectAnimation(IPlayerViewConfig playerViewConfig)
        {
            _playerViewConfig = playerViewConfig;
            _faceUpRotation = Quaternion.Euler(Vector3.right * _playerViewConfig.CardFaceUpAngle);
            _faceDownRotation = Quaternion.Euler(Vector3.right * _playerViewConfig.CardFaceDownAngle);
            _showRotation = Quaternion.Euler(Vector3.right * _playerViewConfig.CardShowAngle);
        }

        public void OnCollect(Transform card, float cardsCount, bool isFaceUp)
        {
            Vector3 destination = _playerViewConfig.PlayerHandsPosition;

            destination.x += _playerViewConfig.PayerFirstCardOffsetX + _playerViewConfig.CardOffsetX * cardsCount;
            destination.y += CardOffsetY * cardsCount;

            DOTween.Sequence()
                .Append(card.DOMove(_playerViewConfig.CardShowCenter, _playerViewConfig.CardShowDuration))
                .Append(card.DOMove(destination, _playerViewConfig.CardDealDuration));

            Sequence rotationSequence = DOTween.Sequence().Append(card.DORotateQuaternion(_showRotation, _playerViewConfig.CardShowDuration));

            if (isFaceUp)
                rotationSequence.Append(card.DORotateQuaternion(_faceUpRotation, _playerViewConfig.CardDealDuration));
            else
                rotationSequence.Append(card.DORotateQuaternion(_faceDownRotation, _playerViewConfig.CardDealDuration));
        }
    }
}