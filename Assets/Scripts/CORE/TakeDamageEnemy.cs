using UnityEngine;

public class TakeDamageEnemy : MonoBehaviour
{
    public ITakeDamage takeDamage;
    public float damage = 0;
    public bool isDamage = false;
    private void Start()
    {
        if(takeDamage == null) takeDamage = transform.root.GetComponentInChildren<ITakeDamage>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isDamage)
        {
            isDamage = true;
            Attack(other.transform, damage);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            isDamage = false;
    }

    public void Attack(Transform other, float damage) => takeDamage.AttackComingPlayer(other, damage);
}
