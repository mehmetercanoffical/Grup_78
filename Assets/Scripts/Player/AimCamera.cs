using UnityEngine;

public class AimCamera : MonoBehaviour
{
    public Transform target; // Kameran�n hedefleyece�i nesne
    public float rotationSpeed = 5.0f; // Kameran�n d�nme h�z�

    void Update()
    {
        if (target != null)
        {
            // Hedefin konumuna do�ru y�nel
            Vector3 direction = target.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
