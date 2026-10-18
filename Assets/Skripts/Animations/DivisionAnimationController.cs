using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DivisionAnimationController : BaseAnimationController
{
    [SerializeField] protected GameObject smokeObject;
    [SerializeField] protected GameObject startSmokePosition, endSmokePosition;
    [SerializeField] protected GameObject attackEffectObject;

    public override void AttackEvent(AttackType attackType)
    {
        if (attackType == AttackType.Fire)
        {
            playEffect(smokeObject, startSmokePosition.transform, endSmokePosition.transform, 30);
        }
        if (attackType == AttackType.HandButtle)
        {
            //playEffect(attackEffectObject, startSmokePosition.transform, endSmokePosition.transform, 1);
        }
    }

    private void playEffect(GameObject obj, Transform startPosition, Transform endPosition, int count)
    {
        if (obj != null)
        {
            for (int i = 0; i < count; i++)
            {
                var lerp = Vector3.Lerp(startPosition.position, endPosition.position, UnityEngine.Random.Range(0, 1f));
                var new_obj = Instantiate(obj, lerp, transform.rotation, transform);
                Destroy(new_obj, 5);
            }
        }
    }
}
