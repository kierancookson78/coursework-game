using System;
using UnityEngine;

public abstract class PowerUp
{
    public abstract void UsePowerUp();

    protected void PlaySoundEffect(AudioClip soundEffect)
    {
        throw new NotImplementedException();
    }

    protected void PlayAnimation(GameObject animationPrefab) 
    { 
        throw new NotImplementedException(); 
    }

    protected void AddPowerUp(PowerUp powerUp)
    {
        throw new NotImplementedException ();
    }
}
