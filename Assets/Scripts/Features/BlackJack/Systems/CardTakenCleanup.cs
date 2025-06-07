using System;
using Features.BlackJack.Components;
using Features.View.Components;
using Scellecs.Morpeh;

namespace Features.BlackJack.Systems
{
    public class CardTakenCleanup : ICleanupSystem
    {
        private Filter _takenFilter;
        private Stash<TakeCardRequestTag> _shouldTakeCard;
        private Stash<TakenTag> _cardTakenTag;
        private Filter _shouldTakeFilter;
        private Stash<PlayerConsumeRequestTag> _playerConsumerTag;
        private Filter _playerConsumerFilter;
        public World World { get; set; }

        public void OnAwake()
        {
            _takenFilter = World.Filter
                .With<TakenTag>()
                .Build();

            _shouldTakeFilter = World.Filter
                .With<TakeCardRequestTag>()
                .Build();

            _playerConsumerFilter = World.Filter
                .With<PlayerConsumeRequestTag>()
                .Build();

            _cardTakenTag = World.GetStash<TakenTag>();
            _shouldTakeCard = World.GetStash<TakeCardRequestTag>();
            _playerConsumerTag = World.GetStash<PlayerConsumeRequestTag>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity entity in _shouldTakeFilter)
                _shouldTakeCard.Remove(entity);

            foreach (Entity entity in _takenFilter)
                _cardTakenTag.Remove(entity);

            foreach (Entity entity in _playerConsumerFilter)
                _playerConsumerTag.Remove(entity);
        }

        public void Dispose() { }
    }
}