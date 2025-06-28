using System;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class DivisionButtleUnit : MonoBehaviour, iButtleUnit
{
    [SerializeField]
    protected EnemyColor enemyColor;
    [SerializeField]
    protected float UnitCount, damagePoints, defensePoints;

    private iMoveController moveController;
    private iButtleUnit target;
    private AttackType attackType;
    private Rigidbody2D rb;
    private Collider2D collider;
    void Start()
    {
        moveController = GetComponent<DivisionMoveController>();
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
    }

    public TypeTroops getType()
    {
        return TypeTroops.infantry;
    }

    public float getDefencePoints(TypeTroops typeTroops)
    {
        return defensePoints;
    }

    public float getAttackPoints()
    {
        return damagePoints;
    }

    public float getUnitCount()
    {
        return UnitCount;
    }

    public bool Attack(iButtleUnit buttleUnit, AttackType attackType)
    {
        if (attackType == AttackType.HandButtle)
        {
            target = buttleUnit;
            this.attackType = attackType;
            moveController.moveTo(target.GetPosition());
        }
        else if (attackType == AttackType.Fire)
        {
            target = buttleUnit;
            this.attackType = attackType;
            if (allowedDistanceForFire())
            {
                moveController.rotateTo(target.GetPosition());
            }
            else
            {
                moveController.moveTo(target.GetPosition());
            }
        }

        return true;
    }

    private bool allowedDistanceForFire()
    {
        var distance = target.GetPosition() - GetPosition();
        return distance.magnitude < 3;
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            if (attackType == AttackType.HandButtle)
            {
                if (rb.IsTouching(target.GetCollider()))
                {
                    var damage = UnitCount * damagePoints;
                    target.Damage(damage);
                }
                else if (!moveController.IsMove())
                {
                    moveController.moveTo(target.GetPosition());
                }
            }
            if (attackType == AttackType.Fire)
            {
                if (!moveController.IsRotate() && allowedDistanceForFire())
                {
                    var damage = UnitCount * damagePoints / 4 / Time.deltaTime / 0.01f;
                    target.Damage(damage);
                    target = null;
                    moveController.Stop();
                }
            }
        }
    }

    public Vector2 GetPosition()
    {
        return transform.position;
    }

    public void Damage(float damage)
    {
        var c = (damage / defensePoints) * Time.deltaTime * 0.01f;
        Debug.Log(c);
        UnitCount -= c;
        if (UnitCount <= 0)
        {
            Destroy(gameObject);
        }
    }

    public Collider2D GetCollider()
    {
        return collider;
    }

    public void BreakAttack()
    {
        target = null;
    }

    public bool IsEnemy(iButtleUnit buttleUnit)
    {
        return enemyColor != buttleUnit.GetColor();
    }

    public EnemyColor GetColor()
    {
        return enemyColor;
    }
}
