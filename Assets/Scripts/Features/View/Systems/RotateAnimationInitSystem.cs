using Features.EntityViewFactory.Components;
using Features.View.Components;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Features.View.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class RotateAnimationInitSystem : IInitializer
    {
        private const string PrefabPath = "Prefabs/RotateAnimation";
        public World World { get; set; }

        public void OnAwake()
        {
            Entity entity = World.CreateEntity();
            World.GetStash<ViewPathComponent>().Add(entity).Value = PrefabPath;
            World.GetStash<CardRotateComponent>().Add(entity).Value = null;
        }

        public void Dispose() { }
    }
}