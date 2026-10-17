
using UnityEngine;

public class BaseMoveController : MonoBehaviour, iMoveController
{
    [SerializeField] public float velocity;
    [SerializeField] public float speedRotation;

    protected Rigidbody2D rb;

    protected Vector2 targetPosition = Vector2.zero;
    protected Vector2 targetPointToRotate = Vector2.zero;  // “очка в направлении которой нужно повернутьс€


    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (!IsActive())
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        if (NeedMove())
        {
            var targetAngle = calcTargetAngle(targetPosition);
            if (Mathf.Abs(targetAngle) > 1f)
                rotate(targetAngle);
            else
                move();
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            var targetAngle = calcTargetAngle(targetPointToRotate);
            if (targetPointToRotate != Vector2.zero && Mathf.Abs(targetAngle) > 1f)
                rotate(targetAngle);
            else
                Stop();
        }
    }

    public void MoveTo(Vector2 pos)
    {
        Stop();
        rb.freezeRotation = false;

        targetPosition = pos;
    }

    public bool NeedMove()
    {
        Vector2 distanceVector = targetPosition - rb.position;
        return targetPosition != Vector2.zero && distanceVector.sqrMagnitude > 0.001f;
    }

    public void RotateTo(Vector2 pos)
    {
        Stop();
        rb.freezeRotation = false;

        targetPointToRotate = pos;
    }

    public bool NeedRotate()
    {
        var targetAngle = calcTargetAngle(targetPointToRotate);
        return targetPointToRotate != Vector2.zero && Mathf.Abs(targetAngle) > 1f;
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

    public void Stop()
    {
        rb.linearVelocity = Vector2.zero;
        rb.freezeRotation = true;

        targetPosition = Vector2.zero;
        targetPointToRotate = Vector2.zero;
    }

    protected void rotate(float angle)
    {
        var multiple = angle >= 0 ? 1 : -1;
        var deltaAngle = multiple * speedRotation * Time.fixedDeltaTime;
        rb.MoveRotation(rb.rotation + deltaAngle);
    }

    protected void move()
    {
        if (targetPosition == Vector2.zero)
            return;

        Vector2 distanceVector = targetPosition - rb.position;
        rb.linearVelocity = distanceVector.normalized * velocity * Time.fixedDeltaTime;
    }

    public void MoveToAndRotateTo(Vector2 pos, Vector2 PointToRotate)
    {
        MoveTo(pos);
        targetPointToRotate = PointToRotate;
    }

    public bool IsActive()
    {
        return targetPosition != Vector2.zero || targetPointToRotate != Vector2.zero;
    }
}
