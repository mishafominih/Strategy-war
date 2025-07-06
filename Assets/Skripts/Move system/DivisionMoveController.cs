using UnityEngine;

public class DivisionMoveController : BaseMoveController
{
    void FixedUpdate()
    {
        Vector2 distanceVector = targetPosition - rb.position;

        if (IsRotate())  // ������� ��������
        {
            rotate();
            stopMove();
        }
        else if (distanceVector.sqrMagnitude > 0.0001f) // ����� ������ ��������
        {
            move(distanceVector);
        }
        else  // �����. �����������
        {
            Stop();
        }
    }
}
