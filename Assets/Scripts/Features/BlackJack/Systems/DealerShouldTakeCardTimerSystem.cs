using Features.BlackJack.Components;
using Features.BlackJack.Configs;
using Scellecs.Morpeh;

namespace Features.BlackJack.Systems
{
    public class DealerShouldTakeCardTimerSystem : ISystem
    {
        
        private Stash<ShouldTakeCardTag> _shouldTakeCardTag;
        private Stash<TakeCardTimerComponent> _takeCardTimer;
        private Filter _filter;
        
        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter
                .With<DealerTag>()
                .With<TakeCardTimerComponent>()
                .Build();
            
            _shouldTakeCardTag = World.GetStash<ShouldTakeCardTag>();
            _takeCardTimer = World.GetStash<TakeCardTimerComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity entity in _filter)
            {
                ref TakeCardTimerComponent timer = ref _takeCardTimer.Get(entity);

                timer.Value -= deltaTime;
                    
                if (timer.Value - deltaTime > 0)
                    continue;

                _shouldTakeCardTag.Add(entity);
                _takeCardTimer.Remove(entity);
            }
        }

        public void Dispose() { }
    }
}