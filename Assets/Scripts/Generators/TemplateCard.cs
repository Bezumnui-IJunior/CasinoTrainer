using UnityEngine;

public class TemplateCard : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _front;

    public void SetSprite(Sprite sprite)
    {
        _front.sprite = sprite;
        gameObject.name = $"Card {sprite.name}";
    }

    public void Destroy()
    {
        DestroyImmediate(this);
    }
}