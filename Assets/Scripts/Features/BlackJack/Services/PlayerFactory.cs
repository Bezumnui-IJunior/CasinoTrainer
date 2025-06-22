using Features.BlackJack.Components;
using Features.View.Components;
using Progress;
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
        private readonly Stash<CardHolderComponent> _cardHolder;
        private readonly Stash<CollectAnimationComponent> _collectAnimation;
        private readonly IPlayerCollectAnimation _playerCollectAnimation;
        private readonly Stash<PlayerTag> _playerTag;
        private readonly Stash<ScoreComponent> _score;
        private readonly Stash<MoneyHolderComponent> _money;
        private readonly IPlayerData _playerData;
        private readonly World _world;

        public PlayerFactory(World world, IPlayerCollectAnimation playerCollectAnimation, IPlayerData playerData)
        {
            _world = world;
            _playerCollectAnimation = playerCollectAnimation;
            _playerData = playerData;
            _playerTag = _world.GetStash<PlayerTag>();
            _cardHolder = _world.GetStash<CardHolderComponent>();
            _score = _world.GetStash<ScoreComponent>();
            _collectAnimation = _world.GetStash<CollectAnimationComponent>();
            _money = _world.GetStash<MoneyHolderComponent>();
        }

        public Entity CreatePlayer()
        {
            Entity entity = _world.CreateEntity();

            _playerTag.Add(entity);
            _cardHolder.Add(entity);
            _score.Add(entity);
            _collectAnimation.Add(entity).Value = _playerCollectAnimation;
            _money.Add(entity).Value = _playerData.PlayerMoney;

            return entity;
        }
    }
}