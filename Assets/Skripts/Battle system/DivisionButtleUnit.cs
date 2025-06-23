using TMPro;
using UnityEngine;

public class DivisionButtleUnit : MonoBehaviour, iButtleUnit
{
    [SerializeField]
    protected float UnitCount, damagePoints, defensePoints;

    private iMoveController moveController;
    private iButtleUnit target;
    private AttackType attackType;
    void Start()
    {
        moveController = GetComponent<DivisionMoveController>();
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
        target = buttleUnit;
        this.attackType = attackType;
        moveController.moveTo(target.GetPosition());

        return true;
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            if (!moveController.IsMove())
            {
                target.Damage();
            }
        }
    }

    public Vector2 GetPosition()
    {
        return transform.position;
    }

    public void Damage()
    {
        return;
    }
}
