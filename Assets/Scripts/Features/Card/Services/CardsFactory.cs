using Features.Card.Components;
using Features.Common.Components;
using Features.EntityViewFactory.Components;
using Infrastructure.Providers;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Features.Card.Services
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class DeckFactory : MonoBehaviour, IDeckFactory
    {
        [SerializeField] private EntityProvider _cardPrefab;
        private Stash<ChangePositionComponent> _changePositionComponent;

        private Stash<DenominalComponent> _denominal;
        private Stash<OrderComponent> _order;
        private Stash<OwnerComponent> _owner;
        private Stash<SetAppearTag> _setAppearTag;
        private Stash<SuitComponent> _suit;
        private Stash<ViewPrefabComponent> _viewPrefab;

        private void Awake()
        {
            World world = World.Default;

            _setAppearTag = world.GetStash<SetAppearTag>();
            _viewPrefab = world.GetStash<ViewPrefabComponent>();
            _denominal = world.GetStash<DenominalComponent>();
            _suit = world.GetStash<SuitComponent>();
            _owner = world.GetStash<OwnerComponent>();
            _changePositionComponent = world.GetStash<ChangePositionComponent>();
            _order = world.GetStash<OrderComponent>();
        }

        public Entity CreateCard(World world, Denominations denominal, Suits suit, Entity deck, int order)
        {
            Entity entity = world.CreateEntity();
            _viewPrefab.Add(entity).Value = _cardPrefab;
            _denominal.Add(entity).Value = denominal;
            _suit.Add(entity).Value = suit;
            _owner.Add(entity).Value = deck;
            _changePositionComponent.Add(entity).Value = transform.position;
            _order.Add(entity).Value = order;
            _setAppearTag.Add(entity);

            return entity;
        }
    }
}