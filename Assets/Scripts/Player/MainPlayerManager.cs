using AttackSystem;
using UnityEngine;

public class MainPlayerManager : MonoBehaviour, ITakeDamage
{

    private AttackMachineBaseManager AttackMachineBase;
    private void Awake() => AttackMachineBase = GetComponent<AttackMachineBaseManager>();
    public void AttackComingPlayer(Transform target, float damage)
    {
        Health health = target.GetComponent<Health>();
        if (health != null)
        {
            health.health -= ((damage));
            health.health = Mathf.Max(0, health.health);
            target.GetComponent<NPCManager>().TakeDamage();
            if (health.health <= 0) target.GetComponent<NPCManager>().Die();
        }
    }

    public void CollisionControl(bool val) => GetComponent<Collider>().enabled = val;
}
