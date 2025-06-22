using System.Linq;
using Windows;
using Features.Common.Components;
using Infrastructure;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace GameStates
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class BlackJackRunningState : State
    {
        private readonly BlackJackFeatures _blackJackFeatures;
        private readonly Stash<DisposingTag> _disposingTag;
        private readonly IWindowsManager _windowsManager;
        private readonly World _world;

        public BlackJackRunningState(
            IStateMachine stateMachine,
            World world,
            IFeatureFactory factory,
            IIndexer indexer,
            IWindowsManager windowsManager) : base(stateMachine)
        {
            _world = world;
            _windowsManager = windowsManager;
            _blackJackFeatures = new BlackJackFeatures(world, factory, indexer);
            _disposingTag = _world.GetStash<DisposingTag>();
        }

        public override void Enter()
        {
            _blackJackFeatures.AddFeatures();
            
            _windowsManager.OpenOrLeaveOnly(
                WindowsId.PlaceBetWindow,
                WindowsId.MoneyWindow
            );
        }

        public override void Exit()
        {
            Unload();
            _world.CleanupUpdate(0);
            _blackJackFeatures.RemoveFeatures();
        }

        private void Unload()
        {
            foreach (Entity entity in _world.GetAllEntities().Where(entity => _disposingTag.Has(entity) == false))
                _disposingTag.Add(entity);
        }
    }
}