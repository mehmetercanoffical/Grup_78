using UnityEngine;

public class SoliderController : MonoBehaviour
{
    public GameObject other;
    public Animator anim;
    public string animName = "Attack";
    public string AnimOnStart = "Attack";
    public bool isAttackOnEvent = false;
    public bool isOtherAnimEventNot = false;
    public bool onStartEvent = false;

    private void Start()
    {
        if (onStartEvent)
            anim.SetTrigger(AnimOnStart);
    }
    public void AttackStart()
    {
        anim.SetTrigger(animName);

        if (!isAttackOnEvent)
            other.GetComponent<PeopleController>().Initialized();
    }

    public void AttackEnabled()
    {
        other.GetComponent<PeopleController>().Initialized();
    }

}
