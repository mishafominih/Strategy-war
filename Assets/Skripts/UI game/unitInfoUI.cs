using TMPro;
using UnityEngine;

public class UnitInfoUI : MonoBehaviour
{
    public iButtleUnit buttleUnit;

    private TMP_Text textMesh;
    void Start()
    {
        textMesh = GetComponentInChildren<TMP_Text>();
        EventsManager.eventHighlight.AddListener(highltghted);
        EventsManager.eventUnHighlight.AddListener(unHighltghted);

        gameObject.SetActive(false);  // ������� ��� ������ �����������. ���������� �������� ��� ������
    }

    private void highltghted(iHighlighted highlighted)
    {
        this.buttleUnit = highlighted.getButtleUnit();
        gameObject.SetActive(true);
    }

    private void unHighltghted()
    {
        this.buttleUnit = null;
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        textMesh.text = string.Format(
            "�����������: {0}\r\n������: {1}\r\n�����: {2}",
            buttleUnit.getUnitCount(),
            buttleUnit.getDefencePoints(TypeTroops.infantry),
            buttleUnit.getAttackPoints()
        );
    }
}
