using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using static Unity.VisualScripting.Member;

public enum EnemyBehavior { 
    Wandering,
    Attacking
}

public class EnemyController : MonoBehaviour
{
    private Navigator Navigator;
    private GameObject PlayerObject;
    private WeaponController WeaponController;
    private Animator animator;

    private float FieldOfView = 75f;
    private float Timer = 0f;
    private bool isPullingTrigger;
    private EnemyBehavior CurrentEnemyBehavior = EnemyBehavior.Wandering;
    

    // Start is called before the first frame update
    void Start()
    {
        this.Navigator = gameObject.GetComponent<Navigator>();
        this.PlayerObject = GameObject.FindGameObjectWithTag("Player");
        this.WeaponController = this.gameObject.GetComponentInChildren<WeaponController>();
        this.animator = GetComponentInChildren<Animator>();
        this.WeaponController.EquipWeapon("EnemyWeapon");
        this.Timer += UnityEngine.Random.Range(0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        Timer -= Time.deltaTime;
        if (Timer >= 0) return;

        switch (CurrentEnemyBehavior)
        {
            case EnemyBehavior.Wandering:
                CheckIfShouldAttack();
                Timer = 1f;
                break;
            case EnemyBehavior.Attacking:
                AttackPlayer();
                Timer = 2f;
                break;
            default:
                break;
        }

        if (animator != null && Navigator != null)
        {
            Vector3 velocity = Navigator.GetComponent<CharacterController>().velocity;
            float speed = new Vector3(velocity.x, 0, velocity.z).magnitude;
            
            animator.SetFloat("Speed", speed);
        }


    }

    private void CheckIfShouldAttack()
    {
        if (IsPlayerVisible() && IsPlayerInFieldOfView() && OnSameFloor()) {
            CurrentEnemyBehavior = EnemyBehavior.Attacking;
        }
    }

    private void AttackPlayer()
    {
        if (isPullingTrigger)
        {
            this.Navigator.PauseToShoot();
            Utilities.LookAt(transform, PlayerObject.transform);
            if (IsPlayerVisible()) this.WeaponController.PullTrigger();
        }
        else
        {
            this.WeaponController.ReleaseTrigger();
            this.Navigator.RunToNextNode();
        }
        isPullingTrigger = !isPullingTrigger;
    }

    public void AlertOfPlayerAttack() {
        this.CurrentEnemyBehavior = EnemyBehavior.Attacking;
        Timer = 0.25f;
    }

    private bool OnSameFloor() {
        return (PlayerObject.transform.position.y - gameObject.transform.position.y) < 1.5f;
    }

    private double AngleToPlayerAroundY() {
        return Utilities.AngleAroundY(gameObject.transform, PlayerObject.transform);
    }

    private bool IsPlayerInFieldOfView() {
        var angleToPlayer = AngleToPlayerAroundY();
        var enemyRotation = (double)(gameObject.transform.rotation.y * (180 / Math.PI) * 2);
        return Math.Abs(enemyRotation - angleToPlayer) < FieldOfView;
    }

    private bool IsPlayerVisible() {
        RaycastHit hit;
        return Physics.Linecast(transform.position, this.PlayerObject.transform.position, out hit) && hit.collider.gameObject.CompareTag("Player");
    }
}
