using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public string weaponKey;               // e.g., "M16" or "AK-47"
    public GameObject weaponPickupPrefab;  // This pickup's prefab (used when dropping)

    private void OnTriggerEnter(Collider other)
    {
        WeaponController weaponController = other.GetComponentInChildren<WeaponController>();
        if (weaponController != null)
        {
            weaponController.EquipWeapon(weaponKey);
        }
    }
}
