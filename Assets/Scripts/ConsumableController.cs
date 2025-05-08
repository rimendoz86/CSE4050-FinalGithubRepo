using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public enum ConsumableType { 
    HealthKit,
    Poison
}
public class ConsumableController : MonoBehaviour
{
    public ConsumableType consumableType = ConsumableType.HealthKit;
    public GameObject HealthKitPrefab;
    public GameObject PoisonPrefab;
    public GameObject ActiveConsumable;
    // Start is called before the first frame update

    // adding sound
    [SerializeField] private AudioClip healthPickupSound; 
    [SerializeField] private AudioClip poisonPickupSound;

    void Start()
    {
        GameObject newConsumable;
        switch (consumableType)
        {
            case ConsumableType.Poison:
                newConsumable = Instantiate(PoisonPrefab);
                break;
            default:
                newConsumable = Instantiate(HealthKitPrefab);
                break;
        }
        newConsumable.transform.position = this.ActiveConsumable.transform.position;
        newConsumable.transform.localScale = this.ActiveConsumable.transform.lossyScale;
        newConsumable.transform.rotation = this.ActiveConsumable.transform.rotation;
        Destroy(this.ActiveConsumable);
        this.ActiveConsumable = newConsumable;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!Utilities.IsPlayer(other.gameObject)) return;
        CharacterVitals characterVitals = other.gameObject.GetComponentInChildren<CharacterVitals>();
        if (characterVitals == null) return;

        switch (consumableType)
        {
            case ConsumableType.HealthKit:
                characterVitals.HealPlayer(50);
                SoundFXManager.instance.PlaySoundFXClip(healthPickupSound, transform, 1f);
                break;
            case ConsumableType.Poison:
                characterVitals.TakeDamage(20);
                SoundFXManager.instance.PlaySoundFXClip(poisonPickupSound, transform, 1f);
                break;
            default:
                break;
        }
        Destroy(this.ActiveConsumable);
        Destroy(this.gameObject);
    }
    // Update is called once per frame
}
