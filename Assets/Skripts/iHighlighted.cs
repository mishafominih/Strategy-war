using System.Collections.Generic;
using UnityEngine;

public interface iHighlighted
{
    public void highlight();
    public void unHighlight();
    public void moveTo(Vector2 pos);
    public void moveToAndRotateTo(Vector2 pos, Vector2 PointToRotate);
    public iButtleUnit getButtleUnit();
}
