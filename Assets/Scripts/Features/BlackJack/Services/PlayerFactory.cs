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
    public class PlayerFactory : IPlayerFactory
    {
        private readonly World _world;
        private readonly IPlayerCollectAnimation _playerCollectAnimation;
        private readonly Stash<PlayerTag> _playerTag;
        private readonly Stash<CardHolderComponent> _cardHolder;
        private readonly Stash<ScoreComponent> _score;
        private readonly Stash<CollectAnimationComponent> _collectAnimation;

        public PlayerFactory(World world, IPlayerCollectAnimation playerCollectAnimation)
        {
            _world = world;
            _playerCollectAnimation = playerCollectAnimation;
            _playerTag = _world.GetStash<PlayerTag>();
            _cardHolder = _world.GetStash<CardHolderComponent>();
            _score = _world.GetStash<ScoreComponent>();
            _collectAnimation = _world.GetStash<CollectAnimationComponent>();
        }

        public Entity CreatePlayer()
        {
            Entity entity = _world.CreateEntity();

            _playerTag.Add(entity);
            _cardHolder.Add(entity);
            _score.Add(entity);
            _collectAnimation.Add(entity).Value = _playerCollectAnimation;
            _world.GetStash<BetComponent>().Add(entity).Value = 10;

            return entity;
        }
    }
}