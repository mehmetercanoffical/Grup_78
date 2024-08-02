using UnityEngine;

public class PeopleController : MonoBehaviour
{
    private Animator anim;
    public string animName = "Init";


    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Initialized()
    {
        GetComponent<Collider>().enabled = false;
        anim.SetTrigger(animName);
    }
}
