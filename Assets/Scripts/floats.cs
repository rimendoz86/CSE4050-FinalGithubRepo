using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floats : MonoBehaviour
{
    
    // Variables to control the float speed and height
    public float amplitude = 0.5f; // The height the cube moves up and down
    public float speed = 2f;       // How fast the cube moves up and down


    private Vector3 startPosition;
void Start()
    {
        // Save the initial position of the cube
        startPosition = transform.position;
    }

    void Update()

    {

        // Use Mathf.Sin to make the cube move in a smooth up-and-down motion
        float newY = startPosition.y + Mathf.Sin(Time.time * speed) * amplitude;
        
        // Apply the new position to the cube
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);

    }

}
