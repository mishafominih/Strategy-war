using System;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class DivisionButtleUnit : BaseButtleUnit
{
    private iMoveController moveController;
    private Rigidbody2D rb;
    private iAnimationController animationController;
    private Timer fireTimer = new Timer(5, 5).Start();  // Выстрел должен произойти сразу
    protected override void Start()
    {
        base.Start();
        moveController = GetComponent<iMoveController>();
        rb = GetComponent<Rigidbody2D>();
        animationController = GetComponent<iAnimationController>();
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
            if (attackType == AttackType.Fire)
            {
                fireTimer.Update();
                if (allowedDistanceForFire())
                {
                    moveController.rotateTo(target.GetPosition());
                }
                else
                {
                    moveController.moveTo(target.GetPosition());
                }
                if (!moveController.IsRotate() && allowedDistanceForFire())
                {
                    if (fireTimer.IsTime())
                    {
                        var damage = UnitCount * damagePoints / 4 / Time.deltaTime / 0.01f;
                        target.Damage(damage);
                        moveController.Stop();
                        animationController.AttackEvent(attackType);

                        fireTimer.Drop();
                    }
                }
            }
        }
    }

    private bool allowedDistanceForFire()
    {
        var distance = target.GetPosition() - GetPosition();
        return distance.magnitude < 3;
    }

    public override TypeTroops getType()
    {
        return TypeTroops.infantry;
    }

    public override void BreakAttack()
    {
        base.BreakAttack();
        fireTimer.Refresh();
    }

    public override float GetMaxUnitCount()
    {
        return 4000;
    }

    public override List<AttackType> getAttackTypes()
    {
        return new List<AttackType>() { AttackType.Fire, AttackType.HandButtle };
    }
}
