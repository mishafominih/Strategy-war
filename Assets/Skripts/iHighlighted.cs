using System.Collections.Generic;
using UnityEngine;

public interface iHighlighted
{
    public void highlight();
    public void unHighlight();
    public void moveTo(Vector2 pos);
    public iButtleUnit getButtleUnit();
    public List<UIButtons> getUIButtons();
}
