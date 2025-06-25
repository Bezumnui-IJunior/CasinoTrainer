using Infrastructure;
using Unity.IL2CPP.CompilerServices;
using VContainer;

namespace GameStates
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class BootstrapState : State
    {
        [Inject]
        public BootstrapState(IStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            ChangeState<LoaderState>();
        }
    }
}