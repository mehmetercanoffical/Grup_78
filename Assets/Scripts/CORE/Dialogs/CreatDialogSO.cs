using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialog", menuName = "Dialogs/Dialog")]
public class CreatDialogSO : ScriptableObject
{
    public string npcName;
    public DialogsText[] dialogs;
}


[Serializable]
public struct DialogsText
{
    public string text;
    public bool isPlayer;

    public DialogsText(string text, bool isPlayer)
    {
        this.text = text;
        this.isPlayer = isPlayer;
    }
}


