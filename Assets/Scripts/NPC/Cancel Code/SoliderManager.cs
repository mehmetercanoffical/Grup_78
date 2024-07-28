using UnityEngine;

public class SoliderManager : MonoBehaviour, INPCManager
{
    public SoliderAttack dragon = new();
    public RuntimeAnimatorController animOve;
    private NPCManager _npcManager;
    public Animator anim;
    private Transform target;


    void Awake()
    {
        _npcManager = GetComponent<NPCManager>();
        anim.runtimeAnimatorController = animOve;
        _npcManager.SetState(dragon);
    }



    public NPCAttackBase nPCAttackBase
    {
        get { return dragon; }
        set { dragon = (SoliderAttack)value; }
    }

    public Transform Target
    {
        get { return target; }
        set { target = value; dragon._target = target; }
    }

    public float currentDistance { get => dragon.currentDistance; set => dragon.currentDistance = value; }
}
