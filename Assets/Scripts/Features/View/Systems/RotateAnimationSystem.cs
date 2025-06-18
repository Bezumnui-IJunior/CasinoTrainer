using Features.BlackJack.Components;
using Features.Card.Components;
using Features.EntityViewFactory.Components;
using Features.View.Components;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using View;

namespace Features.View.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class RotateAnimationSystem : ISystem
    {
        private readonly ICardViewConfig _config;
        private Stash<CardRotateComponent> _animation;

        private Stash<FaceUpTag> _faceUpStash;

        private Filter _filter;
        private Stash<ViewComponent> _view;

        public RotateAnimationSystem(ICardViewConfig config)
        {
            _config = config;
        }

        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter
                .With<RotateTag>()
                .With<ViewComponent>()
                .With<FaceUpTag>()
                .Build();

            _animation = World.GetStash<CardRotateComponent>();
            _view = World.GetStash<ViewComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (CardRotateComponent animation in _animation)
            foreach (Entity card in _filter)
                animation.Value.RotateUp(_view.Get(card).Value.transform, _config);
        }

        public void Dispose() { }
    }
}