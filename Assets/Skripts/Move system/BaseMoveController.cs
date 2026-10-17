using UnityEditor.Tilemaps;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class BaseMoveController : MonoBehaviour, iMoveController
{
    [SerializeField] public float velocity;
    [SerializeField] public float speedRotation;

    protected Vector2 targetPosition;
    protected Rigidbody2D rb;
    protected Vector2 targetPointToRotate = Vector2.zero;  // Точка в направлении которой нужно повернуться когда закончили движение
    protected float targetAngle = 0;
    protected Vector2 endPointToRotate = Vector2.zero;  // Точка в направлении которой нужно повернуться когда закончили движение
    protected float endTargetAngle = 0;  // Дошли до точки и нужно повернуться в нужном направлении

    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetPosition = rb.position;
    }

    public void moveTo(Vector2 pos)
    {
        Stop();
        targetPosition = pos;
        targetPointToRotate = pos;
    }

    public bool NeedMove()
    {
        Vector2 distanceVector = targetPosition - rb.position;
        return distanceVector.sqrMagnitude > 0.0001f;
    }

    public void rotateTo(Vector2 pos)
    {
        Stop();
        targetPointToRotate = pos;
        targetAngle = calcTargetAngle(targetPointToRotate);
    }

    public bool NeedRotate()
    {
        targetAngle = calcTargetAngle(targetPointToRotate);
        return Mathf.Abs(targetAngle) > 1f;
    }

    public bool NeedRotateToTarget()
    {
        endTargetAngle = calcTargetAngle(endPointToRotate);
        return endPointToRotate != Vector2.zero && Mathf.Abs(endTargetAngle) > 1f;
    }

    public void Stop()
    {
        stopMove();
        targetPosition = transform.position;
        stopRotate();
    }

    protected float calcTargetAngle(Vector2 pos)
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

    protected void stopMove()
    {
        rb.linearVelocity = Vector2.zero;
    }

    protected void stopRotate()
    {
        targetAngle = 0;
        endPointToRotate = Vector2.zero;
    }

    protected float rotate(float angle)
    {
        var multiple = angle >= 0 ? 1 : -1;
        var deltaAngle = multiple * speedRotation * Time.fixedDeltaTime;
        rb.MoveRotation(rb.rotation + deltaAngle);
        return angle - deltaAngle;
    }

    protected void move()
    {
        Vector2 distanceVector = targetPosition - rb.position;
        rb.linearVelocity = distanceVector.normalized * velocity * Time.fixedDeltaTime;
    }

    public void moveToAndRotateTo(Vector2 pos, Vector2 PointToRotate)
    {
        moveTo(pos);
        endPointToRotate = PointToRotate;
    }
}
