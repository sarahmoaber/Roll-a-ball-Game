using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    public GameObject player;
    private Vector3 offset;
    void Start()
    {
        // Calculate once and in the beginning
        offset = transform.position - player.transform.position; 
    }

    void LateUpdate()
    {
        // to update in evrey frame
        transform.position = player.transform.position + offset; 
    }

}
