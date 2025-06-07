using Features.BlackJack.Configs;
using Features.BlackJack.Services;
using Features.Dealer.Systems;
using Scellecs.Morpeh.Addons.Feature;
using Unity.IL2CPP.CompilerServices;
using VContainer;

namespace Features.Dealer
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class DealerFeature : CombinedFeature
    {
        private readonly IDealerConfig _dealerConfig;
        private readonly IDealerFactory _dealerFactory;

        [Inject]
        public DealerFeature(IDealerFactory dealerFactory, IDealerConfig dealerConfig)
        {
            _dealerFactory = dealerFactory;
            _dealerConfig = dealerConfig;
        }

        protected override void Initialize()
        {
            AddInitializer(new DealerInitializeSystem(_dealerFactory, _dealerConfig));

            AddSystem(new DealerCooldownSystem());
            AddSystem(new DealerLoseDeciderSystem());
            AddSystem(new DealerWinDeciderSystem());
            AddSystem(new DealerDelegateDeciderSystem());
            AddSystem(new DealerIntroPlayedSystem());
            AddSystem(new DealerRotatingDeciderSystem());
            AddSystem(new DealerTakeCardDeciderSystem());
            AddSystem(new DealerDelegateTurnSystem());
            AddSystem(new DealerRotateCardSystem());
            AddSystem(new DealerTakeCardSystem());
            AddSystem(new DealerCooldownScheduleSystem(_dealerConfig));

            AddSystem(new DealerCleanupSystem());
        }
    }
}