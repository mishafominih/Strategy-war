using UnityEngine;

public class DivisionButtleUnit : MonoBehaviour, iButtleUnit
{
    [SerializeField]
    protected float UnitCount, damagePoints, defensePoints;
    public TypeTroops getType()
    {
        return TypeTroops.infantry;
    }

    public float getDefencePoints(TypeTroops typeTroops)
    {
        return defensePoints * UnitCount;
    }

    public float getAttackPoints()
    {
        return damagePoints * UnitCount;
    }
}
