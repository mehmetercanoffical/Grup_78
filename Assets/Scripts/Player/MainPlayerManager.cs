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
            health.health -= ((damage) / 100f);
            health.health = Mathf.Max(0, health.health);

            if (health.health <= 0)
                Debug.Log("Enemy is dead");
        }
    }
}
