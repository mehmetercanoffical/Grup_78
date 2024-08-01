using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{

    public DialogUI dialogUI;
    public Image healthBar;
    public Image healthBarBoss;
    public GameObject BossHealth;

    public GameObject ArrowCursour;
    public Image cursour;
    public Sprite cursourFirst;
    public Sprite cursourAir;
    public float _heath;

    public GameObject PlayerUI;


    public void ShowPlayer(bool val) => PlayerUI.SetActive(val);


    public void ChangeCursor(bool isFirst)
    {
        if (isFirst) cursour.sprite = cursourFirst;
        else cursour.sprite = cursourAir;
    }


    public void ArrowCursoure(bool val)
    {
        ArrowCursour.SetActive(val);
        ChangeCursor(true);
    }

    internal void UpdateHealthPlayer(float health)
    {

        healthBar.fillAmount = health;

        if (healthBar.fillAmount <= 0) return;

    }
    internal void UpdateBossPlayer(float health)
    {
        BossHealth.SetActive(true);
        healthBarBoss.fillAmount = health;

        if (healthBarBoss.fillAmount <= 0)
        {
            BossHealth.SetActive(false);
            return;
        }

    }
}
