using UnityEngine;

public class AimCamera : MonoBehaviour
{
    public Transform target; // Kameranýn hedefleyeceði nesne
    public float rotationSpeed = 5.0f; // Kameranýn dönme hýzý

    void Update()
    {
        if (target != null)
        {
            // Hedefin konumuna doðru yönel
            Vector3 direction = target.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
