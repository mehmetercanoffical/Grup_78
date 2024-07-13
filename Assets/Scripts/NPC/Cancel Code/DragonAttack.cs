using UnityEngine;
public class DragonAttack : NPCAttackBase
{

    private int _attack = Animator.StringToHash("Attack"); // NEAR
    private int _die = Animator.StringToHash("Die");
    private int _takeDamage = Animator.StringToHash("TakeDamage");
    private int _far = Animator.StringToHash("Far");
    private int _near = Animator.StringToHash("Near"); // ATTACK
    internal Transform _target;
    public float currentDistance;

    public void AttackTo(NPCManager manager, string attackName, float distance)
    {
        manager.isAttacking = true;

        if (manager.isAttacking)
            manager.anim.SetTrigger(attackName);

        currentDistance = distance;
    }

    public override void Start(NPCManager manager)
    {

    }
    public override void Update(NPCManager manager)
    {
        if (_target == null) return;

        float distance = Vector3.Distance(manager.transform.position, _target.position);
        if (manager.isAttacking == true)
        {
            Debug.Log("Ýs Attacking running");
            return;
        }
        else
            AttackTo(manager, manager._npcAttackSetting.attackName, distance);


    }


    public override void SayBaseManager(NPCBaseManager basemanager)
    {
        // I am die you should know

    }

    public override void Die(NPCManager manager)
    {

    }

    public override void Exit(NPCManager manager)
    {

    }
}
