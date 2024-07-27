using UnityEngine;

public class SoliderAttack : NPCAttackBase
{
    private int _attack = Animator.StringToHash("Attack"); // NEAR
    private int _die = Animator.StringToHash("Die");
    private int _takeDamage = Animator.StringToHash("TakeDamage");
    private int _far = Animator.StringToHash("Far");
    private int _near = Animator.StringToHash("Near"); // ATTACK
    internal Transform _target;
    public float currentDistance;
    private float attackTime = 0;

    public void AttackTo(NPCManager manager, string attackName, float distance)
    {
        manager.anim.SetTrigger(attackName);
        currentDistance = distance;
        Debug.Log("Attacking");
    }

    public override void Start(NPCManager manager) => attackTime = manager.attackWaitTime;
    public override void Update(NPCManager manager)
    {
        if (_target == null) return;

        float distance = Vector3.Distance(manager.transform.position, _target.position);

        if ((manager.isAttacking == true && distance <= manager.remainingDistance))
        {
            attackTime -= Time.deltaTime;
            if (attackTime <= 0)
            {
                attackTime = manager.attackWaitTime;
                AttackTo(manager, manager._npcAttackSetting.attackName, distance);

            }
        }
    }

    public override void SayBaseManager(NPCBaseManager basemanager)
    {


    }

    public override void Die(NPCManager manager)
    {

    }

    public override void Exit(NPCManager manager)
    {

    }
}
