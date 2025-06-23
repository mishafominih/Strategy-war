using UnityEngine;

public interface iButtleUnit
{
    public TypeTroops getType();
    public float getDefencePoints(TypeTroops typeTroops);
    public float getAttackPoints();
    public float getUnitCount();
    public bool Attack(iButtleUnit buttleUnit, AttackType attackType);
}
