using UnityEngine;

// Used to destroy explosion prefab after its animation has finished.
public class DestroyAfterAnimation : MonoBehaviour
{
    public void OnAnimationFinished()
    {
        Destroy(gameObject);
    }
}
