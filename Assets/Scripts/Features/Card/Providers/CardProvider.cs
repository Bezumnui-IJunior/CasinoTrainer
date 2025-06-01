using Features.Card.Components;
using Infrastructure.Providers;
using UnityEngine;

namespace Features.Card.Providers
{
    public class CardProvider : ComponentsProvider
    {
        [SerializeField] private MeshRenderer _meshRenderer;

        protected override void OnInitialize()
        {
            AddToStash<FaceMeshRendererComponent>().Value = _meshRenderer;
        }
    }
}