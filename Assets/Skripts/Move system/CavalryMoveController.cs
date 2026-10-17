using UnityEngine;

public class CavalryMoveController : BaseMoveController
{
    void FixedUpdate()
    {
        if (NeedMove())
        {
            if (NeedRotate())  // Вначале повернем в направлении цели
            {
                targetAngle = rotate(targetAngle);
                stopMove();
            }
            else // Затем начнем движение
            {
                move();
            }
        }
        else
        {
            stopMove();
            if (NeedRotateToTarget()) // когда закончили идти - повернемся в направлении на заданную точку
            {
                endTargetAngle = rotate(endTargetAngle);
            }
            else
            {
                Stop();
            }
        }
    }
}
