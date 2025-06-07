using Features.BlackJack.Components;
using Features.Card.Components;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Features.BlackJack.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class FaceUpCardSystem : ISystem
    {
        private Stash<FaceUpTag> _faceUpStash;
        private Filter _filter;

        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter
                .With<RotateTag>()
                .Without<FaceUpTag>()
                .Build();

            _faceUpStash = World.GetStash<FaceUpTag>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity card in _filter)
                _faceUpStash.Add(card);
        }

        public void Dispose() { }
    }
}