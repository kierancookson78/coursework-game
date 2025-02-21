using UnityEngine;

public class DestroyBullets : MonoBehaviour
{
    public float destructTime = 1f;
    void Update()
    {
        destructTime -= Time.deltaTime;

        if ( destructTime <= 0 )
        {
            Destroy( gameObject );
        }
    }
}
