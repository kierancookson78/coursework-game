using UnityEngine;

public class Nuke : PowerUp
{
    private string targetTag = "Enemy";
    public override void UsePowerUp(AudioClip soundEffect, Vector3 effectPosition, GameObject animationPrefab, Quaternion animationRotation)
    {
        PlayAnimation(animationPrefab, effectPosition, animationRotation);
        PlaySoundEffect(soundEffect, effectPosition);

        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(targetTag);
        int destroyedCount = 0;

        foreach (GameObject obj in taggedObjects)
        {
            if (Application.isPlaying)
            {
                GameObject.Destroy(obj);
            }
            else
            {
                GameObject.DestroyImmediate(obj);
            }
            destroyedCount++;
        }
        ScoreManager.Instance.AddScore(100 * destroyedCount);
    }
}
