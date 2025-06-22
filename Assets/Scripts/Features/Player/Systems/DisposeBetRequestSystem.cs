using Features.Common.Components;
using Features.View.Components;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Features.Player.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class DisposeBetRequestSystem : ICleanupSystem
    {
        private Stash<DisposingTag> _disposingTag;
        private Filter _requestFilter;
        public World World { get; set; }

        public void OnAwake()
        {
            _requestFilter = World.Filter
                .With<PlaceBetRequest>()
                .Without<DisposingTag>()
                .Build();
            
            _disposingTag = World.GetStash<DisposingTag>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity bet in _requestFilter)
                _disposingTag.Add(bet);
        }

        public void Dispose() { }
    }
}