using System.Collections.Generic;
using UnityEngine;

public class CardGenerator : MonoBehaviour
{
    [SerializeField] private TemplateCard _cardPrefab;

    [SerializeField] private List<Sprite> _sprites;

    [ContextMenu("Generate")]
    private void Generate()
    {
        foreach (Sprite sprite in _sprites)
        {
            TemplateCard card = Instantiate(_cardPrefab, transform);
            card.SetSprite(sprite);
            Debug.Log($"{card.name} created.");
            card.Destroy();
        }
    }
}