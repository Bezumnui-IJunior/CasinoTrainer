using Features.BlackJack.Components;
using Features.View.Components;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using View.Services;

namespace Features.BlackJack.Services
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class DealerFactory : IDealerFactory
    {
        private readonly IDealerCollectAnimation _animation;
        private readonly Stash<CollectAnimationComponent> _animationComponent;
        private readonly Stash<CardHolderTag> _cardHolderTag;
        private readonly Stash<DealerTag> _dealerTag;
        private readonly Stash<ScoreComponent> _score;
        private readonly World _world;

        public DealerFactory(World world, IDealerCollectAnimation animation)
        {
            _world = world;
            _animation = animation;
            _dealerTag = _world.GetStash<DealerTag>();
            _cardHolderTag = _world.GetStash<CardHolderTag>();
            _score = _world.GetStash<ScoreComponent>();
            _animationComponent = _world.GetStash<CollectAnimationComponent>();
        }

        public Entity CreateDealer()
        {
            Entity entity = _world.CreateEntity();
            _dealerTag.Add(entity);
            _cardHolderTag.Add(entity);
            _score.Add(entity);
            _animationComponent.Add(entity).Value = _animation;

            return entity;
        }
    }
}