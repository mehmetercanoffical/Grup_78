using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{

    [SerializeField] private GameObject DialogPanel;
    [SerializeField] private GameObject DialogPlayerPanel;

    [SerializeField] private TextMeshProUGUI CharackterName;
    [SerializeField] private TextMeshProUGUI TextDisplay;

    [SerializeField] private Image NowSpeakIcon;
    [SerializeField] private Sprite PlayerIcon;
    [SerializeField] private Sprite NpcIcon;
    [HideInInspector] public string npcName;
    public string playerName;

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
