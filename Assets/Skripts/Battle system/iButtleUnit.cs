using System.Collections.Generic;
using UnityEngine;

public interface iButtleUnit
{
    public TypeTroops getType();
    public float getDefencePoints(TypeTroops typeTroops);
    public float getAttackPoints();
    public float getUnitCount();
    public float GetMaxUnitCount();
    public bool Attack(iButtleUnit buttleUnit, AttackType attackType);
    public void BreakAttack();
    public void Damage(float damage);
    public Vector2 GetPosition();
    public Collider2D GetCollider();
    public bool IsEnemy(iButtleUnit buttleUnit);
    public EnemyColor GetColor();
    public List<AttackType> getAttackTypes();
}
