using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationNode : MonoBehaviour
{
    public float StationaryTime = 2f;
    public string Name = "NoName";
    public List<GameObject> NodeEdges;
    

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        if (NodeEdges == null) {
            Debug.Log($"{Name} Navigation Node contains no edges. Stationary");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //this.MoveNode();
    }
}
