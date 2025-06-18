using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public float yOffset = 1f;

    private float highestY;

    void Start()
    {
        if (target != null)
            highestY = target.position.y;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Hanya ikuti ke atas, tidak turun
        if (target.position.y > highestY)
        {
            highestY = target.position.y;
            Vector3 desiredPosition = new Vector3(transform.position.x, highestY + yOffset, transform.position.z);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
