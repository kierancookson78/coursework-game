using System;
using UnityEngine;

public abstract class PowerUp
{

    protected void PlaySoundEffect(AudioClip soundEffect, Vector3 soundPosition)
    {
        AudioSource.PlayClipAtPoint(soundEffect, soundPosition);
    }

    protected void PlayAnimation(GameObject animationPrefab, Vector3 animationPosition, Quaternion animationRotation) 
    {
        GameObject.Instantiate(animationPrefab, animationPosition, animationRotation);
    }
    public abstract void UsePowerUp(AudioClip soundEffect, Vector3 effectPosition, GameObject animationPrefab, Quaternion animationRotation);
}
