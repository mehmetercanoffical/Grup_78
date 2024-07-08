using UnityEngine;

public class Dialog : MonoBehaviour
{
    public Animator animator;
    public CreatDialogSO dialogSO;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //animator.SetBool("talk", true);
            UIManager.Instance.npcName = dialogSO.npcName;
            DialogController.Instance.StartConversation(dialogSO.dialogs);
            Debug.Log(UIManager.Instance.npcName);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.F))
                DialogController.Instance.Next();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //animator.SetBool("talk", false);
            DialogController.Instance.Reset();
        }
    }
}