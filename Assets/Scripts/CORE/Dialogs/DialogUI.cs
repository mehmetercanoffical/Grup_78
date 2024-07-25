using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogUI : Singleton<DialogUI>
{
    public GameObject DialogPanel;

    public TextMeshProUGUI CharackterName;
    public TextMeshProUGUI TextDisplay;

    public Image NowSpeakIcon;
    public Sprite PlayerIcon;
    public Sprite NpcIcon;
    public string playerName;

    [HideInInspector] public string npcName;


    private void Start() => SetText(string.Empty);

    public void ShowDialog(bool isActive)
    {
        DialogPanel.SetActive(isActive);
    }
    public void SetText(string conversation)
    {
        TextDisplay.SetText(conversation);
    }
    public string GetText() => TextDisplay.text;

    public void SetNowSpeak(bool isPlayer)
    {
        NowSpeakIcon.sprite = isPlayer ? PlayerIcon : NpcIcon;
        CharackterName.SetText(isPlayer ? playerName : npcName);

    }
}
