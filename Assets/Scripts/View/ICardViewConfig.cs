using UnityEngine;

namespace View
{
    public interface ICardViewConfig
    {
        float CardWidth { get; }
        float CardRotatePositionY { get; }
        float CardRotateSecondsUp { get; }
        float CardRotateSecondsDown { get; }
        Vector3 CardRotation { get; }
        float CardRotateSecondsRotation { get; }
    }
}