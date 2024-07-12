using UnityEngine;
using NPCSpace;
using Unity.VisualScripting;
public class DragonAttack : NPCAttackBase
{

    private int _attack = Animator.StringToHash("Attack"); // NEAR
    private int _die = Animator.StringToHash("Die");
    private int _takeDamage = Animator.StringToHash("TakeDamage");
    private int _far = Animator.StringToHash("Far");
    private int _near = Animator.StringToHash("Near"); // ATTACK
    internal Transform _target;

    public float currentDistance;

    public override void Start(NPCManager manager)
    {
    }
    public override void Update(NPCManager manager)
    {
        if (_target == null) return;

        float distance = Vector3.Distance(manager.transform.position, _target.position);

        if (manager.isAttacking == true) return;

        if (manager.attackType == ATTACKTYPE.FAR)
        {
            manager.isAttacking = true;
            FarPlayer(manager);
            currentDistance = distance;
        }
        else if (manager.attackType == ATTACKTYPE.NEAR)
        {
            manager.isAttacking = true;
            NearPlayer(manager);
            currentDistance = distance;
        }
        else if (manager.attackType == ATTACKTYPE.SONEAR)
        {
            manager.isAttacking = true;
            AttackPlayer(manager);
            currentDistance = distance;
        }
        //else
        //{
        //    checkDistanceForAttackType(manager, distance, count);
        //}

    }




    public override void AttackPlayer(NPCManager manager)
    {
        if (manager.isAttacking)
        {
            manager.anim.SetTrigger(_near);
        }
    }

    public override void FarPlayer(NPCManager manager)
    {
        if (manager.isAttacking)
        {
            Debug.Log("Far");
            manager.anim.SetTrigger(_far);
        }
    }

    public override void NearPlayer(NPCManager manager)
    {

        if (manager.isAttacking)
        {
            Debug.Log("Near");
            manager.anim.SetTrigger(_attack);
        }
    }

    public override void SayBaseManager(NPCBaseManager basemanager)
    {

    }


    public override void TakeDamage(NPCManager manager)
    {

    }
    public override void Die(NPCManager manager)
    {

    }

    public override void Exit(NPCManager manager)
    {

    }
}
