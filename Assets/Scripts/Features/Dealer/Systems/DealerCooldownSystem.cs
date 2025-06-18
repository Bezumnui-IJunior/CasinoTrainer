using Features.BlackJack.Components;
using Features.Dealer.Components;
using Scellecs.Morpeh;

namespace Features.Dealer.Systems
{
    public class DealerCooldownSystem : ISystem
    {
        private Filter _filter;

        private Stash<TakeCardCooldownComponent> _takeCardTimer;

        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter
                .With<DealerTag>()
                .With<TakeCardCooldownComponent>()
                .Build();

            _takeCardTimer = World.GetStash<TakeCardCooldownComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity entity in _filter)
            {
                ref TakeCardCooldownComponent cooldown = ref _takeCardTimer.Get(entity);

                cooldown.Value -= deltaTime;

                if (cooldown.Value - deltaTime > 0)
                    continue;

                _takeCardTimer.Remove(entity);
            }
        }

        public void Dispose() { }
    }
}