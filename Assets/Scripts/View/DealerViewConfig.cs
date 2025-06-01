using UnityEngine;

namespace View
{
    [CreateAssetMenu(fileName = "ECS Configs / DealerViewConfig", menuName = "ECS Configs / DealerViewConfig", order = 0)]
    public class DealerViewConfig : ScriptableObject, IDealerViewConfig
    {
        [SerializeField] private float _cardDealDuration;
        [SerializeField]  private float _cardRotateDuration;
        [SerializeField] private float _cardGetUpDuration;
        [SerializeField] private Vector3 _cardFaceUpAngle;
        [SerializeField] private Vector3 _cardFaceDownAngle;
        [SerializeField] private Vector3 _cardPathCenter;
        [SerializeField] private Vector3 _handsPosition;
        [SerializeField] private float _cardWidth = 0.33f;
        [SerializeField] private float _firstCardOffsetXFactor = -3;
        [SerializeField] private float _cardOffsetXFactor = 0.5f;
        

        public float CardDealDuration => _cardDealDuration;
        public float CarRotateDuration => _cardRotateDuration;
        public float CardGetUpDuration => _cardGetUpDuration;
        public Vector3 CardFaceUpAngle => _cardFaceUpAngle;
        public Vector3 CardFaceDownAngle => _cardFaceDownAngle;
        public Vector3 CardPathCenter => _cardPathCenter;
        public Vector3 HandsPosition => _handsPosition;
        public float FirstCardOffsetX => _firstCardOffsetXFactor * _cardWidth;
        public float CardOffsetX => _cardOffsetXFactor * _cardWidth;
    }
}