using UnityEngine;

public interface INPCManager
{
    NPCAttackBase nPCAttackBase { get; set; }
    Transform Target { get; set; }
    float currentDistance { get; set; }

}

public class DragonManager : MonoBehaviour, INPCManager
{
    public DragonAttack dragon = new();
    private NPCManager _npcManager;
    public RuntimeAnimatorController animOv;
    private Transform target;


    void Start()
    {
        _npcManager = GetComponent<NPCManager>();
        _npcManager.anim.runtimeAnimatorController = animOv;
        _npcManager.SetState(dragon);
        //_npcManager._npcMove.RemainingDistance();
    }



    public NPCAttackBase nPCAttackBase
    {
        get { return dragon; }
        set { dragon = (DragonAttack)value; }
    }

    public Transform Target
    {
        get { return target; }
        set { target = value; dragon._target = target; }
    }

    public float currentDistance { get => dragon.currentDistance; set => dragon.currentDistance = value; }
}

