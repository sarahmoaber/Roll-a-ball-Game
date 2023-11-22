using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExttraObjectControllar : MonoBehaviour
{

    void Start(){}
    void Update(){}


      void OnCollisionEnter(Collision collision)
    {
        // modify the color When the ball collides with the Extra Object 
         if (collision.gameObject.CompareTag("Ball"))
        {
            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = Color.red;
            }
        }
    }
}
