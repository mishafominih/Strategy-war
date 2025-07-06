using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class UnitInfoUI : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> buttons = new List<GameObject>();
    public List<iHighlighted> highlighted = new List<iHighlighted>();

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
        if (buttonPressed == ButtonType.Attack)  // Нажали на кнопку атаки
        {
            if (highlighted[0].getButtleUnit().IsEnemy(highlightedItem.getButtleUnit()))
            {
                foreach (var h in highlighted)
                    h.getButtleUnit().Attack(highlightedItem.getButtleUnit(), attackType);
            }
            UnHighlight(null);
        }
        else if (highlighted.Contains(highlightedItem))  // Нажали на тот же юнит, снимаем выделение
        {
            UnHighlight(highlightedItem);
        }
        else  // Нажали первый раз, ставим выделение
        {
            highlighted.Add(highlightedItem);
            gameObject.SetActive(true);

            foreach (AttackType button in highlightedItem.getButtleUnit().getAttackTypes())
                if (buttons.Count > (int)button)
                    buttons[(int)button].SetActive(true);

            highlightedItem.highlight();
            EventsManager.eventHighlight.Invoke(highlightedItem);
        }
    }

    private void UnHighlight(iHighlighted highlightedItem)
    {
        if (highlightedItem != null)
        {
            highlightedItem.unHighlight();
            EventsManager.eventUnHighlight.Invoke(highlightedItem);
            highlighted.Remove(highlightedItem);
        }
        else
        {
            foreach (var h in new List<iHighlighted>(highlighted))
                UnHighlight(h);
        }
        if (highlighted.Count == 0)
        {
            buttonPressed = ButtonType.None;
            gameObject.SetActive(false);
        }
    }

    private void eventTouchEmptyHandler(Vector2 position)
    {
        if (highlighted != null)
        {
            if (buttonPressed == ButtonType.Move)
            {
                foreach (var h in highlighted)
                    h.moveTo(position);
            }

            UnHighlight(null);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float unitCount = 0, defPoints = 0, attackPoints = 0;
        foreach (var h in highlighted)
        {
            var buttleUnit = h.getButtleUnit();
            unitCount += buttleUnit.getUnitCount();
            defPoints += buttleUnit.getDefencePoints(TypeTroops.infantry);
            attackPoints += buttleUnit.getAttackPoints();
        }
        textMesh.text = string.Format(
            "Солдат: {0}\r\nЗащита: {1}\r\nАтака: {2}",
            (int)unitCount,
            defPoints,
            attackPoints
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
