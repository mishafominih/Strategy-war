using System.Collections.Generic;
using UnityEngine;

public class CavalryButtleUnit : BaseButtleUnit
{
    [SerializeField] private BoxCollider2D attackCollider;
    private iMoveController moveController;
    protected override void Start()
    {
        base.Start();
        moveController = GetComponent<iMoveController>();
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            if (attackType == AttackType.HandButtle)
            {
                if (attackCollider.IsTouching(target.GetCollider()))
                {
                    if (moveController.NeedRotateTo(target.GetPosition()))
                        moveController.RotateTo(target.GetPosition());
                    else
                        moveController.Stop();

                    var damage = UnitCount * damagePoints;
                    target.Damage(damage);
                }
                else if (!moveController.NeedMove())
                {
                    moveController.MoveTo(target.GetPosition());
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
