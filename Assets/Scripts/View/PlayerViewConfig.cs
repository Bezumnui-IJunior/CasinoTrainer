using UnityEngine;

namespace View
{
    [CreateAssetMenu(fileName = "PlayerViewConfig", menuName = "ECS Configs / PlayerViewConfig", order = 51)]
    public class PlayerViewConfig : ScriptableObject, IPlayerViewConfig
    {
        [SerializeField] private float _cardDealDuration;
        [SerializeField] private float _cardShowDuration;
        [SerializeField] private float _cardShowAngle;
        [SerializeField] private float _cardFaceUpAngle;
        [SerializeField] private float _cardFaceDownAngle;
        [SerializeField] private Vector3 _cardShowCenter = new Vector3(0, 0.8f, -1);
        [SerializeField] private Vector3 _playerHandsPosition = new Vector3(0, 0, -1);
        [SerializeField] private float _cardWidth = 0.33f;
        [SerializeField] private float _payerFirstCardOffsetXFactor = -3;
        [SerializeField] private float _cardOffsetXFactor = 0.5f;

        public float CardDealDuration => _cardDealDuration;

        public float CardShowDuration => _cardShowDuration;
        public float CardShowAngle => _cardShowAngle;
        public float CardFaceUpAngle => _cardFaceUpAngle;
        public float CardFaceDownAngle => _cardFaceDownAngle;
        public Vector3 CardShowCenter => _cardShowCenter;
        public Vector3 PlayerHandsPosition => _playerHandsPosition;
        public float PayerFirstCardOffsetX => _payerFirstCardOffsetXFactor * _cardWidth;
        public float CardOffsetX => _cardOffsetXFactor * _cardWidth;
    }
}