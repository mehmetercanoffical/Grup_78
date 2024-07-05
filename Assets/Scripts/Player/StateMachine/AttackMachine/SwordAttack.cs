
using System;
using UnityEngine;

public class SwordAttack : AttackMachineBase
{

    private int currentAttack;
    int attackCount = 0;

    private int swordAttack = Animator.StringToHash("swordAttack");
    private int getSword = Animator.StringToHash("getSwordAttack");
    private int holdSword = Animator.StringToHash("holdSwordAttack");
    private int disamSword = Animator.StringToHash("disamSword");



    public override void EnterState(AttackMachineBaseManager attack)
    {
        attack.AttackAnim.runtimeAnimatorController = attack.SwordAttack;

        //if (!attack.isSwordOnHandle)
        //    CallAnimation(attack, getSword);
    }

    public override void ExitState(AttackMachineBaseManager attack)
    {
        if (attack.isSwordOnHandle)
            CallAnimation(attack, disamSword);
    }

    public override void UpdateState(AttackMachineBaseManager attack)
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!attack.isSwordOnHandle)
                attack.AttackAnim.SetTrigger(getSword);
            else
            {
                int attackAnim = Animator.StringToHash(attack.swordAttackAnim[attackCount]);
                ChangeSwordAttackingCrossFide(attack, attackAnim);
            }

        }
        if (Input.GetMouseButton(0))
        {

            // Shield
            if (attack.isSwordOnHandle)
            {
                //CallAnimation(attack, holdSword);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            attackCount += 1;
            // Lenght of the attack animation attackCount = 0

            if (attackCount >= attack.swordAttackAnim.Count) attackCount = 0;

        }

        MouseDowmOne(attack);

    }


    private void MouseDowmOne(AttackMachineBaseManager attack)
    {
        if (Input.GetMouseButtonDown(1))
        {
            //attack.AttackAnim.SetTrigger(shield);
        }

        if (Input.GetMouseButtonUp(1)) CallAnimation(attack, holdSword);
    }

    void CallAnimation(AttackMachineBaseManager attack, int title) => attack.AttackAnim.SetTrigger(title);

    public void ChangeSwordAttackingCrossFide(AttackMachineBaseManager attack, int nextAnim)
    {

        if (currentAttack == nextAnim) return;
        currentAttack = nextAnim;
        attack.AttackAnim.CrossFadeInFixedTime(currentAttack, 0.1f);
        Debug.Log(currentAttack);

    }


}
