using UnityEngine;

public class Dialog : MonoBehaviour
{
    public Animator animator;
    public CreatDialogSO dialogSO;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent(out PlayerLevelOne player))
                other.GetComponent<PlayerLevelOne>().RotateToBilge();

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
            {
                 DialogController.Instance.Next();
                if (DialogController.Instance.isFinish) CloseDialog(other);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CloseDialog(other);
        }
    }

    private void CloseDialog(Collider other)
    {
        DialogController.Instance.Reset();
        DialogUI.Instance.ShowDialog(false);
        animator.SetTrigger("Idle");
        if (other.TryGetComponent(out PlayerLevelOne player))
            player.TurnPortal();
    }
}