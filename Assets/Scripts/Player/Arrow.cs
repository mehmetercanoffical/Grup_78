using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Shoot(Transform player, Vector3 direction, float speed)
    {

        rb.isKinematic = false;
        rb.velocity = Vector3.zero;

        rb.velocity = direction.normalized * speed;

        Destroy(gameObject, 3f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
            Debug.Log("Arrow Collision " + collision.gameObject.name);
    }

}
