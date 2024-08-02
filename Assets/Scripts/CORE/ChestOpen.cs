using System.Collections;
using UnityEngine;

public class ChestOpen : MonoBehaviour
{
    public Animator anim;
    public void Open()
    {
        anim.SetTrigger("Open");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            Open();
    }

    public void NextLevel()
    {
        StartCoroutine(LevelStart());
    }

    IEnumerator LevelStart()
    {
        yield return new WaitForSeconds(1f);
        LevelManager.Instance.NextLevel();
    }
}
