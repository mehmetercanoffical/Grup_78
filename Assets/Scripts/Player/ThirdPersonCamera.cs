using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform character; // Karakterin Transform bileþeni
    public Transform headPoint; // Karakterin baþýndaki point
    public float mouseSensitivity = 100f; // Mouse hassasiyeti
    public float distanceFromHead = 2f; // Kameranýn baþtan uzaklýðý
    public float verticalOffset = 0.5f; // Kameranýn dikey kaymasý

    private float xRotation = 0f; // X eksenindeki rotasyon
    private float yRotation = 0f; // Y eksenindeki rotasyon

    void Start()
    {
        // Cursor'u gizle ve kilitle
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Mouse hareketlerini oku
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Y eksenindeki hareketi sýnýrlý bir þekilde uygula
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // X ekseninde hareketi uygula
        yRotation += mouseX;

        // Kamera rotasyonunu hesapla ve uygula
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);

        // Kamerayý karakterin baþýndaki point noktasýna göre ayarla
        Vector3 offset = new Vector3(0, verticalOffset, -distanceFromHead);
        transform.position = headPoint.position + transform.rotation * offset;

        // Karakterin kamera yönüne doðru dönmesini saðla
        character.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }

}
