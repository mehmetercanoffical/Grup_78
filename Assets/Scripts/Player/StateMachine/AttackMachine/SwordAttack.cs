
using UnityEngine;

public class SwordAttack : AttackMachineBase
{
    public override void EnterState(AttackMachineBaseManager attack)
    {
        attack.Attack.runtimeAnimatorController = attack.SwordAttack;   
    }

    public override void ExitState(AttackMachineBaseManager attack)
    {

    }

    public override void UpdateState(AttackMachineBaseManager attack)
    {
        if (Input.GetMouseButtonDown(0))
        {
            attack.Attack.SetTrigger("ArrowAttack");
        }
    }
}
