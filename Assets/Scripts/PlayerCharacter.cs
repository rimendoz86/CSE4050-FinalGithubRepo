using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{

    private int health;
    private int maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 5;
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hurt(int damage) {
        health -= damage;
        //Debug.Log($"Health: {health}");
    }
    public string getHealthText() {
        return $"Health: {health}/{maxHealth}";
        
    }
}
