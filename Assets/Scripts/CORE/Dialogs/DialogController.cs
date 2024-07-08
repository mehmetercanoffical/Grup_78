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

    private void Start()
    {
        SetTextOnUI(string.Empty);
    }

    public void SetTextOnUI(string text)
    {
        UIManager.Instance.SetText(text);
    }



    public void StartConversation(DialogsText[] conversation)
    {
        sentences = conversation;
        UIManager.Instance.SetNowSpeak(sentences[index].isPlayer);
        UIManager.Instance.ShowDialog(true);
        index = 0;
        StartCoroutine(Conversation());
    }

    public void Next()
    {
        if (UIManager.Instance.GetText() == sentences[index].text) NextSentence();
        else
        {
            StopAllCoroutines();
            SetTextOnUI(sentences[index].text);
        }
    }
    IEnumerator Conversation()
    {

        foreach (char i in sentences[index].text.ToCharArray())
        {
            string getText = UIManager.Instance.GetText() + i.ToString();
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
            UIManager.Instance.SetNowSpeak(sentences[index].isPlayer);
            StartCoroutine(Conversation());
        }
        else
        {
            UIManager.Instance.ShowDialog(false);
            SetTextOnUI(string.Empty);

            index = 0;
        }
    }

    internal void Reset()
    {
        UIManager.Instance.ShowDialog(false);
        SetTextOnUI(string.Empty);
        index = 0;
    }
}