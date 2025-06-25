using System;
using Windows;
using Monetization;
using Notifications;
using Progress;
using TMPro;
using Unity.IL2CPP.CompilerServices;
using Unity.Services.LevelPlay;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Common.Windows
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class GetMoneyByAdButton : MonoBehaviour
    {
        private const float RewardValue = 200;
        [SerializeField] private Button _watchAdButton;
        [SerializeField] private TextMeshProUGUI _textMesh;
        private RewardedAdvert _advert;
        private IMonetizationService _monetizationService;
        private IPlayerData _playerData;
        private IWindowsManager _windowsManager;
        private IMoneyAdvertService _moneyAdvertService;
        private INotificationsFactory _notificationsFactory;
        private LevelPlayRewardedAd RewardedAdvertisement => _advert.LevelPlayRewardedAd;

        private void Awake()
        {
            if (_watchAdButton == null)
                _watchAdButton = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _watchAdButton.interactable = false;
            _advert = _monetizationService.MoneyRewardedAd;

            RewardedAdvertisement.OnAdRewarded += OnAdRewarded;
            RewardedAdvertisement.OnAdLoaded += OnLoaded;
            RewardedAdvertisement.OnAdLoadFailed += OnLoadFailed;
            RewardedAdvertisement.OnAdClosed += OnAdClosed;

            _watchAdButton.onClick.AddListener(OnWatchAdClicked);

            if (_monetizationService.IsInitialized == false)
            {
                SetLoading();
            }
            else if (RewardedAdvertisement.IsAdReady())
            {
                OnLoaded();
            }
            else
            {
                _advert.Load();
                SetLoading();
            }
        }

        private void OnDisable()
        {
            _watchAdButton.onClick.RemoveListener(OnWatchAdClicked);

            RewardedAdvertisement.OnAdRewarded -= OnAdRewarded;
            RewardedAdvertisement.OnAdLoaded -= OnLoaded;
            RewardedAdvertisement.OnAdLoadFailed -= OnLoadFailed;
            RewardedAdvertisement.OnAdClosed -= OnAdClosed;
        }

        private void SetCapped()
        {
            _textMesh.text = "Try later. Your cap is reached";
            _watchAdButton.interactable = false;
        }

        private void SetReady()
        {
            _textMesh.text = "Click to Watch an Advert";
            _watchAdButton.interactable = true;
        }

        private void SetLoading()
        {
            _textMesh.text = "Loading an Advert...";
            _watchAdButton.interactable = false;
        }

        private void SetError()
        {
            _textMesh.text = "Try later.";
            _watchAdButton.interactable = false;
        }

        private void OnAdClosed(LevelPlayAdInfo obj)
        {
            _moneyAdvertService.WatchAdvert();
            
            if (_moneyAdvertService.IsFirstAdvert())
                _notificationsFactory.ScheduleAdvertReadyReminder();
            
            SetLoading();
            _advert.Load();
        }

        private void OnLoadFailed(LevelPlayAdError info)
        {
            Debug.Log($"Load failed: {info.ErrorCode} {info.ErrorMessage}");
            SetError();
        }

        private void OnLoaded(LevelPlayAdInfo levelPlayAdInfo = null)
        {
            if (TrySetCapped() == false)
                SetReady();
        }

        private void LoadAdvert()
        {
            if (TrySetCapped())
                return;
            
            _advert.Load();
            SetLoading();
        }

        private void ShowAdvert()
        {
            if (TrySetCapped())
                return;
            
            if (RewardedAdvertisement.IsAdReady() == false)
            {
                LoadAdvert();
                return;
            }
            
            RewardedAdvertisement.ShowAd();
            _watchAdButton.interactable = false;
        }

        private bool TrySetCapped()
        {
            if (_moneyAdvertService.СanWatchAdvert())
                return false;

            SetCapped();

            return true;

        }

        private void OnAdRewarded(LevelPlayAdInfo info, LevelPlayReward reward)
        {
            _playerData.PlayerMoney += RewardValue;
            _playerData.Save();
            _windowsManager.Open(WindowsId.RewardGivenWindow);
        }

        private void OnWatchAdClicked()
        {
            if (RewardedAdvertisement == null)
                throw new NullReferenceException();
            
            ShowAdvert();
        }

        [Inject]
        private void Construct(IMonetizationService monetizationService, IPlayerData playerData, IWindowsManager windowsManager, IMoneyAdvertService moneyAdvertService, INotificationsFactory notificationsFactory)
        {
            _monetizationService = monetizationService;
            _playerData = playerData;
            _windowsManager = windowsManager;
            _moneyAdvertService = moneyAdvertService;
            _notificationsFactory = notificationsFactory;
        }
    }
}