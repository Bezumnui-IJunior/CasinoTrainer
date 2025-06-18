using Features.BlackJack.Components;
using Features.Dealer.Components;
using Features.View.Components;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using View.Services;

namespace Features.BlackJack.Services
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class GameOverFactory : IGameOverFactory
    {
        private readonly IDealerCollectAnimation _animation;
        private readonly Stash<GameOverTag> _gameOverTag; 
        private readonly Stash<WinnerComponent> _winner; 
        private readonly World _world;

        public GameOverFactory(World world)
        {
            _world = world;
            _gameOverTag = _world.GetStash<GameOverTag>();
            _winner = _world.GetStash<WinnerComponent>();
        }

        public Entity CreateGameOver(Entity winner)
        {
            Entity entity = CreateDraw();
            _winner.Add(entity).Value = winner.Id;

            return entity;
        }

        public Entity CreateDraw()
        {
            Entity entity = _world.CreateEntity();
            _gameOverTag.Add(entity);
            return entity;
        }
    }
}