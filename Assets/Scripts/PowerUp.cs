using System;
using UnityEngine;

public abstract class PowerUp
{
    public abstract void UsePowerUp();

    protected void PlaySoundEffect(AudioClip soundEffect, Vector3 soundPosition)
    {
        AudioSource.PlayClipAtPoint(soundEffect, soundPosition);
    }

    protected void PlayAnimation(GameObject animationPrefab, Vector3 animationPosition, Quaternion animationRotation) 
    {
        GameObject.Instantiate(animationPrefab, animationPosition, animationRotation);
    }

    protected void AddPowerUp(PowerUp powerUp)
    {
        throw new NotImplementedException ();
    }
}
