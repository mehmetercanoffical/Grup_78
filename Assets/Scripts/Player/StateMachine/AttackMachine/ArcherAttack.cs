using UnityEngine;
using static ThirdPersonCamera;

public class ArcherAttack : AttackMachineBase
{
    public override void EnterState(AttackMachineBaseManager attack)
    {
        attack.Attack.runtimeAnimatorController = attack.BowAttack;
    }

    public override void ExitState(AttackMachineBaseManager attack)
    {

    }

    public override void UpdateState(AttackMachineBaseManager attack)
    {
        if (Input.GetMouseButton(1))
        {
            ThirdPersonCamera.Instance.SwitchCameraStyle(CameraStyle.Combat);
        }
        else
            ThirdPersonCamera.Instance.SwitchCameraStyle(CameraStyle.Basic);
    }
}

