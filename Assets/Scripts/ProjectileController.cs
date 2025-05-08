using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private Rigidbody RigidBody;
    public bool IsShotByPlayer = false;
    private float timer = 0;
    public int Damage = 20;
    public float bulletSpread = 0.015f;
    // Start is called before the first frame update
    void Start()
    {
        this.RigidBody = this.gameObject.GetComponent<Rigidbody>();
        if (!IsShotByPlayer) this.bulletSpread = 0.05f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 1f) {
            //prevent leak if never collides
            Destroy(gameObject);
        }
    }
    
    public void Project(Transform weaponTranform,float velocity) {
        this.RigidBody = this.gameObject.GetComponent<Rigidbody>();
        
        Vector3 direction = transform.position - weaponTranform.transform.position;
        Vector3 bulletAngleAdjust = IsShotByPlayer
            ? new Vector3(0f, -0.09f, 0f) 
            : new Vector3(0f, -0.15f, 0f);

        bulletAngleAdjust += new Vector3(Random.Range(-bulletSpread, bulletSpread), Random.Range(-bulletSpread, bulletSpread), Random.Range(-bulletSpread, bulletSpread));
        var bulletVelocity = (new Vector3(direction.x, direction.y,direction.z).normalized + bulletAngleAdjust) * velocity;
        this.RigidBody.velocity = bulletVelocity;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (this.IsShotByPlayer 
            && Utilities.IsPlayer(collision.gameObject)) return;

        var characterVitals = collision.gameObject.GetComponent<CharacterVitals>() ?? collision.gameObject.GetComponentInParent<CharacterVitals>();

        if (characterVitals != null) {
            characterVitals.TakeDamage(Damage);
        }

        var enemyBehavior = collision.gameObject.GetComponent<EnemyController>();
        if (this.IsShotByPlayer && enemyBehavior != null) {
            enemyBehavior.AlertOfPlayerAttack();
        }

        Destroy(gameObject);
    }
}
