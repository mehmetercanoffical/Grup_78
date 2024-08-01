using UnityEngine;

public class ParticleHit : MonoBehaviour
{
    public float damage = 0.01f;
    private void OnParticleCollision(GameObject other)
    {
        this.gameObject.GetComponent<MainPlayerManager>().AttackComingPlayer(other.transform, damage);
    }
}