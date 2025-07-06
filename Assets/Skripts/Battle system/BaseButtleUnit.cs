using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BaseButtleUnit : MonoBehaviour, iButtleUnit
{
    [SerializeField]
    protected EnemyColor enemyColor;
    [SerializeField]
    protected float UnitCount, damagePoints, defensePoints;

    protected iButtleUnit target;
    protected AttackType attackType;
    protected virtual void Start()
    {
        EventsManager.eventButtleUnitDie.AddListener(buttleUnitDieHandler);
    }

    protected void buttleUnitDieHandler(iButtleUnit buttleUnit)
    {
        if (buttleUnit == target)
        {
            BreakAttack();
        }
    }

    public bool IsEnemy(iButtleUnit buttleUnit)
    {
        return enemyColor != buttleUnit.GetColor();
    }

    public EnemyColor GetColor()
    {
        return enemyColor;
    }

    public virtual TypeTroops getType()
    {
        throw new System.NotImplementedException();
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

    public virtual float GetMaxUnitCount()
    {
        return 0;
    }

    public bool Attack(iButtleUnit buttleUnit, AttackType attackType)
    {
        target = buttleUnit;
        this.attackType = attackType;

        return true;
    }

    public virtual void BreakAttack()
    {
        target = null;
    }

    public void Damage(float damage)
    {
        var c = (damage / defensePoints) * Time.deltaTime * 0.01f;
        UnitCount -= c;
        if (UnitCount <= 0)
        {
            // ��������� ������� � ����� ������
            EventsManager.eventButtleUnitDie.Invoke(this);
            Destroy(gameObject);
        }
    }

    public Vector2 GetPosition()
    {
        return transform.position;
    }

    public Collider2D GetCollider()
    {
        return GetComponent<Collider2D>();
    }

    public virtual List<AttackType> getAttackTypes()
    {
        return new List<AttackType>();
    }
}
