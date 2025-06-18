using Windows;
using Features.BlackJack.Components;
using Features.Common.Components;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Features.GameOver.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class GameOverTurnSystem : ISystem
    {
        private readonly IWindowsManager _windowsManager;
        private Stash<GameOverTag> _gameOverTag;
        private Stash<TurnHolderComponent> _turnHolder;

        public World World { get; set; }

        public void OnAwake()
        {
            _turnHolder = World.GetStash<TurnHolderComponent>();
            _gameOverTag = World.GetStash<GameOverTag>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (ref GameOverTag _ in _gameOverTag)
            foreach (ref TurnHolderComponent holder in _turnHolder)
                holder.Value = 0;
        }

        public void Dispose() { }
    }
}