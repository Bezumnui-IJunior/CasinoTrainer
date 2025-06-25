using Unity.Services.LevelPlay;

namespace Monetization
{
    public class RewardedAdvert : IRewardedAdvert
    {
        private readonly string _placementName;

        public RewardedAdvert(LevelPlayRewardedAd levelPlayRewardedAd, string placementName = "")
        {
            _placementName = placementName;
            LevelPlayRewardedAd = levelPlayRewardedAd;
            LevelPlayRewardedAd.OnAdLoaded += OnLoaded;
            LevelPlayRewardedAd.OnAdLoadFailed += OnLoadFailed;
        }

        public bool IsLoading { get; private set; }

        public LevelPlayRewardedAd LevelPlayRewardedAd { get; }

        public bool Load()
        {
            if (IsLoading || LevelPlayRewardedAd.IsAdReady())
                return false;

            IsLoading = true;
            LevelPlayRewardedAd.LoadAd();

            return true;
        }

        public bool IsPlacementCapped() =>
            LevelPlayRewardedAd.IsPlacementCapped(_placementName);

        ~RewardedAdvert()
        {
            LevelPlayRewardedAd.OnAdLoaded -= OnLoaded;
            LevelPlayRewardedAd.OnAdLoadFailed -= OnLoadFailed;
        }

        private void OnLoadFailed(LevelPlayAdError obj)
        {
            IsLoading = false;
        }

        private void OnLoaded(LevelPlayAdInfo obj)
        {
            IsLoading = false;
        }
    }
}