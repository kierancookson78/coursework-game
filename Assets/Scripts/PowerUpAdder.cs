using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PowerUpAdder : MonoBehaviour
{
    private PowerUpHotbar powerUpHotbar;
    private bool isShieldUnlocked = false;
    private bool isCannonUnlocked = false;
    private bool isNukeUnlocked = false;
    void Start()
    {
        powerUpHotbar = GetComponent<PowerUpHotbar>();
    }

    void Update()
    {
        int sheildStreak = ScoreManager.Instance.GetShieldStreak();
        int cannonStreak = ScoreManager.Instance.GetCannonStreak();
        int nukeStreak = ScoreManager.Instance.GetNukeStreak();
        bool isNukeUsed = powerUpHotbar.NukeHasBeenUsed();

        if (sheildStreak == 5 && !isShieldUnlocked)
        {
            Shield shield = new Shield();
            powerUpHotbar.AddPowerUpToSlot(shield, 0);
            isShieldUnlocked = true;
            ScoreManager.Instance.ResetShieldStreak();
        }
        if (cannonStreak == 10 && !isCannonUnlocked)
        {
            CannonBlast cannonBlast = new CannonBlast();
            powerUpHotbar.AddPowerUpToSlot(cannonBlast, 1);
            isCannonUnlocked = true;
            ScoreManager.Instance.ResetCannonStreak();
        }
        if (nukeStreak == 15 && !isNukeUnlocked && !isNukeUsed)
        {
            Nuke nuke = new Nuke();
            powerUpHotbar.AddPowerUpToSlot(nuke, 2);
            isNukeUnlocked = true;
            ScoreManager.Instance.ResetNukeStreak();
        }
    }

    public void LockCannon()
    {
        isCannonUnlocked = false;
    }

    public void LockShield() 
    {
        isShieldUnlocked = false;
    }

    public void LockNuke()
    {
        isNukeUnlocked = false;
    }

    public bool CannonIsUnlocked()
    {
        return isCannonUnlocked;
    }

    public bool ShieldIsUnlocked()
    {
        return isShieldUnlocked;
    }

    public bool NukeIsUnlocked()
    {
        return isNukeUnlocked;
    }
}
