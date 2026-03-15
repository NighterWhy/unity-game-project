using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX = 200f; // Fare hassasiyeti (X ekseni)
    public float sensY = 200f; // Fare hassasiyeti (Y ekseni)

    public Transform orientation; // Karakterin yönünü döndürecek obje

    float xRotation;
    float yRotation;

    private void Start()
    {
        // Fareyi ekranın ortasına kilitler ve gizler
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Fareden gelen veriyi al
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;

        // Yukarı/Aşağı bakmayı 90 dereceyle sınırla (Boyun kırmamak için)
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // 1. Kamerasını döndür (Hem yukarı/aşağı hem sağa/sola)
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);

        // 2. Karakterin yönünü (Orientation) döndür (Sadece sağa/sola)
        // Böylece W'ye basınca baktığın yere gidersin
        if (orientation != null)
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}