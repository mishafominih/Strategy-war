using UnityEngine;

public class DivisionMoveController : MonoBehaviour, iMoveController
{
    [SerializeField] public float velocity;
    [SerializeField] public float speedRotation;

    private Vector2 targetPosition;
    private Rigidbody2D rb;
    private float targetAngle = 0;
    public void moveTo(Vector2 pos)
    {
        Stop();
        targetPosition = pos;
        targetAngle = calcTargetAngle(pos);
    }

    private float calcTargetAngle(Vector2 pos)
    {
        Vector2 distanceVector = pos - rb.position;
        var angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg - 90;
        var targetAngle = angle - rb.rotation;

        while (targetAngle < -180)
            targetAngle += 360;
        while (targetAngle > 180)
            targetAngle -= 360;
        return targetAngle;
    }

    public void Stop()
    {
        rb.linearVelocity = Vector2.zero;
        targetPosition = transform.position;
        targetAngle = 0;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetPosition = rb.position;
    }

    void FixedUpdate()
    {
        Vector2 distanceVector = targetPosition - rb.position;

        if (IsRotate())  // ������� ��������
        {
            rotate();
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

    private void rotate()
    {
        var multiple = targetAngle >= 0 ? 1 : -1;
        var deltaAngle = multiple * speedRotation * Time.fixedDeltaTime;
        rb.MoveRotation(rb.rotation + deltaAngle);
        targetAngle -= deltaAngle;
    }

    private void move(Vector2 distanceVector)
    {
        rb.linearVelocity = distanceVector.normalized * velocity * Time.fixedDeltaTime;
    }

    public bool IsMove()
    {
        return rb.linearVelocity.magnitude > 0.001f;
    }

    public void rotateTo(Vector2 pos)
    {
        targetAngle = calcTargetAngle(pos);
        Stop();
    }

    public bool IsRotate()
    {
        return Mathf.Abs(targetAngle) > 1f;
    }
}
