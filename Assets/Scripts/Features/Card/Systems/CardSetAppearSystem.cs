using Features.Card.Components;
using Features.Card.Services;
using Features.EntityViewFactory.Components;
using Scellecs.Morpeh;
using UnityEngine;

namespace Features.Card.Systems
{
    public class CardSetAppearSystem : ISystem
    {
        private readonly ICardsView _cardsView;
        private Stash<DenominalComponent> _denominal;
        private Stash<FaceMeshRendererComponent> _faceRenderer;
        private Filter _filter;
        private Stash<SetAppearTag> _setAppearTag;
        private Stash<SuitComponent> _suit;

        public CardSetAppearSystem(ICardsView cardsView)
        {
            _cardsView = cardsView;
        }

        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter
                .With<SuitComponent>()
                .With<ViewComponent>()
                .With<SetAppearTag>()
                .With<DenominalComponent>()
                .With<FaceMeshRendererComponent>()
                .Build();

            _denominal = World.GetStash<DenominalComponent>();
            _suit = World.GetStash<SuitComponent>();
            _setAppearTag = World.GetStash<SetAppearTag>();
            _faceRenderer = World.GetStash<FaceMeshRendererComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity entity in _filter)
            {
                _setAppearTag.Remove(entity);
                ref SuitComponent suit = ref _suit.Get(entity);
                ref DenominalComponent denominal = ref _denominal.Get(entity);
                Texture2D texture = _cardsView.GetCardTexture(denominal.Value, suit.Value);
                _faceRenderer.Get(entity).Value.material.mainTexture = texture;
            }
        }

        public void Dispose() { }
    }
}