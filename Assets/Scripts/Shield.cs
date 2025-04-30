using UnityEngine;
using UnityEngine.UI;

public class Shield : PowerUp
{
    public override void UsePowerUp(PlayerHealthComponent playerHealthComponent, Slider shieldBar)
    {
        shieldBar.gameObject.SetActive(true);
        playerHealthComponent.ActivateShield();
    }
}
