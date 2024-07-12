using NPCSpace;
using UnityEngine;


public interface ITakeDamage
{
    void Attack();
}

public class TakeDamage : MonoBehaviour
{

    private NPC takeDamage;
    private void Start()
    {
        takeDamage = transform.root.GetComponent<NPC>();
    }
    public void Attack()
    {
        takeDamage.Attack();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            Attack();
        if (other.CompareTag("Enemy"))
            Attack();
    }

    //public void PlayerAttack(Transform _enemy, float attackDamage)
    //{
    //    Health health = _enemy.GetComponent<Health>();
    //    if (health != null)
    //    {
    //        health.health -= ((attackDamage) / 100f);
    //        health.health = Mathf.Max(0, Mathf.Min(1, health.health));

    //        if (health.health <= 0)
    //            Debug.Log("Enemy is dead");
    //    }
    //}
}
