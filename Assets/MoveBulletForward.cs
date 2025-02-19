using UnityEngine;

public class MoveBulletForward : MonoBehaviour
{
    public float bulletSpeed = 5f;
    void Update()
    {
        Vector3 pos = transform.position;
        Vector3 velocity = new(0, bulletSpeed *  Time.deltaTime, 0);
        pos += transform.rotation * velocity;
        transform.position = pos;
    }
}
