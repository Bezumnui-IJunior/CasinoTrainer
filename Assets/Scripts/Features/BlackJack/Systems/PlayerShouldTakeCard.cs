using Features.BlackJack.Components;
using Features.View.Components;
using Scellecs.Morpeh;
using ShouldTakeCardTag = Features.BlackJack.Components.ShouldTakeCardTag;

namespace Features.BlackJack.Systems
{
    public class PlayerShouldTakeCard : ISystem
    {
        private Filter _tagFilter;
        private Stash<ShouldTakeCardTag> _shouldTakeCard;
        private Filter _playerFilter;

        public World World { get; set; }

        public void OnAwake()
        {
            _tagFilter = World.Filter
                .Without<ShouldTakeCardTag>()
                .Build();
            
            _playerFilter = World.Filter
                .With<PlayerTag>()
                .Build();

            _shouldTakeCard = World.GetStash<ShouldTakeCardTag>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity _ in _tagFilter)
            foreach (Entity player in _playerFilter)
                _shouldTakeCard.Add(player);

        }

        public void Dispose() { }
    }
}