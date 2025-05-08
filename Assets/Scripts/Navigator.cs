using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum NavigationMode { 
    Stationary,
    Walking,
    Running,
    Shooting,
    DoNothing
}
public class Navigator : MonoBehaviour
{
    [SerializeField]
    private float WalkSpeed = 1.5f;
    public NavigationNode NextNavigationNode;

    private CharacterController charController;
    [SerializeField]
    private NavigationMode CurrentNavigationMode = NavigationMode.Stationary;
    private float StationaryTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        charController = this.gameObject.GetComponent<CharacterController>();
    }

    public void Update()
    {

        if (StationaryTimer >= 0)
        {
            StationaryTimer -= Time.deltaTime;
        }

        switch (CurrentNavigationMode)
        {
            case NavigationMode.Stationary:
                if (StationaryTimer >= 0) return;
                this.CurrentNavigationMode = NavigationMode.Walking;
                StationaryTimer = 0f;
                break;
            case NavigationMode.Walking:
                this.MoveNode(this.WalkSpeed);
               break;
            case NavigationMode.Running:
                this.MoveNode(this.WalkSpeed * 2);
                break;
            case NavigationMode.DoNothing:
                //just sitting here doing nothing;
               break;
            default:
                break;
        }
    }

    // Update is called once per frame
    public void MoveNode(float speed)
    {
        this.LookAtNextNode();
        Vector3 movement = new Vector3(0, 0, 1) * speed;
        movement = Vector3.ClampMagnitude(movement, speed);
        movement.y = -9.81f;
        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        charController.Move(movement);
    }

    public void LookAtNextNode() {
        Utilities.LookAt(transform, NextNavigationNode.transform);
    }

    public void PauseToShoot() {
        this.CurrentNavigationMode = NavigationMode.DoNothing;
    }

    public void RunToNextNode() {
        if(CurrentNavigationMode != NavigationMode.DoNothing)
            this.CurrentNavigationMode = NavigationMode.Running;
    }
    //TODO: If collides with another navigator. turn it back around to previous node after a breif stationary.
    private void OnTriggerEnter(Collider other)
    {
        NavigationNode navigationNode = other.gameObject.GetComponent<NavigationNode>();
        if (navigationNode == null) return;

        List<GameObject> nodeEdges = navigationNode.NodeEdges;
        int index = Random.Range(0, nodeEdges.Count);
        GameObject nextNavigationNodeGameObject = nodeEdges[index];
        var nextNavigationNode = nextNavigationNodeGameObject.GetComponent<NavigationNode>();
        this.StationaryTimer = this.NextNavigationNode.StationaryTime * Random.Range(0.75f,1.5f);
        this.CurrentNavigationMode = NavigationMode.Stationary;
        this.NextNavigationNode = nextNavigationNode;
    }
}
