using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceCarController : MonoBehaviour
{
    public GameObject RedLamp;
    public GameObject BlueLamp;
    private float Timer = 0f;
    private bool Toggle = false;
    // Start is called before the first frame update
    void Start()
    {
        this.Timer += Random.Range(0f, 1f);
        this.Toggle = Random.value > .5f;
    }

    // Update is called once per frame
    void Update()
    {
        this.Timer += Time.deltaTime;

        if (this.Timer > 1f) {
            this.Toggle = !this.Toggle;
            this.RedLamp.GetComponent<Light>().intensity = this.Toggle ? 2 : 0;
            this.BlueLamp.GetComponent<Light>().intensity = this.Toggle ? 0 : 2;
            this.Timer = 0f;
        }

    }
}
