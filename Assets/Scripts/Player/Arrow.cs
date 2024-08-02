using UnityEngine;

namespace AttackSystem
{
    public class Arrow : MonoBehaviour
    {
        private Rigidbody rb;
        void Start() => rb = GetComponent<Rigidbody>();

        public void Shoot(Vector3 direction, float speed)
        {
            rb.isKinematic = false;
            rb.AddForce(direction * speed, ForceMode.Impulse);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                
                if(other.GetComponent<NPCManager>().nPCS == NPCS.TROLL)
                    other.GetComponent<NPCManager>().maxDistanceOffset += 5f;

                Destroy(gameObject);
            }
        }
    }
}
