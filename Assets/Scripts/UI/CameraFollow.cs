using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public float yOffset = 1f;
    public float upwardSpeed = 1f; // Kecepatan naik otomatis kamera

    private float highestY;

    void Start()
    {
        if (target != null)
            highestY = target.position.y;
        else
            highestY = transform.position.y;
    }

    void LateUpdate()
    {
        // Kamera selalu naik secara konstan
        float nextY = transform.position.y + (upwardSpeed * Time.deltaTime);

        // Jika player lompat lebih tinggi, kamera ikuti dia (hanya naik)
        if (target != null && target.position.y + yOffset > nextY)
        {
            highestY = target.position.y;
            nextY = target.position.y + yOffset;
        }

        // Lerp agar pergerakan kamera halus
        Vector3 desiredPosition = new Vector3(transform.position.x, nextY, transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
