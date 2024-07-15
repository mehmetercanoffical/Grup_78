using UnityEngine;
using NPCSpace;

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
            //rb.velocity = direction * speed;

        }
    }
}
