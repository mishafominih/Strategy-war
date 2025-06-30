using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DivisionAnimationController : MonoBehaviour, iAnimationController
{
    [SerializeField] protected GameObject smokeObject;
    [SerializeField] protected GameObject startSmokePosition, endSmokePosition;

    private iButtleUnit buttleUnit;
    public void AttackEvent(AttackType attackType)
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

    void Start()
    {
        buttleUnit = GetComponent<DivisionButtleUnit>();
    }

    void FixedUpdate()
    {
        if (buttleUnit != null)
        {
            var k = buttleUnit.getUnitCount() / buttleUnit.GetMaxUnitCount();  // от 0 до 1
            var scale = 0.5f + k / 2;
            transform.localScale = new Vector3(scale, scale, scale);
        }
    }

}
