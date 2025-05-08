using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spin : MonoBehaviour
{
    public float speed = 3f;
   void Update()
   {
    transform.Rotate(0, speed * Time.deltaTime,0);
   }

}
