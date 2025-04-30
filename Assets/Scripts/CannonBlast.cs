using UnityEngine;

public class CannonBlast : PowerUp
{
    public override void UsePowerUp(PowerUpHotbar powerUpHotbar, PlayerController playerController)
    {
        powerUpHotbar.ActivateCannon();
        playerController.MultiplyBulletSpeed();
        playerController.MultiplyFireRate();
    }
}
