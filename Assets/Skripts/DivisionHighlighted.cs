using UnityEngine;

public class DivisionHighlighted : MonoBehaviour, iHighlighted
{
    private Color oldColor;
    public void highlight()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        oldColor = sprite.color;
        sprite.color = Color.red;
    }

    public void unHighlight()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.color = oldColor;
    }
}
