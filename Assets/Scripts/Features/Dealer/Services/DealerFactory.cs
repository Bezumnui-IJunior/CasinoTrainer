using Features.BlackJack.Components;
using Features.BlackJack.Services;
using Features.Dealer.Components;
using Features.View.Components;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using View.Services;

namespace Features.Dealer.Services
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class DealerFactory : IDealerFactory
    {
        private readonly IDealerCollectAnimation _animation;
        private readonly Stash<CollectAnimationComponent> _animationComponent;
        private readonly Stash<CardHolderComponent> _cardHolderTag;
        private readonly Stash<DealerTag> _dealerTag;
        private readonly Stash<ScoreComponent> _score;
        private readonly World _world;
        private readonly Stash<HiddenCardsComponent> _hiddenCards;

        public DealerFactory(World world, IDealerCollectAnimation animation)
        {
            _world = world;
            _animation = animation;
            _dealerTag = _world.GetStash<DealerTag>();
            _cardHolderTag = _world.GetStash<CardHolderComponent>();
            _score = _world.GetStash<ScoreComponent>();
            _animationComponent = _world.GetStash<CollectAnimationComponent>();
            _hiddenCards = _world.GetStash<HiddenCardsComponent>();
        }

        public Entity CreateDealer()
        {
            Entity entity = _world.CreateEntity();
            _dealerTag.Add(entity);
            _cardHolderTag.Add(entity);
            _score.Add(entity);
            _animationComponent.Add(entity).Value = _animation;
            _hiddenCards.Add(entity);
            return entity;
        }
    }
}