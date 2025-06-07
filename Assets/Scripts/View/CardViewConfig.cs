using UnityEngine;

namespace View
{
    [CreateAssetMenu(fileName = "CardViewConfig", menuName = "ECS Configs / CardViewConfig", order = 51)]

    public class CardViewConfig : ScriptableObject, ICardViewConfig
    {
        [SerializeField] private float _cardWidth = 0.33f;
        [SerializeField] private float _cardRotatePositionY = 2;
        [SerializeField] private float _cardRotateSecondsUp = 1f;
        [SerializeField] private float _cardRotateSecondsDown = 1f;
        [SerializeField] private float _cardRotateSecondsRotation = .5f;
        [SerializeField] private Vector3 _cardRotation;
        
        public float CardWidth => _cardWidth;
        public float CardRotatePositionY => _cardRotatePositionY;
        public float CardRotateSecondsUp => _cardRotateSecondsUp;
        public float CardRotateSecondsDown => _cardRotateSecondsDown;
        public float CardRotateSecondsRotation => _cardRotateSecondsRotation;
        public Vector3 CardRotation => _cardRotation;
    }
}