using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public WeaponController WeaponController { get; set; }

    // Start is called before the first frame update
    void Start()
    {
       this.WeaponController =  GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WeaponController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0) return;

        if (Input.GetMouseButtonDown(0)) {
            this.WeaponController.PullTrigger();
        }

        if (Input.GetMouseButtonUp(0))
        {
            this.WeaponController.ReleaseTrigger();
        }

    }

}
