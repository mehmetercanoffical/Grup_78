using System.Collections;
using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;

public class DialogController : Singleton<DialogController>
{
    private DialogsText[] sentences;
    private int index;
    public float typingSpeed;
    private string text;
    public bool isFinish = false;

    private void Start()
    {
        SetTextOnUI(string.Empty);
    }

    public void SetTextOnUI(string text)
    {
        DialogUI.Instance.SetText(text);
    }



    public void StartConversation(DialogsText[] conversation)
    {
        sentences = conversation;
        DialogUI.Instance.SetNowSpeak(sentences[index].isPlayer);
        DialogUI.Instance.ShowDialog(true);
        index = 0;
        StartCoroutine(Conversation());
    }

    public void Next()
    {
        if (DialogUI.Instance.GetText() == sentences[index].text)
        {
            NextSentence();
        }
        else
        {
            StopAllCoroutines();
            SetTextOnUI(sentences[index].text);
            Debug.Log("Here");
        }
    }
    IEnumerator Conversation()
    {

        foreach (char i in sentences[index].text.ToCharArray())
        {
            string getText = DialogUI.Instance.GetText() + i.ToString();
            SetTextOnUI(getText);
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        if (index < sentences.Length - 1)
        {
            index++;
            SetTextOnUI(string.Empty);
            Debug.Log(sentences[index].isPlayer);
            DialogUI.Instance.SetNowSpeak(sentences[index].isPlayer);
            StartCoroutine(Conversation());
        }
        else
        {
            DialogUI.Instance.ShowDialog(false);
            SetTextOnUI(string.Empty);
            isFinish = true;
            index = 0;
        }
    }

    internal void Reset()
    {
        DialogUI.Instance.ShowDialog(false);
        SetTextOnUI(string.Empty);
        index = 0;
    }
}