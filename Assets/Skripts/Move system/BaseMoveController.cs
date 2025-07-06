using UnityEngine;

public class BaseMoveController : MonoBehaviour, iMoveController
{
    [SerializeField] public float velocity;
    [SerializeField] public float speedRotation;

    protected Vector2 targetPosition;
    protected Rigidbody2D rb;
    protected float targetAngle = 0;

    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetPosition = rb.position;
    }

    public void moveTo(Vector2 pos)
    {
        Stop();
        targetPosition = pos;
        targetAngle = calcTargetAngle(pos);
    }

    public bool IsMove()
    {
        return rb.linearVelocity.magnitude > 0.001f;
    }

    public void rotateTo(Vector2 pos)
    {
        Stop();
        targetAngle = calcTargetAngle(pos);
    }

    public bool IsRotate()
    {
        return Mathf.Abs(targetAngle) > 1f;
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
    }

    protected void rotate()
    {
        var multiple = targetAngle >= 0 ? 1 : -1;
        var deltaAngle = multiple * speedRotation * Time.fixedDeltaTime;
        rb.MoveRotation(rb.rotation + deltaAngle);
        targetAngle -= deltaAngle;
    }

    protected void move(Vector2 distanceVector)
    {
        rb.linearVelocity = distanceVector.normalized * velocity * Time.fixedDeltaTime;
    }

}
