using System.Collections.Generic;
using UnityEngine;

public class CavalryButtleUnit : MonoBehaviour, iButtleUnit
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
    private iAnimationController animationController;
    void Start()
    {
        moveController = GetComponent<iMoveController>();
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        animationController = GetComponent<iAnimationController>();

        EventsManager.eventButtleUnitDie.AddListener(buttleUnitDieHandler);
    }

    private void buttleUnitDieHandler(iButtleUnit buttleUnit)
    {
        if (buttleUnit == target)
        {
            BreakAttack();
        }
    }

    public TypeTroops getType()
    {
        return TypeTroops.cavalry;
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

        return true;
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

    public Vector2 GetPosition()
    {
        return transform.position;
    }

    public void Damage(float damage)
    {
        var c = (damage / defensePoints) * Time.deltaTime * 0.01f;
        UnitCount -= c;
        if (UnitCount <= 0)
        {
            // Публикуем событие о своей смерти
            EventsManager.eventButtleUnitDie.Invoke(this);
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

    public float GetMaxUnitCount()
    {
        return 4000;
    }

    public List<AttackType> getAttackTypes()
    {
        return new List<AttackType>() { AttackType.HandButtle };
    }
}
