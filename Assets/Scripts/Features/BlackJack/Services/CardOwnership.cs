using Features.Card.Components;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Features.BlackJack.Services
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class CardOwnership : ICardOwnership
    {
        private readonly Stash<OwnerComponent> _owner = World.Default.GetStash<OwnerComponent>();

        public bool IsOwnedBy(Entity card, Entity owner) =>
            _owner.Has(card) && _owner.Get(card).Value.Id == owner.Id;
    }
}