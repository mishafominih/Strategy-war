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

        gameObject.SetActive(false);  // Вначале нет ничего выделенного. ОТображать панельку нет смысла

        foreach (GameObject button in buttons)
            button.SetActive(false);
    }

    private void eventTouchHighlightedHandler(iHighlighted highlightedItem)
    {
        if (highlighted == highlightedItem)  // Нажали на тот же юнит, снимаем выделение
        {
            UnHighlight();
        }
        else if (highlighted == null)  // Нажали первый раз, ставим выделение
        {
            highlighted = highlightedItem;
            gameObject.SetActive(true);

            foreach (AttackType button in highlighted.getButtleUnit().getAttackTypes())
                if (buttons.Count > (int)button)
                    buttons[(int)button].SetActive(true);

            highlighted.highlight();
            EventsManager.eventHighlight.Invoke(highlighted);
        }
        else if (buttonPressed == ButtonType.Attack)  // Нажали на кнопку атаки
        {
            if (highlighted.getButtleUnit().IsEnemy(highlightedItem.getButtleUnit()))
            {
                highlighted.getButtleUnit().Attack(highlightedItem.getButtleUnit(), attackType);
            }
            UnHighlight();
        }
        else  // Нажали на другого юнита, перекинем выделение на него
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
            "Солдат: {0}\r\nЗащита: {1}\r\nАтака: {2}",
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
