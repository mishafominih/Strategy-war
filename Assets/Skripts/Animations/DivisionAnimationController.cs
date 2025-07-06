using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DivisionAnimationController : BaseAnimationController
{
    [SerializeField] protected GameObject smokeObject;
    [SerializeField] protected GameObject startSmokePosition, endSmokePosition;

    public override void AttackEvent(AttackType attackType)
    {
        if (attackType == AttackType.Fire)
        {
            if (smokeObject != null)
            {
                for (int i = 0; i < 30; i++)
                {
                    var lerp = Vector3.Lerp(startSmokePosition.transform.position, endSmokePosition.transform.position, UnityEngine.Random.Range(0, 1f));
                    var new_obj = Instantiate(smokeObject, lerp, transform.rotation, transform);
                    Destroy(new_obj, 5);
                }
            }
        }
    }
}
