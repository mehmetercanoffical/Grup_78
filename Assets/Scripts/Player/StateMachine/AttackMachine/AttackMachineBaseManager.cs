using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMachineBaseManager : MonoBehaviour
{

    
    public Animator Attack;

    public AnimatorOverrideController SwordAttack;
    public AnimatorOverrideController BowAttack;


    public AttackMachineBase currentState;
    public SwordAttack swordAttack = new SwordAttack();
    public ArcherAttack ArcherAttack = new ArcherAttack();



    void Start() => SetState(swordAttack);

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
            SetState(ArcherAttack);

        if (Input.GetKeyDown(KeyCode.Q))
            SetState(swordAttack);

        currentState?.UpdateState(this);
    }

    void SetState(AttackMachineBase state)
    {
        currentState?.ExitState(this);

        currentState = state;
        gameObject.name = "Player - " + state.GetType().Name;

        currentState?.EnterState(this);

    }
}
