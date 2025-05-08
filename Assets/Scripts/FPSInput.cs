using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSInput : MonoBehaviour
{
    public float speed = 3.0f;
    public float gravity = -9.8f;
    public CharacterController charController;
   // public SceneController sceneController;
    public const float baseSpeed = 6f;

    //private void OnEnable()
    //{
    //    Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    //}
    //private void OnDisable()
    //{
    //    Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    //}

    public void OnSpeedChanged(float value) {
        speed = baseSpeed * value; 
    }
    // Start is called before the first frame update
    void Start()
    {
        charController = GetComponent<CharacterController>();
       // sceneController = GameObject.FindWithTag("GameController").GetComponent<SceneController>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (sceneController.IsGameOver) return;

        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        //transform.Translate(deltaX, 0,s deltaZ);

        Vector3 movement = new Vector3(deltaX, 0, deltaZ);

        movement = Vector3.ClampMagnitude(movement, speed);

        // Apply gravity
        movement.y = gravity;

        movement *= Time.deltaTime;

        movement = transform.TransformDirection(movement);

        charController.Move(movement);

    }
}
