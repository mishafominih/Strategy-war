using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class UnitInfoUI : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> buttons = new List<GameObject>();
    public iHighlighted highlighted;

    private TMP_Text textMesh;

    private AttackType attackType;
    private ButtonType buttonPressed = ButtonType.None;
    void Start()
    {
        textMesh = GetComponentInChildren<TMP_Text>();
        EventsManager.eventTouchHighlighted.AddListener(eventTouchHighlightedHandler);
        EventsManager.eventTouchEmpty.AddListener(eventTouchEmptyHandler);

        gameObject.SetActive(false);  // ������� ��� ������ �����������. ���������� �������� ��� ������

        foreach (GameObject button in buttons)
            button.SetActive(false);
    }

    private void eventTouchHighlightedHandler(iHighlighted highlightedItem)
    {
        if (highlighted == highlightedItem)  // ������ �� ��� �� ����, ������� ���������
        {
            UnHighlight();
        }
        else if (highlighted == null)  // ������ ������ ���, ������ ���������
        {
            highlighted = highlightedItem;
            gameObject.SetActive(true);

            foreach (AttackType button in highlighted.getButtleUnit().getAttackTypes())
                if (buttons.Count > (int)button)
                    buttons[(int)button].SetActive(true);

            highlighted.highlight();
            EventsManager.eventHighlight.Invoke(highlighted);
        }
        else if (buttonPressed == ButtonType.Attack)  // ������ �� ������ �����
        {
            if (highlighted.getButtleUnit().IsEnemy(highlightedItem.getButtleUnit()))
            {
                highlighted.getButtleUnit().Attack(highlightedItem.getButtleUnit(), attackType);
            }
            UnHighlight();
        }
        else  // ������ �� ������� �����, ��������� ��������� �� ����
        {
            highlighted.unHighlight();
            EventsManager.eventUnHighlight.Invoke(highlighted);

            highlighted = highlightedItem;

            highlighted.highlight();
            EventsManager.eventHighlight.Invoke(highlighted);
        }
    }

    private void UnHighlight()
    {
        buttonPressed = ButtonType.None;
        gameObject.SetActive(false);
        highlighted.unHighlight();
        EventsManager.eventUnHighlight.Invoke(highlighted);
        highlighted = null;
    }

    private void eventTouchEmptyHandler(Vector2 position)
    {
        if (highlighted != null)
        {
            if (buttonPressed == ButtonType.Move)
            {
                highlighted.moveTo(position);
            }

            UnHighlight();
        }
    }

    // Update is called once per frame
    void Update()
    {
        var buttleUnit = highlighted.getButtleUnit();
        textMesh.text = string.Format(
            "������: {0}\r\n������: {1}\r\n�����: {2}",
            (int)buttleUnit.getUnitCount(),
            buttleUnit.getDefencePoints(TypeTroops.infantry),
            buttleUnit.getAttackPoints()
        );
    }

    public void Shoot()
    {
        buttonPressed = ButtonType.Attack;
        attackType = AttackType.Fire;
    }

    public void HandButtle()
    {
        buttonPressed = ButtonType.Attack;
        attackType = AttackType.HandButtle;
    }
    public void Move()
    {
        buttonPressed = ButtonType.Move;
    }
}
