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
    private bool isCommand = false;
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
            highlighted = null;
            isCommand = false;
            gameObject.SetActive(false);
            highlightedItem.unHighlight();
            EventsManager.eventUnHighlight.Invoke(highlightedItem);
        }
        else if (highlighted == null)  // Нажали первый раз, ставим выделение
        {
            highlighted = highlightedItem;
            gameObject.SetActive(true);

            foreach (AttackType button in highlighted.getAttackTypes())
                if (buttons.Count > (int)button)
                    buttons[(int)button].SetActive(true);

            highlighted.highlight();
            EventsManager.eventHighlight.Invoke(highlighted);
        }
        else if (isCommand)  // Нажали на кнопку атаки
        {
            isCommand = false;
            highlighted.getButtleUnit().Attack(highlightedItem.getButtleUnit(), attackType);
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

    private void eventTouchEmptyHandler(Vector2 position)
    {
        if (highlighted != null)
        {
            highlighted.moveTo(position);

            isCommand = false;
            gameObject.SetActive(false);
            highlighted.unHighlight();
            EventsManager.eventUnHighlight.Invoke(highlighted);
            highlighted = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var buttleUnit = highlighted.getButtleUnit();
        textMesh.text = string.Format(
            "Солдат: {0}\r\nЗащита: {1}\r\nАтака: {2}",
            buttleUnit.getUnitCount(),
            buttleUnit.getDefencePoints(TypeTroops.infantry),
            buttleUnit.getAttackPoints()
        );
    }

    public void Shoot()
    {
        isCommand = true;
        attackType = AttackType.Fire;
    }

    public void HandButtle()
    {
        isCommand = true;
        attackType = AttackType.HandButtle;
    }
}
