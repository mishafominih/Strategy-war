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
            // ���������� ������
            var k = buttleUnit.getUnitCount() / buttleUnit.GetMaxUnitCount();  // �� 0 �� 1
            k = (float)Math.Round(k, 1);  // ��������� �� ������� ����� ����� �������
            var scale = 0.5f + k / 2;
            transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
