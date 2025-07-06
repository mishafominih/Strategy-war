using UnityEngine;

public class CavalryMoveController : BaseMoveController
{
    void FixedUpdate()
    {
        Vector2 distanceVector = targetPosition - rb.position;

        if (IsRotate())  // Вначале повернем
        {
            rotate();
            stopMove();
        }
        else if (distanceVector.sqrMagnitude > 0.0001f) // Затем начнем движение
        {
            move(distanceVector);
        }
        else  // Дошли. Остановимся
        {
            Stop();
        }
    }
}
