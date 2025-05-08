using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadePickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            // Search in children or parents in case the WeaponController is nested
            WeaponController controller = other.GetComponentInChildren<WeaponController>()
                                          ?? other.GetComponentInParent<WeaponController>();

            if (controller != null)
            {
                controller.AddGrenade(1);
                Destroy(gameObject);
                // Debug.Log("Grenade Collected!");
            }
            else
            {
                Debug.LogWarning("WeaponController not found on player.");
            }
        }
    }
}
