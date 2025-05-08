using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitchZone : MonoBehaviour
{
    public string weaponToGive = "AK-47"; // The weapon shown in the box
    public GameObject m16Visual; // The model shown for M16 in the box
    public GameObject ak47Visual; // The model shown for AK-47 in the box

    private void Start()
    {
        UpdateVisual();
    }

    private void OnTriggerEnter(Collider other)
    {
        WeaponController weaponController = other.GetComponentInChildren<WeaponController>();
        if (weaponController == null) return;

        // Swap logic
        string weaponToTake = weaponToGive; // e.g., AK-47
        string weaponToDrop = weaponToTake == "AK-47" ? "M16" : "AK-47";

        // Equip the player with the pickup weapon
        weaponController.EquipWeapon(weaponToTake);

        // Change the zone to offer the other weapon
        weaponToGive = weaponToDrop;
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        if (m16Visual != null) m16Visual.SetActive(weaponToGive == "M16");
        if (ak47Visual != null) ak47Visual.SetActive(weaponToGive == "AK-47");
    }
}
