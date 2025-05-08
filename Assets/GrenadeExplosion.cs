using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeExplosion : MonoBehaviour
{
    public float delay = 2f;
    public float explosionRadius = 5f;
    public float explosionForce = 700f;
    public GameObject explosionEffectPrefab;

    

    void Start()
    {
        StartCoroutine(ExplodeAfterDelay());
    }

    IEnumerator ExplodeAfterDelay()
    {
        yield return new WaitForSeconds(delay);

        // Spawn explosion effect
        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
        }

        // Damage or affect nearby objects
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearby in colliders)
        {
            Rigidbody rb = nearby.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }

            // Optional: Apply damage if the object has health script
            // Example:
            var vitals = nearby.GetComponent<CharacterVitals>();
            if (vitals != null)
            {
                vitals.TakeDamage(50);
            }
        }

        Destroy(gameObject); // Destroy the grenade
    }
}
