using Features.BlackJack.Components;
using Features.Dealer.Components;
using Features.View.Components;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Features.BlackJack.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class PlayerDoneCollectingSystem : ISystem
    {
        private Stash<TakeCardRequestTag> _allowedToTakeCardTag;
        private Filter _dealerFilter;
        private Request<PlayerDoneCollectingRequest> _request;
        public World World { get; set; }

        public void OnAwake()
        {
            _dealerFilter = World.Filter
                .With<CardHolderComponent>()
                .With<DealerTag>()
                .Build();

            _allowedToTakeCardTag = World.GetStash<TakeCardRequestTag>();
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