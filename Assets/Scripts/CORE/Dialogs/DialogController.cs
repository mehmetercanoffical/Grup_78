public class DialogController : Singleton<DialogController>
{
    private DialogsText[] sentences;
    private int index;
    public int currentSenteces = 0;
    public float typingSpeed;
    private string text;
    public bool isFinish = false;

    private void Start() => SetTextOnUI(string.Empty);

    public void SetTextOnUI(string text) => DialogUI.Instance.SetText(text);

    public void StartConversation(DialogsText[] conversation)
    {
        sentences = conversation;
        DialogUI.Instance.SetNowSpeak(sentences[index].isPlayer);
        DialogUI.Instance.ShowDialog(true);
        ShowText();
    }

    public void ShowText()
    {
        DialogUI.Instance.SetNowSpeak(sentences[currentSenteces].isPlayer);
        SetTextOnUI(sentences[currentSenteces].text);
    }

    public void NextText()
    {
        if (currentSenteces < sentences.Length - 1)
        {
            currentSenteces++;
            ShowText();
        }
        else
        {
            DialogUI.Instance.ShowDialog(false);
            SetTextOnUI(string.Empty);
            isFinish = true;
            currentSenteces = 0;
        }
    }




    //public void Next()
    //{

    //    if (DialogUI.Instance.GetText() == sentences[index].text) NextSentence();
    //    else
    //    {
    //        StopAllCoroutines();
    //        SetTextOnUI(sentences[index].text);

    //    }
    //}
    //IEnumerator Conversation()
    //{

    //    foreach (char i in sentences[index].text.ToCharArray())
    //    {
    //        string getText = DialogUI.Instance.GetText() + i.ToString();
    //        SetTextOnUI(getText);
    //        yield return new WaitForSeconds(typingSpeed);
    //    }
    //}

    //public void NextSentence()
    //{

    //    if (index < sentences.Length - 1)
    //    {
    //        index++;
    //        SetTextOnUI(string.Empty);
    //        DialogUI.Instance.SetNowSpeak(sentences[index].isPlayer);
    //        StartCoroutine(Conversation());
    //    }
    //    else
    //    {
    //        DialogUI.Instance.ShowDialog(false);
    //        SetTextOnUI(string.Empty);
    //        isFinish = true;
    //        index = 0;
    //    }
    //}

    internal void Reset()
    {
        DialogUI.Instance.ShowDialog(false);
        SetTextOnUI(string.Empty);
        index = 0;
    }
}