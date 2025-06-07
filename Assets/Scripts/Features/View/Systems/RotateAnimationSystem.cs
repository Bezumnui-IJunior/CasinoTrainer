using Features.BlackJack.Components;
using Features.Card.Components;
using Features.EntityViewFactory.Components;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using View;
using View.Services;

namespace Features.View.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class RotateAnimationSystem : ISystem
    {
        private readonly ICardRotateAnimation _cardRotateAnimation;
        private readonly ICardViewConfig _config;

        private Stash<FaceUpTag> _faceUpStash;

        private Filter _filter;
        private Stash<ViewComponent> _view;

        public RotateAnimationSystem(ICardRotateAnimation cardRotateAnimation, ICardViewConfig config)
        {
            _cardRotateAnimation = cardRotateAnimation;
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

            _view = World.GetStash<ViewComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity card in _filter)
                _cardRotateAnimation.RotateUp(_view.Get(card).Value.transform, _config);
        }

        public void Dispose() { }
    }
}