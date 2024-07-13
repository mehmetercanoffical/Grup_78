using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageEnemy : MonoBehaviour
{
    private ITakeDamage takeDamage;
    public int damage = 0;
    private void Start()
    {
        takeDamage = transform.root.GetComponent<ITakeDamage>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            Attack(other.transform, damage);
    }

    public void Attack(Transform other, float damage) => takeDamage.Attack(other, damage);
}
