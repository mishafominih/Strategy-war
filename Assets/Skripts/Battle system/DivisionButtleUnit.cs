using System;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class DivisionButtleUnit : BaseButtleUnit
{
    private iMoveController moveController;
    [SerializeField] private BoxCollider2D attackCollider;
    private iAnimationController animationController;
    private Timer fireTimer = new Timer(5, 5).Start();  // Выстрел должен произойти сразу
    protected override void Start()
    {
        base.Start();
        moveController = GetComponent<iMoveController>();
        animationController = GetComponent<iAnimationController>();
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
                else
                {
                    moveController.MoveTo(target.GetPosition());
                }
            }
            if (attackType == AttackType.Fire)
            {
                fireTimer.Update();
                if (allowedDistanceForFire())
                {
                    if (moveController.NeedRotateTo(target.GetPosition()))
                    {
                        moveController.RotateTo(target.GetPosition());
                    }
                    else
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
                else
                {
                    moveController.MoveTo(target.GetPosition());
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
