using UnityEngine;

namespace View
{
    public interface IDealerViewConfig
    {
        float CardDealDuration { get; }
        float CarRotateDuration { get; }
        float CardGetUpDuration { get; }
        Vector3 CardFaceUpAngle { get; }
        Vector3 CardFaceDownAngle { get; }
        Vector3 CardPathCenter { get; }
        Vector3 HandsPosition { get; }
        float FirstCardOffsetX { get; }
        float CardOffsetX { get; }
    }
}