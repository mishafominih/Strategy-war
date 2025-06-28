using UnityEngine;

public interface iMoveController
{
    public void moveTo(Vector2 pos);
    public void rotateTo(Vector2 pos);
    public bool IsMove();
    public bool IsRotate();
    public void Stop();
}
