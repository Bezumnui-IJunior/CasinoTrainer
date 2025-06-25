using Unity.Services.LevelPlay;

namespace Monetization
{
    public interface IRewardedAdvert
    {
        bool IsLoading { get; }
        LevelPlayRewardedAd LevelPlayRewardedAd { get; }
        bool Load();
    }
}