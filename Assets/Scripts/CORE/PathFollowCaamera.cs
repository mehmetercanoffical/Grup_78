using UnityEngine;

public class PathFollowCaamera : MonoBehaviour
{
    public PathRoll path; // Takip edilecek yol
    public Transform target; // Takip edilecek hedef
    public float followSpeed = 5f; // Takip h�z�
    public float lookAheadTime = 0.1f; // �leriye bakma s�resi

    private float pathPosition = 0f; // Yol �zerindeki konum

    void Update()
    {
        if (path == null || target == null)
        {
            return;
        }

        // Hedefin yol �zerindeki konumunu hesapla
        pathPosition = Mathf.Clamp01(pathPosition + followSpeed * Time.deltaTime);

        // Kameran�n yeni pozisyonunu hesapla
        Vector3 newPosition = path.GetPointAt(pathPosition);
        transform.position = newPosition;

        // Kameran�n ileriye bakma pozisyonunu hesapla
        Vector3 lookAheadPosition = path.GetPointAt(Mathf.Clamp01(pathPosition + lookAheadTime));

        // Kameray� hedefe do�ru d�nd�r
        transform.LookAt(lookAheadPosition);
    }
}
