using System;
using Features.BlackJack.Components;
using Features.View.Components;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Features.View.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class PlayerDoneCollectingSystem : ISystem
    {
        private Request<PlayerDoneCollectingRequest> _request;
        private Filter _dealerFilter;
        private Stash<ShouldTakeCardTag> _allowedToTakeCardTag;
        public World World { get; set; }

        public void OnAwake()
        {
            _dealerFilter = World.Filter
                .With<CardHolderTag>()
                .With<DealerTag>()
                .Build();

            _allowedToTakeCardTag = World.GetStash<ShouldTakeCardTag>();
            _request = World.GetRequest<PlayerDoneCollectingRequest>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (PlayerDoneCollectingRequest _ in _request.Consume())
            foreach (Entity entity in _dealerFilter)
                _allowedToTakeCardTag.Add(entity);
        }

        public void Dispose() { }
    }
}