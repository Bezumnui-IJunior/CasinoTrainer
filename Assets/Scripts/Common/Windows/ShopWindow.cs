using Windows;
using Unity.IL2CPP.CompilerServices;
using Unity.Services.LevelPlay;
using UnityEngine;
using VContainer;

namespace Common.Windows
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class ShopWindow : Window
    {
        [SerializeField] private GetMoneyByAdButton _watchAdButton;
        [SerializeField] private ExitButton _exitButton;
        private LevelPlayRewardedAd _rewardedAdvertisement;

        [Inject]
        private void Construct(IObjectResolver resolver)
        {
            resolver.Inject(_watchAdButton);
            resolver.Inject(_exitButton);
        }
    }
}