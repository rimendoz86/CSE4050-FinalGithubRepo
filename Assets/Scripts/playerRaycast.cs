using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class playerRaycast : MonoBehaviour
{
    public GameObject crosshair;
    public GameObject openInstruction;
    public float interactionDistance;
    public LayerMask layers;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance, layers))
        {
            if (hit.collider.gameObject.GetComponent<openDoor>())
            {
                crosshair.SetActive(true);
                openInstruction.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.gameObject.GetComponent<openDoor>().openClose();
                }
            }
            else
            {
                crosshair.SetActive(false);
                openInstruction.SetActive(false);
            }            
        }
        else
        {
            crosshair.SetActive(false);
            openInstruction.SetActive(false);
        }
    }
}
