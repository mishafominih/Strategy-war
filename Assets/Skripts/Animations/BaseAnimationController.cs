using System;
using UnityEngine;

public class BaseAnimationController : MonoBehaviour, iAnimationController
{
    protected iButtleUnit buttleUnit;

    public virtual void AttackEvent(AttackType attackType) { }

    void Start()
    {
        buttleUnit = GetComponent<iButtleUnit>();
    }

    void FixedUpdate()
    {
        UpdateScale();
    }

    protected void UpdateScale()
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
