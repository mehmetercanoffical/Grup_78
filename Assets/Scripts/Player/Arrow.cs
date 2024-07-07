using UnityEngine;


namespace AttackSystem
{
    public class Arrow : MonoBehaviour
    {
        private Rigidbody rb;
        void Start() => rb = GetComponent<Rigidbody>();

        public void Shoot(Transform player, Vector3 direction, float speed)
        {
            rb.isKinematic = false;
            rb.velocity = direction.normalized * speed;

            Destroy(gameObject, 3f);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision != null)
            {
                if (collision.gameObject.GetComponent<Enemy>() != null)
                {

                    Debug.Log("Hit enemy");
                    rb.isKinematic = true;
                    rb.AddForce(Vector3.zero);


                    ContactPoint contact = collision.contacts[0];
                    Vector3 pos = contact.point.normalized;
                    transform.position = pos;
                    transform.SetParent(collision.transform);

                    //collision.gameObject.GetComponent<Enemy>().TakeDamage(1);
                }
            }
        }
    }
}
