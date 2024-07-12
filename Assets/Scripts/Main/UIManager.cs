using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{

    public DialogUI dialogUI;
    public Image healthBar;
    public Color healthColor;

    internal void UpdateHealthPlayer(float health)
    {
        Debug.Log("Health: " + health);

        if (health <= 0)
        {
            Debug.Log("Player is dead");
            return;
        }


        //healthBar.fillAmount = health;
        //if (health < 30)
        //    healthBar.color = Color.red;
        //else
        //    healthBar.color = healthColor;

    }
}
