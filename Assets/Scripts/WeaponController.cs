using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;
using TMPro;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private bool IsPlayer = false;
    [SerializeField] private GameObject ProjectileExit;
    [SerializeField] private GameObject ProjectilePrefab;
    [SerializeField]
    private GameObject MuzzleFlashPrefab; // Muzzle flash prefab
    [SerializeField] private GameObject muzzlExit;
    [SerializeField] public GameObject meshContainer;
    [SerializeField] private AudioClip ShootSound;

    [SerializeField] private GameObject m16Model;
    [SerializeField] private GameObject ak47Model;

    //to stop player
    private bool isAlive = true;


    // for the garnade 
    [SerializeField] private GameObject grenadePrefab;
    [SerializeField] private Transform grenadeSpawnPoint;
    [SerializeField] private TMP_Text grenadeUIText;

    private int grenadeCount = 0;
    //
    private string currentWeapon = "M16";  // Start with M16
    private float ProjectileVelocity = 300.0f;
    private readonly float m16RateOfFire = 0.33f;
    private readonly float ak47RateOfFire = 0.15f;
    private readonly float enemyRateOfFire = 0.5f;
    private bool IsAutomatic = false;
    private float RateOfFire = 0.25f;
    private bool IsTriggerPulled { get; set; }
    private float ShootingTimer { get; set; }

    void Start()
    {
        UpdateGrenadeUI();

        // ✅ Ensures proper fire rate gets set on scene reload
        EquipWeapon(currentWeapon);
    }

    //Update is called once per frame

    void Update()
    {   

        if (!isAlive)
        {
            IsTriggerPulled = false; // Extra safety
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.G) && grenadeCount > 0)
        {
            ThrowGrenade();
        }

        if (!IsTriggerPulled) return;

        ShootingTimer += Time.deltaTime;
        if (ShootingTimer >= RateOfFire)
        {
                if (isAlive && IsTriggerPulled) //  Prevent coroutine from even starting
                {
                    StartCoroutine(ShootGun());
                }
            ShootingTimer = 0f;
            if (!IsAutomatic) this.ReleaseTrigger();
        }


    }

    public void PullTrigger()
    {
        if (!isAlive)
        {
            //Debug.LogWarning("Tried to pull trigger when player is dead.");
            return;
        }
        
        //Debug.Log("Trigger pulled"); // 👈 This should not print after death
        this.IsTriggerPulled = true;
        this.ShootingTimer = this.RateOfFire;
    }
    public void ReleaseTrigger()
    {
        this.IsTriggerPulled = false;
        this.ShootingTimer = 0f;
    }

    public void EquipWeapon(string weaponKey , GameObject pickupPrefab, Vector3 pickupPosition) {
        //TODO; Institate a new weapon consumabe, place exisitng prefab and other info and put into the work space. 
        // Drop the currently held weapon as a pickup (if applicable)
        //if (pickupPrefab != null)
        //{
          //  GameObject droppedWeapon = Instantiate(pickupPrefab, pickupPosition + Vector3.right * 1f, Quaternion.identity);
            // Optional: Add rigidbody or collider logic here
        //}
    if (weaponKey == currentWeapon) return;

    if (m16Model != null) m16Model.SetActive(false);
    if (ak47Model != null) ak47Model.SetActive(false);

    switch (weaponKey)
    {
        case "M16":
            IsAutomatic = true;
            RateOfFire = 0.33f;
            m16Model?.SetActive(true);
            break;

        case "AK-47":
            IsAutomatic = true;
            RateOfFire = 0.15f;
            ak47Model?.SetActive(true);
            break;
        case "EnemyWeapon":
            IsAutomatic = true;
            RateOfFire = 0.33f;
            meshContainer.SetActive(false);
                break;
    }

    currentWeapon = weaponKey;
    }

    public IEnumerator ShootGun() {

        if (!isAlive || !IsTriggerPulled)
        {
            //Debug.LogWarning("ShootGun called but player is dead or not triggering.");
            yield break;
        }

        // This must not run after death
        if (ShootSound != null && isAlive)
        {
            SoundFXManager.instance.PlaySoundFXClip(ShootSound, gameObject.transform, 80f);
        }        

        if (MuzzleFlashPrefab != null && muzzlExit != null)
        {
            GameObject flash =  Instantiate(MuzzleFlashPrefab, muzzlExit.transform.position, muzzlExit.transform.rotation);
            flash.transform.SetParent(muzzlExit.transform,true);
            Destroy(flash, 0.2f);

            //here is what makes it stand still 
            //GameObject flash = Instantiate(MuzzleFlashPrefab, muzzlExit.transform);
            //flash.transform.localPosition = Vector3.zero;
            //flash.transform.localRotation = Quaternion.identity;
            //flash.transform.localScale = Vector3.one; // Reset scale in case it's affected
            //Destroy(flash, 0.2f);

        }
        else
        {
            //Debug.LogWarning("MuzzleFlashPrefab or muzzlExit is not assigned.");
        }


        var projectile = Instantiate(this.ProjectilePrefab);
        projectile.transform.SetParent(ProjectileExit.transform, true);
        projectile.transform.localPosition = Vector3.zero;
        var projectileController = projectile.GetComponent<ProjectileController>();
        projectileController.IsShotByPlayer = this.IsPlayer;
        projectileController.Project(gameObject.transform, this.ProjectileVelocity);

        yield return null;
    }

    public void EquipWeapon(string weaponKey)
    {
        if (m16Model != null) m16Model.SetActive(false);
        if (ak47Model != null) ak47Model.SetActive(false);

        switch (weaponKey)
        {
            case "M16":
                IsAutomatic = true;
                RateOfFire = m16RateOfFire;
                m16Model?.SetActive(true);
                break;

            case "AK-47":
                IsAutomatic = true;
                RateOfFire = ak47RateOfFire;
                ak47Model?.SetActive(true);
                break;

            case "EnemyWeapon":
                IsAutomatic = true;
                RateOfFire = enemyRateOfFire;
                meshContainer?.SetActive(false);
                break;

            default:
                RateOfFire = m16RateOfFire;
                break;
        }

        currentWeapon = weaponKey;
    }
    
    // here is the Grenades 
    private void UpdateGrenadeUI()
    {
        if (grenadeUIText != null)
        {
            if (grenadeCount > 0)
            {
                grenadeUIText.gameObject.SetActive(true);
                grenadeUIText.text = "Grenades: " + grenadeCount;
            }
            else
            {
                grenadeUIText.text = "Grenades: 0";
                grenadeUIText.gameObject.SetActive(false);
            }
        }
    }
    // add grenade
    public void AddGrenade(int amount)
    {
        grenadeCount += amount;
        //Debug.Log("Grenades added. New count: " + grenadeCount); //  Add this
        UpdateGrenadeUI();
    }

    public void DisableWeapon()
{
    isAlive = false;
    //Debug.Log("Weapon disabled");
    ReleaseTrigger();
    StopAllCoroutines(); // Cancel ongoing shooting

}

    private void ThrowGrenade()
    {
        if (grenadePrefab != null && grenadeSpawnPoint != null)
        {
            GameObject grenade = Instantiate(grenadePrefab, grenadeSpawnPoint.position, grenadeSpawnPoint.rotation);
            Rigidbody rb = grenade.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce((grenadeSpawnPoint.forward + Vector3.up * 0.5f) * 15f, ForceMode.VelocityChange);
            }
        }

        grenadeCount--;
        UpdateGrenadeUI();
    }
}
