using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class CharacterVitals : MonoBehaviour
{
    public bool IsPlayer = false;
    public bool IsHostage = false;
    public int MaxHealthPoints = 100;
    public int HealthPoints = 100;

    [SerializeField] private AudioClip playerHurtSound;
    [SerializeField] private AudioClip enemyHurtSound;
    [SerializeField] private AudioClip defeatSound;

    private Animator animator;

    void Start()
    {
        if (this.IsPlayer)
        {
            GameHubController.instance.UpdateHealth(StaticData.HealthData);
        }

        // Get the Animator component from a child (e.g., the mesh/model)
        animator = GetComponentInChildren<Animator>();
    }

    void Update() { }

    public void TakeDamage(int healthPoints)
    {
        int newHealth;
        //Debug.Log(gameObject.name + ": You hit me!!!");
        sceneController sceneCtrl = FindObjectOfType<sceneController>();

        if (IsPlayer)
        {
            newHealth = StaticData.HealthData -= healthPoints;
            StaticData.HealthData = newHealth < 0 ? 0 : newHealth;
            SoundFXManager.instance.PlaySoundFXClip(playerHurtSound, transform, 1f);
            UpdateUI_Health();

            if (StaticData.HealthData == 0)
            {
                SoundFXManager.instance.PlaySoundFXClip(defeatSound, transform, 1f);
                soundManager.instance?.StopMusic();
                GetComponent<WeaponController>()?.DisableWeapon();
               //Debug.Log("Player died. You lose.");
                sceneCtrl.ShowLosePanel("You died.");
            }
        }
        else
        {
            newHealth = this.HealthPoints -= healthPoints;
            this.HealthPoints = newHealth < 0 ? 0 : newHealth;

            SoundFXManager.instance.PlaySoundFXClip(enemyHurtSound, transform, 1f);

            if (animator != null)
            {
                animator.SetTrigger("IsHit");
            }

            if (HealthPoints == 0)
            {
                StartCoroutine(CharacterDeath());
            }
        }

        if (IsHostage && HealthPoints == 0)
        {
            SoundFXManager.instance.PlaySoundFXClip(defeatSound, transform, 1f);
            soundManager.instance?.StopMusic();
           //Debug.Log("Hostage killed. You lose.");
            sceneCtrl.ShowLosePanel("You killed a hostage!");
        }
    }

    private IEnumerator CharacterDeath()
    {
       //Debug.Log(gameObject.name + " killed");

        if (animator != null)
        {
            int deathType = Random.Range(0, 2);
            animator.SetTrigger("Die");
        }

        yield return new WaitForSeconds(1.5f); // Allow death animation to play
        Destroy(gameObject);
    }

    private void UpdateUI_Health()
    {
        GameHubController.instance?.UpdateHealth(StaticData.HealthData);
    }

    public void HealPlayer(int healthPoints)
    {
        int newHealth = StaticData.HealthData += healthPoints;
        StaticData.HealthData = Mathf.Clamp(newHealth, 0, MaxHealthPoints);
        if (IsPlayer) UpdateUI_Health();

    }
    
}
