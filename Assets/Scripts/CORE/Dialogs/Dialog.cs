using UnityEngine;

public class Dialog : MonoBehaviour
{
    public Animator animator;
    public CreatDialogSO dialogSO;
    bool val;
    GameObject _other;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _other = other.gameObject;
            val = true;

            if (other.TryGetComponent(out PlayerLevelOne _))
                other.GetComponent<PlayerLevelOne>().RotateToBilge();

            DialogUI.Instance.npcName = dialogSO.npcName;
            DialogController.Instance.StartConversation(dialogSO.dialogs);
            animator.SetTrigger("Talk");
        }
    }

    private void Update()
    {
        if (!val) return;
        if (Input.GetKeyDown(KeyCode.F))
        {
            DialogController.Instance.Next();
            if (DialogController.Instance.isFinish)
            {
                CloseDialog();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _other = other.gameObject;
            CloseDialog();
            val = false;
        }
    }

    private void CloseDialog()
    {
        DialogController.Instance.Reset();
        DialogUI.Instance.ShowDialog(false);
        animator.SetTrigger("Idle");
        if (_other.TryGetComponent(out PlayerLevelOne player)) player.TurnPortal();
    }
}