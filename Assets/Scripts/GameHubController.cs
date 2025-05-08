using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameHubController : MonoBehaviour
{
    public static GameHubController instance;

    [SerializeField]
    private TextMeshProUGUI HealthValue;
    [SerializeField]
    private TextMeshProUGUI HostageCounter;
    private int HostageCounterValue = 0;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        this.UpdateHostage();
    }

    // Update is called once per frame
    public void UpdateHealth(int healthValue) { 
        this.HealthValue.text = healthValue.ToString();
    }

    public void HostageCaptured(int hostageCount) {
        this.HostageCounterValue = hostageCount;
        this.UpdateHostage();
         
    }
    private void UpdateHostage() {
        this.HostageCounter.text = $"{HostageCounterValue}/3";
    }
}
