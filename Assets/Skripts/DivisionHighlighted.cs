using System.Collections.Generic;
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

    public void moveTo(Vector2 pos)
    {
        getButtleUnit().BreakAttack();
        GetComponent<iMoveController>().moveTo(pos);
    }

    public iButtleUnit getButtleUnit()
    {
        return GetComponent<DivisionButtleUnit>();
    }

    public List<AttackType> getAttackTypes()
    {
        return new List<AttackType>(){ AttackType.Fire, AttackType.HandButtle };
    }
}
