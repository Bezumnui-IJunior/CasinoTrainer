using Features.Card.Components;
using Features.Common.Components;
using Features.EntityViewFactory.Components;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Features.Card.Services
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class DeckFactory : IDeckFactory
    {
        private const string CardPrefab = "Prefabs/Card";
        private const float CardHeight = 0.0012f;

        private static readonly Vector3 SpawnPosition = new Vector3(-1.429f, 0.001f + CardHeight * 52, 0.462f);
        // private static readonly Vector3 SpawnPosition = new Vector3(-1.429f, 0.101f, 0.462f);

        private readonly Stash<ChangePositionComponent> _changePositionComponent;
        private readonly Stash<DenominalComponent> _denominal;
        private readonly Stash<OrderComponent> _order;
        private readonly Stash<OwnerComponent> _owner;
        private readonly Stash<SetAppearTag> _setAppearTag;
        private readonly Stash<SuitComponent> _suit;
        private readonly Stash<ViewPathComponent> _viewPath;

        public DeckFactory()
        {
            World world = World.Default;

            _setAppearTag = world.GetStash<SetAppearTag>();
            _viewPath = world.GetStash<ViewPathComponent>();
            _denominal = world.GetStash<DenominalComponent>();
            _suit = world.GetStash<SuitComponent>();
            _owner = world.GetStash<OwnerComponent>();
            _changePositionComponent = world.GetStash<ChangePositionComponent>();
            _order = world.GetStash<OrderComponent>();
        }

        public Entity CreateCard(World world, Denominations denominal, Suits suit, Entity deck, int order)
        {
            Vector3 position = SpawnPosition;
            position.y -= CardHeight * order;

            Entity entity = world.CreateEntity();
            _viewPath.Add(entity).Value = CardPrefab;
            _denominal.Add(entity).Value = denominal;
            _suit.Add(entity).Value = suit;
            _owner.Add(entity).Value = deck;
            _changePositionComponent.Add(entity).Value = position;
            _order.Add(entity).Value = order;
            _setAppearTag.Add(entity);

            return entity;
        }
    }
}