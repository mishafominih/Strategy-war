using UnityEngine;

public interface iMoveController
{
    public void MoveTo(Vector2 pos);
    public bool NeedMove();
    public void RotateTo(Vector2 pos);
    public bool NeedRotateTo(Vector2 pos);
    public bool IsActive();
    public void MoveToAndRotateTo(Vector2 pos, Vector2 PointToRotate);
    public void Stop();
}
