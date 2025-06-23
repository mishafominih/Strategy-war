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
    public iButtleUnit buttleUnit;

    private TMP_Text textMesh;
    void Start()
    {
        textMesh = GetComponentInChildren<TMP_Text>();
        EventsManager.eventHighlight.AddListener(highltghted);
        EventsManager.eventUnHighlight.AddListener(unHighltghted);

        gameObject.SetActive(false);  // Вначале нет ничего выделенного. ОТображать панельку нет смысла

        foreach (GameObject button in buttons)
            button.SetActive(false);
    }

    private void highltghted(iHighlighted highlighted)
    {
        this.buttleUnit = highlighted.getButtleUnit();
        gameObject.SetActive(true);

        foreach (AttackType button in highlighted.getAttackTypes())
            if (buttons.Count > (int)button)
                buttons[(int)button].SetActive(true);
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
            "Солдат: {0}\r\nЗащита: {1}\r\nАтака: {2}",
            buttleUnit.getUnitCount(),
            buttleUnit.getDefencePoints(TypeTroops.infantry),
            buttleUnit.getAttackPoints()
        );
    }

    public void Shoot()
    {
        buttleUnit.Attack(buttleUnit, AttackType.Fire);
    }

    public void HandButtle()
    {
        buttleUnit.Attack(buttleUnit, AttackType.HandButtle);
    }
}
