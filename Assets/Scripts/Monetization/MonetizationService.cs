using Unity.Services.LevelPlay;
using UnityEngine;
using VContainer;

namespace Monetization
{
    public class MonetizationService : IMonetizationService
    {
#if UNITY_ANDROID
        private const string InterstitialId = "Interstitial_Android";
        private const string RewardedId = "Rewarded_Android";
        private const string BannerId = "Banner_Android";
#elif UNITY_IOS
        private const string InterstitialId = "iu1s0heb26xsc546";
        private const string RewardedId = "s04uybej1460x0sg";
        private const string GetMoneyRewardedId = "0ompqdk94dczp9eg";
        // private const string GetMoneyRewardedPlacement = "Get_Money";
        private const string BannerId = "dsagj0ec35rch39s";
#endif

        private const string AppleAppKey = "2285b0c4d";

        public RewardedAdvert MoneyRewardedAd { get; }
        
        public bool IsInitialized { get; private set; }

        [Inject]
        public MonetizationService()
        {
            MoneyRewardedAd = new RewardedAdvert(CreateRewardedAd());
            IsInitialized = false;
            LevelPlay.OnInitSuccess += SdkInitializationSuccessEvent;
            LevelPlay.OnInitFailed += SdkInitializationFailedEvent;

            LevelPlay.SetMetaData("is_test_suite", "enable");
            string userId = IronSource.Agent.getAdvertiserId();
            Debug.Log("IronSource User ID: " + userId);
            LevelPlay.Init(AppleAppKey);
        }

        ~MonetizationService()
        {
            
            LevelPlay.OnInitSuccess -= SdkInitializationSuccessEvent;
            LevelPlay.OnInitFailed -= SdkInitializationFailedEvent;
        }
        

        private void SdkInitializationFailedEvent(LevelPlayInitError obj)
        {
            Debug.Log($"Failed to init advert ({obj.ErrorCode}): {obj.ErrorMessage}.");
            LevelPlay.ValidateIntegration();
        }

        private void SdkInitializationSuccessEvent(LevelPlayConfiguration obj)
        {
            IsInitialized = true;
            MoneyRewardedAd.Load();
            Debug.Log($"Advert successfully activated.");
        }

        public LevelPlayInterstitialAd CreateInterstitialAd() =>
            new LevelPlayInterstitialAd(InterstitialId);
        
        public LevelPlayRewardedAd CreateRewardedAd() =>
            new LevelPlayRewardedAd(RewardedId);

        public LevelPlayBannerAd CreateBannerAd() =>
            new LevelPlayBannerAd(BannerId);
        
        private LevelPlayRewardedAd CreateMoneyRewardedAd() =>
            new LevelPlayRewardedAd(GetMoneyRewardedId);
    }
}