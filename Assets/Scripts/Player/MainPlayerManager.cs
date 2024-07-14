using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerManager : MonoBehaviour, ITakeDamage
{

    public void Attack(Transform target, float damage)
    {
        Health health = target.GetComponent<Health>();
        if (health != null)
        {
            Debug.Log("Player is attacking " + health.health);
            health.health -= ((damage));
            health.health = Mathf.Max(0, health.health);
            target.GetComponent<NPCManager>().TakeDamage();
            if (health.health <= 0)
                Debug.Log("Enemy is dead");
        }
    }
}
