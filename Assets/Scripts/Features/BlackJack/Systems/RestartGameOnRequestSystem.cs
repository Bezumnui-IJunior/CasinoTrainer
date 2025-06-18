using Features.BlackJack.Components;
using GameStates;
using Infrastructure;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Features.BlackJack.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class RestartGameOnRequestSystem : ICleanupSystem
    {
        private readonly IStateMachine _stateMachine;
        private Stash<RestartGameRequestTag> _restartRequest;

        public RestartGameOnRequestSystem(IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public World World { get; set; }

        public void OnAwake()
        {
            _restartRequest = World.GetStash<RestartGameRequestTag>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (RestartGameRequestTag _ in _restartRequest)
                _stateMachine.ChangeState<BlackJackRunningState>();
        }

        public void Dispose() { }
    }
}