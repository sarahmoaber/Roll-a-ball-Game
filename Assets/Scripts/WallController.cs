using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class WallController : MonoBehaviour
{
    void Start(){}
    void Update(){}
     void OnCollisionEnter(Collision collision)
    {
        // modify the color When the ball collides with the wall
         if (collision.gameObject.CompareTag("Ball"))
        {
            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                Color customColor = new Color(0.3679245f, 0.04396579f, 0.04396579f);
                renderer.material.color = customColor;
            }
        }
    }
    
}
