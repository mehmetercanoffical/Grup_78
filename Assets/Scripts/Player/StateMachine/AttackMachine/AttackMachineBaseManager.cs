using System.Collections.Generic;
using UnityEngine;
namespace AttackSystem
{
    public class AttackMachineBaseManager : MonoBehaviour
{

    [Header("Animator")]

    public Animator AttackAnim;
    public AnimatorOverrideController SwordAttack;
    public AnimatorOverrideController BowAttack;

    public AttackMachineBase currentState;
    public SwordAttack swordAttack = new SwordAttack();
    public ArcherAttack archerAttack = new ArcherAttack();
    public FreeAttack freeAttack = new FreeAttack();

    [Header("Bow Attack")]
    public GameObject Bow;
    public Transform BowHandle;
    public Transform BowBreak;
    public Archer archer;

    [Header("Sword Attack")]
    public GameObject Sword;
    public Transform SwordHandle;
    public Transform SwordBreak;
    public List<string> swordAttackAnim = new List<string>();

    [HideInInspector] public bool isSwordOnHandle = false;
    [HideInInspector] public bool isBowOnHandle = false;


    void Start() => SetState(freeAttack);

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) SetState(archerAttack);

        if (Input.GetKeyDown(KeyCode.Q)) SetState(swordAttack);

        currentState?.UpdateState(this);
    }

    void SetState(AttackMachineBase state)
    {
        currentState?.ExitState(this);
        currentState = state;

        gameObject.name = "Player - " + state.GetType().Name;
        currentState?.EnterState(this);

    }


    #region Get Attack Obj Region
    // DON'T DELETE
    public void GetSword()
    {

        if (isSwordOnHandle) AttackObject(Sword, SwordBreak);
        else AttackObject(Sword, SwordHandle);
        isSwordOnHandle = !isSwordOnHandle;
    }

    // DON'T DELETE
    public void GetBow()
    {
        if (isBowOnHandle) AttackObject(Bow, BowBreak);
        else AttackObject(Bow, BowHandle);
        isBowOnHandle = !isBowOnHandle;
    }

    void AttackObject(GameObject obj, Transform parent)
    {
        obj.transform.SetParent(parent);
        obj.transform.position = parent.position;
        obj.transform.rotation = parent.rotation;
    }

   
    #endregion

    public void DestroyArrow()
    {

        archer.DestroyArrow();
    }
}
}
