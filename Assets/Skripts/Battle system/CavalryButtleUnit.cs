using System.Collections.Generic;
using UnityEngine;

public class CavalryButtleUnit : BaseButtleUnit
{
    private iMoveController moveController;
    private Rigidbody2D rb;
    protected override void Start()
    {
        base.Start();
        moveController = GetComponent<iMoveController>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            if (attackType == AttackType.HandButtle)
            {
                if (rb.IsTouching(target.GetCollider()))
                {
                    moveController.Stop();
                    var damage = UnitCount * damagePoints;
                    target.Damage(damage);
                }
                else if (!moveController.IsMove())
                {
                    moveController.moveTo(target.GetPosition());
                }
            }
        }
    }

    public override TypeTroops getType()
    {
        return TypeTroops.cavalry;
    }

    public override float GetMaxUnitCount()
    {
        return 4000;
    }

    public override List<AttackType> getAttackTypes()
    {
        return new List<AttackType>() { AttackType.HandButtle };
    }
}
