using Features.EntityViewFactory.Components;
using Features.View.Components;
using Infrastructure.Providers;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using View.Services;

namespace Features.View.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class RotateAnimationSetupSystem : ISystem
    {
        private Stash<CardRotateComponent> _cardRotate;
        private Filter _filter;
        private Stash<ViewInitTag> _initTag;
        private Stash<ViewComponent> _view;
        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter
                .With<ViewComponent>()
                .With<CardRotateComponent>()
                .Without<ViewInitTag>()
                .Build();

            _view = World.GetStash<ViewComponent>();
            _cardRotate = World.GetStash<CardRotateComponent>();
            _initTag = World.GetStash<ViewInitTag>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity entity in _filter)
            {
                ref EntityProvider view = ref _view.Get(entity).Value;
                ICardRotateAnimation rotateAnimation = view.GetComponent<ICardRotateAnimation>();

                if (rotateAnimation == null)
                    throw new MissingComponentException("rotateAnimation was not found.");

                _cardRotate.Get(entity).Value = rotateAnimation;
                _initTag.Add(entity);
            }
        }

        public void Dispose() { }
    }
}