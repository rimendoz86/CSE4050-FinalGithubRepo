using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public int TotalHostages = 3;
    public int Hostages = 0;

    [SerializeField]
    private sceneController sceneController;

    public GameHubController HUDController { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        this.HUDController = GameObject.FindGameObjectWithTag("GameController").GetComponentInChildren<GameHubController>();
       //Debug.Log(this.HUDController);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SavedAHostage() {
        Hostages++;
        this.HUDController.HostageCaptured(Hostages);
        if (Hostages == TotalHostages) {
            this.sceneController.ShowWinPanel("You rescued all the hostages!!!");
        }
    }
}
