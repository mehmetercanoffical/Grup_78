using UnityEngine;
using UnityEngine.UIElements;
using static ThirdPersonCamera;

namespace AttackSystem
{
    public class ArcherAttack : AttackMachineBase
    {
        private int arrowlastFire = Animator.StringToHash("ArrowAttack");
        private int disamArrowTitle = Animator.StringToHash("disamArrow");
        private int drawArrowTitle = Animator.StringToHash("drawArrow");
        private int getArrowTitle = Animator.StringToHash("getArrow");
        bool first = false;
        

        public override void EnterState(AttackMachineBaseManager attack)
        {
            attack.AttackAnim.runtimeAnimatorController = attack.BowAttack;
        }

        public override void ExitState(AttackMachineBaseManager attack)
        {
            if (attack.isBowOnHandle) attack.AttackAnim.SetTrigger(disamArrowTitle);

        }
        public override void UpdateState(AttackMachineBaseManager attack)
        {
            MouseOne();
            MouseZero(attack);
        }

        private void MouseZero(AttackMachineBaseManager attack)
        {
            if (PlayerMove.Instance.isPlayerMoving)
            {
                attack.DestroyArrow();
                return;
            }


            if (Input.GetMouseButtonDown(0))
            {
                if (attack.archer.arrow != null)
                {
                    attack.archer.DestroyArrow();
                    //attack.AttackAnim.SetTrigger(arrowlastFire);
                }
                if (!attack.isBowOnHandle)
                    attack.AttackAnim.SetTrigger(getArrowTitle);
                else
                {
                    if (!first) return;
                    attack.isBowOnHandle = true;
                    attack.AttackAnim.SetTrigger(drawArrowTitle);
                }

            }
            if (Input.GetMouseButton(0))
            {
                if (!first) return;
                PlayerMove.Instance.CombatShootTimeRotatetoLookingCamera();
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (!first)
                {
                    first = true;
                    return;
                }

                if (PlayerMove.Instance.isPlayerMoving)
                {
                    attack.DestroyArrow();
                    return;
                }
                else
                {
                    attack.AttackAnim.SetTrigger(arrowlastFire);
                    attack.archer.Shoot();
                }


            }
        }

        void MouseOne()
        {
            if (Input.GetMouseButton(1))
            {
                ThirdPersonCamera.Instance.SwitchCameraStyle(CameraStyle.Combat);

                //UIManager.Instance.ChangeCursor(false);
            }
            else
            {
                ThirdPersonCamera.Instance.SwitchCameraStyle(CameraStyle.Basic);
                //UIManager.Instance.ChangeCursor(true);
            }
        }
    }
}