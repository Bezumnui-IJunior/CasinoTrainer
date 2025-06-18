using Features.Card.Components;
using Features.EntityViewFactory.Components;
using Features.View.Components;
using Scellecs.Morpeh;
using UnityEngine;

namespace Features.Card.Systems
{
    public class CardSetAppearSystem : ISystem
    {
        private Stash<DenominalComponent> _denominal;
        private Stash<FaceMeshRendererComponent> _faceRenderer;
        private Filter _filter;
        private Stash<SetAppearTag> _setAppearTag;
        private Stash<SuitComponent> _suit;
        private Stash<CardsViewComponent> _cardsView;

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
            _cardsView = World.GetStash<CardsViewComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (CardsViewComponent cardsView in _cardsView)
            foreach (Entity entity in _filter)
            {
                _setAppearTag.Remove(entity);
                ref SuitComponent suit = ref _suit.Get(entity);
                ref DenominalComponent denominal = ref _denominal.Get(entity);
                Texture2D texture = cardsView.Value.GetCardTexture(denominal.Value, suit.Value);
                _faceRenderer.Get(entity).Value.material.mainTexture = texture;
            }
        }

        public void Dispose() { }
    }
}