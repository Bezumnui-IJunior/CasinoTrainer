using Features.Card.Services;
using Features.EntityViewFactory.Components;
using Features.View.Components;
using Infrastructure.Providers;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Features.View.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class CardsViewSystem : ISystem
    {
        private Stash<CardsViewComponent> _cardsView;
        private Filter _filter;
        private Stash<ViewInitTag> _initTag;
        private Stash<ViewComponent> _view;
        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter
                .With<ViewComponent>()
                .With<CardsViewComponent>()
                .Without<ViewInitTag>()
                .Build();

            _view = World.GetStash<ViewComponent>();
            _cardsView = World.GetStash<CardsViewComponent>();
            _initTag = World.GetStash<ViewInitTag>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity entity in _filter)
            {
                ref EntityProvider view = ref _view.Get(entity).Value;
                ICardsView cardsView = view.GetComponent<ICardsView>();

                if (cardsView == null)
                    throw new MissingComponentException("cardsView was not found.");

                _cardsView.Get(entity).Value = cardsView;
                _initTag.Add(entity);
            }
        }

        public void Dispose() { }
    }
}