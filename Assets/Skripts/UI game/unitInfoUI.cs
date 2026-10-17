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


        UnHighlight(null);  // Вначале нет ничего выделенного. ОТображать панельку нет смысла
        EventsManager.eventButtleUnitDie.AddListener(buttleUnitDieHandler);
    }

    private void eventTouchHighlightedHandler(List<iHighlighted> highlighteds)
    {
        if (buttonPressed == ButtonType.Attack)  // Нажали на кнопку атаки
        {
            var highlighted_item = highlighteds[0];
            if (highlighted[0].getButtleUnit().IsEnemy(highlighted_item.getButtleUnit()))
            {
                foreach (var h in highlighted)
                    h.getButtleUnit().Attack(highlighted_item.getButtleUnit(), attackType);
            }
            UnHighlight(null);
        }
        else 
        {
            bool has_unhighlited = false;
            foreach (var highlighted_item in highlighteds)
            {
                if (!highlighted.Contains(highlighted_item))
                    has_unhighlited = true;
            }
            if (has_unhighlited)
            {
                foreach (var highlighted_item in highlighteds)
                    if (!highlighted.Contains(highlighted_item))
                        Highlight(highlighted_item);
            }
            else
            {
                foreach (var highlighted_item in highlighteds)
                    if (highlighted.Contains(highlighted_item))
                        UnHighlight(highlighted_item);
            }
        }
    }

    private void Highlight(iHighlighted highlighted_item)
    {
        if (highlighted.Count > 0)
        {
            //var isOneType = highlighted[0].getButtleUnit().getType() == highlightedItem.getButtleUnit().getType();
            var isOneColor = highlighted[0].getButtleUnit().GetColor() == highlighted_item.getButtleUnit().GetColor();
            if (!isOneColor)
            {
                UnHighlight(null);
            }
        }
        highlighted.Add(highlighted_item);

        gameObject.SetActive(true);

        foreach (AttackType button in highlighted_item.getButtleUnit().getAttackTypes())
            if (buttons.Count > (int)button)
                buttons[(int)button].SetActive(true);

        highlighted_item.highlight();
        EventsManager.eventHighlight.Invoke(highlighted_item);
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
            foreach (GameObject button in buttons)
                button.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    private void eventTouchEmptyHandler(Vector2 startTouchPosition, Vector2 endTouchPosition)
    {
        if (highlighted != null)
        {
            if (buttonPressed == ButtonType.Move)
            {
                foreach (var h in highlighted)
                    if (startTouchPosition == endTouchPosition)
                        h.moveTo(startTouchPosition);
                    else
                        h.moveToAndRotateTo(startTouchPosition, endTouchPosition);
            }

            UnHighlight(null);
        }
    }

    protected void buttleUnitDieHandler(iButtleUnit buttleUnit)
    {
        foreach (var h in highlighted)
            if (h.getButtleUnit() == buttleUnit)
            {
                UnHighlight(h);
                break;
            }
    }

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
            defPoints / highlighted.Count,
            attackPoints / highlighted.Count
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
