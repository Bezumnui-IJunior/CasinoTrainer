using UnityEngine;

namespace View
{
    public interface IPlayerViewConfig
    {
        float CardDealDuration { get; }
        float CardShowDuration { get; }
        float CardShowAngle { get; }
        float CardFaceUpAngle { get; }
        float CardFaceDownAngle { get; }
        Vector3 CardShowCenter { get; }
        Vector3 PlayerHandsPosition { get; }
        float PayerFirstCardOffsetX { get; }
        float CardOffsetX { get; }
    }
}