using UnityEngine;

public interface iMoveController
{
    public void moveTo(Vector2 pos);
    public void rotateTo(Vector2 pos);
    public bool NeedMove();
    public bool NeedRotate();
    public bool NeedRotateToTarget();
    public void Stop();
    public void moveToAndRotateTo(Vector2 pos, Vector2 PointToRotate);
}
