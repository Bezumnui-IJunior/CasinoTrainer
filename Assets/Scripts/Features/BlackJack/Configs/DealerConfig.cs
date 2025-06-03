using UnityEngine;

namespace Features.BlackJack.Configs
{
    [CreateAssetMenu(fileName = "DealerConfig", menuName = "ECS Configs / DealerConfig", order = 51)]
    public class DealerConfig : ScriptableObject, IDealerConfig
    {
        [SerializeField] private float _firstCardTimeout = 2;
        [SerializeField] private float _takeCardTimeout = 1;

        public float FirstCardTimeout => _firstCardTimeout;
        public float TakeCardTimeout => _takeCardTimeout;
    }
}