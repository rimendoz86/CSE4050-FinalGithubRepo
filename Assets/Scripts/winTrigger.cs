using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class winTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           var gameLogic = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameLogic>();
           gameLogic.SavedAHostage();
            Destroy(gameObject);
        }
    }
}
