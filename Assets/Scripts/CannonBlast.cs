using UnityEngine;

// The cannon blast power up increases size, damage, fire rate and bullet speed.
public class CannonBlast : PowerUp
{
    public override void UsePowerUp(PowerUpHotbar powerUpHotbar, PlayerController playerController)
    {
        powerUpHotbar.ActivateCannon();
        playerController.MultiplyBulletSpeed();
        playerController.MultiplyFireRate();
    }
}
