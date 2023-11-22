using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Rotator : MonoBehaviour
{

    void Update()
    {
        // Rotate the object around its transform
        transform.Rotate (new Vector3 (15, 30, 45) * Time.deltaTime);
    }


    void OnCollisionEnter(Collision collision)
    {
         if (collision.gameObject.CompareTag("Ball"))
        {
            // modify the color When the ball collides with the yellow object
            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = Color.black;
            }
        }
    }
}
