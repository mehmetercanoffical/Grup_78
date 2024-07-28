using UnityEngine;

public class PathFollowCaamera : MonoBehaviour
{
    public PathRoll path; // Takip edilecek yol
    public Transform target; // Takip edilecek hedef
    public float followSpeed = 5f; // Takip hýzý
    public float lookAheadTime = 0.1f; // Ýleriye bakma süresi

    private float pathPosition = 0f; // Yol üzerindeki konum

    void Update()
    {
        if (path == null || target == null)
        {
            return;
        }

        // Hedefin yol üzerindeki konumunu hesapla
        pathPosition = Mathf.Clamp01(pathPosition + followSpeed * Time.deltaTime);

        // Kameranýn yeni pozisyonunu hesapla
        Vector3 newPosition = path.GetPointAt(pathPosition);
        transform.position = newPosition;

        // Kameranýn ileriye bakma pozisyonunu hesapla
        Vector3 lookAheadPosition = path.GetPointAt(Mathf.Clamp01(pathPosition + lookAheadTime));

        // Kamerayý hedefe doðru döndür
        transform.LookAt(lookAheadPosition);
    }
}
