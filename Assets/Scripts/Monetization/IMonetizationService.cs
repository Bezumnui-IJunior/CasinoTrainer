using System;
using Unity.Services.LevelPlay;

namespace Monetization
{
    public interface IMonetizationService
    {
        public RewardedAdvert MoneyRewardedAd { get; }
        bool IsInitialized { get; }
    }
}