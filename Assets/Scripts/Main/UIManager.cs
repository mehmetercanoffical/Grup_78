using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{

    public DialogUI dialogUI;
    public Image healthBar;
    public Color healthColor;

    public GameObject ArrowCursour;
    public Image cursour;
    public Sprite cursourFirst;
    public Sprite cursourAir;


    public void ChangeCursor(bool isFirst)
    {
        if (isFirst)
            cursour.sprite = cursourFirst;
        else
            cursour.sprite = cursourAir;
    }


    public void ArrowCursoure(bool val)
    {
        ArrowCursour.SetActive(val);
        ChangeCursor(true);
    }

    internal void UpdateHealthPlayer(float health)
    {


        healthBar.fillAmount = health;

        if (healthBar.fillAmount <= 0)
        {
            Debug.Log("Player is dead");
            return;
        }

    }
}
