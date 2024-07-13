using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITakeDamage
{
    void Attack(Transform target, float damage);
}


public class TakeDamagePlayer : MonoBehaviour
{
    private ITakeDamage takeDamage;
    public int damage = 0;
    private void Start()
    {
        takeDamage = transform.root.GetComponent<ITakeDamage>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collision " + collision.gameObject.name);
            Attack(collision.transform, damage);
        }
    }

    public void Attack(Transform other, float damage) => takeDamage.Attack(other, damage);
}
