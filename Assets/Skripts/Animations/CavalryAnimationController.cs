using System;
using UnityEngine;

public class CavalryAnimationController : MonoBehaviour, iAnimationController
{
    private iButtleUnit buttleUnit;

    public void AttackEvent(AttackType attackType)
    {

    }

    void Start()
    {
        buttleUnit = GetComponent<iButtleUnit>();
    }

    void FixedUpdate()
    {
        if (buttleUnit != null)
        {
            // Регулируем размер
            var k = buttleUnit.getUnitCount() / buttleUnit.GetMaxUnitCount();  // от 0 до 1
            k = (float)Math.Round(k, 1);  // Округляем до первого знака после запятой
            var scale = 0.5f + k / 2;
            transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
