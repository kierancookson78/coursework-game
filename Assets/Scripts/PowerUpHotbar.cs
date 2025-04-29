using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class PowerUpHotbar : MonoBehaviour
{
    [SerializeField] private Transform playerJet;
    public PowerUp[] powerUpSlots = new PowerUp[3];
    private int selectedSlot = 0;

    [Header("UI Elements")]
    [SerializeField] private Image[] hotbarImages; // Assign Image components in the Inspector
    [SerializeField] private Image selectionIndicator; // Assign an Image to indicate selection

    [Header("Power-up Icons")]
    [SerializeField] private PowerUpIconData[] powerUpIconData;

    [System.Serializable]
    public struct PowerUpIconData
    {
        public string powerUpName; // Name of the PowerUp class (e.g., "SpeedBoost", "Invisibility")
        public Sprite icon;       // The icon for that power-up
    }

    [Header("Nuke Power Up")]
    [SerializeField] private AudioClip nukeSound;
    [SerializeField] private GameObject explosionPrefab;

    void Start()
    {
        // Ensure the UI arrays are the correct size
        if (hotbarImages.Length != powerUpSlots.Length)
        {
            Debug.LogError("Number of hotbar images does not match the number of power-up slots!");
            enabled = false;
            return;
        }

        if (selectionIndicator == null)
        {
            Debug.LogError("Selection indicator Image is not assigned!");
            enabled = false;
            return;
        }

        // Initialize the hotbar UI
        UpdateHotbarUI();
        UpdateSelectionIndicator();
    }

    void Update()
    {
        // Handle hotbar selection using number keys 1, 2, and 3
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedSlot = 0;
            UpdateSelectionIndicator();
            Debug.Log("Selected Hotbar Slot: 1");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedSlot = 1;
            UpdateSelectionIndicator();
            Debug.Log("Selected Hotbar Slot: 2");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedSlot = 2;
            UpdateSelectionIndicator();
            Debug.Log("Selected Hotbar Slot: 3");
        }

        // Handle using the selected power-up with right-click
        if (Input.GetMouseButtonDown(1) && powerUpSlots[selectedSlot] != null)
        {
            Debug.Log($"Using Power-up in Slot {selectedSlot + 1}: {powerUpSlots[selectedSlot].GetType().Name}");
            powerUpSlots[selectedSlot].UsePowerUp(nukeSound, playerJet.position, explosionPrefab, playerJet.rotation);
            RemovePowerUpFromSlot(selectedSlot);
        }
    }

    // Public method to add a power-up to a specific slot
    public void AddPowerUpToSlot(PowerUp powerUp, int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < powerUpSlots.Length)
        {
            powerUpSlots[slotIndex] = powerUp;
            UpdateHotbarUI();
            Debug.Log($"Added {powerUp.GetType().Name} to Hotbar Slot {slotIndex + 1}");
        }
        else
        {
            Debug.LogError("Invalid hotbar slot index.");
        }
    }

    // Public method to remove a power-up from a specific slot
    public void RemovePowerUpFromSlot(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < powerUpSlots.Length)
        {
            powerUpSlots[slotIndex] = null;
            UpdateHotbarUI();
            Debug.Log($"Removed power-up from Hotbar Slot {slotIndex + 1}");
        }
        else
        {
            Debug.LogError("Invalid hotbar slot index.");
        }
    }

    // Update the UI images to represent the power-ups
    private void UpdateHotbarUI()
    {
        for (int i = 0; i < powerUpSlots.Length; i++)
        {
            if (hotbarImages[i] != null)
            {
                if (powerUpSlots[i] != null)
                {
                    // Find the icon in the array based on the power-up's *type*.
                    Sprite icon = GetIconForPowerUp(powerUpSlots[i]);
                    hotbarImages[i].sprite = icon;
                    hotbarImages[i].enabled = icon != null; // Make sure the image is visible only if there's an icon
                }
                else
                {
                    // If the slot is empty, hide the image
                    hotbarImages[i].sprite = null;
                    hotbarImages[i].enabled = false;
                }
            }
        }
    }

    // Helper function to get the icon for a power-up.
    private Sprite GetIconForPowerUp(PowerUp powerUp)
    {
        if (powerUp == null) return null;

        string powerUpName = powerUp.GetType().Name; // Get the name of the PowerUp class.

        // Use LINQ to find the PowerUpIconData that matches the powerUpName
        PowerUpIconData data = powerUpIconData.FirstOrDefault(d => d.powerUpName == powerUpName);
        return data.icon; // Returns the sprite, or null if not found.
    }

    // Update the position of the selection indicator
    private void UpdateSelectionIndicator()
    {
        if (selectionIndicator != null && hotbarImages.Length > selectedSlot && hotbarImages[selectedSlot] != null)
        {
            // Position the selection indicator over the selected hotbar image
            selectionIndicator.transform.position = hotbarImages[selectedSlot].transform.position;
            selectionIndicator.gameObject.SetActive(true); // Ensure the indicator is visible
        }
        else if (selectionIndicator != null)
        {
            selectionIndicator.gameObject.SetActive(false); // Hide if no valid slot is selected
        }
    }
}

