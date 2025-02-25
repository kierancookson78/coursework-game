using UnityEngine;

public class DestroyBullets : MonoBehaviour
{
    [SerializeField] private float destructTime = 1f;
    void Update()
    {
        destructTime -= Time.deltaTime;

        if ( destructTime <= 0 )
        {
            Destroy( gameObject );
        }
    }
}
