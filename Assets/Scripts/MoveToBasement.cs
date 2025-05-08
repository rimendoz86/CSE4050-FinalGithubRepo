using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoveToBasement : MonoBehaviour
{
    public GameObject Instruction;
    public bool Action = false;

    // Start is called before the first frame update
    void Start()
    {
        Instruction.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Instruction.SetActive(true);
            Action = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Instruction.SetActive(false);
        Action = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Action == true)
            {
                Instruction.SetActive(false);
                Action = false;
                SceneManager.LoadScene(2);
            }
        }
    }
}
