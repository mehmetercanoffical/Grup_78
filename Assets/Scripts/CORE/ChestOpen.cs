    using System.Collections;
using System.Collections.Generic;
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
        LevelManager.Instance.NextLevel();
    }
}
