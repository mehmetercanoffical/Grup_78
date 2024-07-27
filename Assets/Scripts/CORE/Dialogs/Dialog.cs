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
            DialogUI.Instance.npcName = dialogSO.npcName;
            DialogController.Instance.StartConversation(dialogSO.dialogs);
            animator.SetTrigger("Talk");    
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
            DialogUI.Instance.ShowDialog(false);
            animator.SetTrigger("Idle");
        }
    }
}