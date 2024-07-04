using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform character; // Karakterin Transform bile�eni
    public Transform headPoint; // Karakterin ba��ndaki point
    public float mouseSensitivity = 100f; // Mouse hassasiyeti
    public float distanceFromHead = 2f; // Kameran�n ba�tan uzakl���
    public float verticalOffset = 0.5f; // Kameran�n dikey kaymas�

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

        // Y eksenindeki hareketi s�n�rl� bir �ekilde uygula
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // X ekseninde hareketi uygula
        yRotation += mouseX;

        // Kamera rotasyonunu hesapla ve uygula
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);

        // Kameray� karakterin ba��ndaki point noktas�na g�re ayarla
        Vector3 offset = new Vector3(0, verticalOffset, -distanceFromHead);
        transform.position = headPoint.position + transform.rotation * offset;

        // Karakterin kamera y�n�ne do�ru d�nmesini sa�la
        character.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }

}
