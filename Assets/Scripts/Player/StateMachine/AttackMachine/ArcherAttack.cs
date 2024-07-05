using UnityEngine;
using static ThirdPersonCamera;

public class ArcherAttack : AttackMachineBase
{
    public float ArcherBleend = 0.2f;
    private int archerBlendTitle = Animator.StringToHash("Arrow");
    private int arrowAttackTitle = Animator.StringToHash("ArrowAttack");
    private int disamArrowTitle = Animator.StringToHash("disamArrow");
    private int getArrowTitle = Animator.StringToHash("getArrow");
    public float lerpTime = 2f;
    float endValue = .2f;

    public override void EnterState(AttackMachineBaseManager attack)
    {

        attack.AttackAnim.runtimeAnimatorController = attack.BowAttack;

        //if (!attack.isBowOnHandle)
        //    attack.AttackAnim.SetTrigger(getArrowTitle);

    }

    public override void ExitState(AttackMachineBaseManager attack)
    {
        if (attack.isBowOnHandle)
            attack.AttackAnim.SetTrigger(disamArrowTitle);


    }
    public override void UpdateState(AttackMachineBaseManager attack)
    {
        if (Input.GetMouseButton(1))
            ThirdPersonCamera.Instance.SwitchCameraStyle(CameraStyle.Combat);
        else
            ThirdPersonCamera.Instance.SwitchCameraStyle(CameraStyle.Basic);


        if (Input.GetMouseButtonDown(0))
        {
            if (!attack.isBowOnHandle)
                attack.AttackAnim.SetTrigger(getArrowTitle);
            else 
            {
                attack.isBowOnHandle = true;
                attack.AttackAnim.SetFloat(archerBlendTitle, .2f);
            }

        }
        if (Input.GetMouseButton(0))
        {
            if(endValue <= 1) endValue += Mathf.Lerp(ArcherBleend, 1, lerpTime * Time.deltaTime);

            attack.AttackAnim.SetFloat(archerBlendTitle, endValue);
        }
        if (Input.GetMouseButtonUp(0))
        {
            endValue = .2f;
            attack.AttackAnim.SetTrigger(arrowAttackTitle);
            attack.AttackAnim.SetFloat(archerBlendTitle, 0f);

        }
    }


}
