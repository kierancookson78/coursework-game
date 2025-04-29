using UnityEngine;

public class PowerUpAdder : MonoBehaviour
{
    private PowerUpHotbar PowerUpHotbar;
    void Start()
    {
        PowerUpHotbar = GetComponent<PowerUpHotbar>();
        Shield shield = new Shield();
        CannonBlast cannonBlast = new CannonBlast();
        Nuke nuke = new Nuke();
        PowerUpHotbar.AddPowerUpToSlot(shield, 0);
        PowerUpHotbar.AddPowerUpToSlot(cannonBlast, 1);
        PowerUpHotbar.AddPowerUpToSlot(nuke, 2);
    }
}
