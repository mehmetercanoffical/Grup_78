using UnityEngine;

public interface ITakeDamage
{
    void Attack(Transform target, float damage);
    void CollisionControl(bool val);
}


public class TakeDamagePlayer : MonoBehaviour
{
    private ITakeDamage takeDamage;
    public int damage = 0;
    private void Start()
    {
        takeDamage = transform.root.GetComponent<ITakeDamage>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Attack");
            Attack(other.transform, damage);
        }
    }
    public void Attack(Transform other, float damage) => takeDamage.Attack(other, damage);
}
