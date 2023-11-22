using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{

    public GameObject messageDisplayObject;
    // To count the number of times the ball touches the object "yellow object"
    private HashSet<GameObject> countedObjects = new HashSet<GameObject>(); 
    //To count the number of times the ball touches the Wall
    private HashSet<GameObject> collidedWalls = new HashSet<GameObject>();
    public GameObject[] walls; 
    public GameObject winTextObject; // display the "YOU WIN!" text when a certain condition is met.
    public GameObject loseTextObject; // display the "YOU LOST" text when a certain condition is met.
    public float speed = 0;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI messageText;
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    private int wallHits; 
    private float scaleFactor = 0.1f; // to make the ball enlarge when it collides with the object.
    
    void Start()
    {
        rb = GetComponent <Rigidbody>();

        // to keep track of the points collected by the player.
        count = 0;
        SetCountText();

        // to track the number of times the player hits the wall.
        wallHits = 0;
        SetLoseText();

        // Delay the activation of the feature until the end of the game.
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
    }

    
    void OnMove (InputValue movementValue)
   {
        Vector2 movementVector = movementValue.Get<Vector2>(); 

        // Store the X and Y components of the movement
        movementX = movementVector.x; 
        movementY = movementVector.y;
   }// End OnMove

   void SetCountText() 
   {
       countText.text =  "Point: " + count.ToString();

       // Display the text when all pickups are collected
       if (count >= 27)
       {
           winTextObject.SetActive(true);

        // Disable player input
        // Implement this line in a way that ensures the ball remains visible on the screen and does not disappear.
           GetComponent<PlayerInput>().enabled = false;

           // Stop the ball's movement gradually
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
       }
   }

   void SetLoseText() 
   {
       // Display the lose text when all walls have been hit.
       if (wallHits >= 4)
       {
            loseTextObject.SetActive(true);
       }
       else
       {
            loseTextObject.SetActive(false);
       }
   }


   private void FixedUpdate() 
   {
        // Create a 3D movement vector. 
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY); 

        // Use the Rigidbody component to apply a force that moves the player.
        rb.AddForce(movement * speed); 
   }// End FixedUpdate

    void OnTriggerEnter(Collider other) 
    {  
        // this code in case we want the ( extra object )  disappear not change it color  
        // if (other.gameObject.CompareTag("PickUp"))
        // {
        //     // Increase the scale of the ball
        //     transform.localScale += new Vector3(scaleFactor, scaleFactor, scaleFactor);

        //     // Destroy the collected cube.
        //     Destroy(other.gameObject);

        //     // add 10 point instead of one point. 
        //     count = count + 10;
        //     SetCountText();

        //     // this message will appears after player pick up the object.
        //     StartCoroutine(DisplayMessage("You got 10 more points!"));
        // }  
    }// End OnTriggerEnter

 
    void OnCollisionEnter(Collision collision) 
    {
            // Only count the object if it hasn't been counted before.
        if (collision.gameObject.CompareTag("Object") && !countedObjects.Contains(collision.gameObject) )
        {
            count += 1;
            SetCountText();

            // Increase the scale of the ball
            transform.localScale += new Vector3(scaleFactor, scaleFactor, scaleFactor);

            // Add the object to the set of counted objects.
            countedObjects.Add(collision.gameObject);
        } 
        else if (collision.gameObject.CompareTag("Wall")&& !countedObjects.Contains(collision.gameObject))
        {
            wallHits++;
            SetLoseText();
            countedObjects.Add(collision.gameObject);

            if (wallHits == 1)
            {
                // Display message for hitting the first wall.
                StartCoroutine(DisplayMessage("Careful! You hit one wall. If you hit all four you will lose!"));
            }
            else if (wallHits == 2)
            {
                // Display message for hitting the second wall.
                StartCoroutine(DisplayMessage("Careful! You hit a second wall. If you hit all four you will lose!"));
            }
            else if (wallHits == 3)
            {
                // Display message for hitting the third wall.
                StartCoroutine(DisplayMessage("Careful! You hit a third wall. If you hit all four you will lose!"));
            }
            else if (wallHits == 4)
            {
                // Indicate that the game has ended when the player hits the last wall.
                SetLoseText();
                EndGame();
            }
        } // End Wall   
        else if (collision.gameObject.CompareTag("PickUp") && !countedObjects.Contains(collision.gameObject) )
        {
            // Extra Object
            count += 10;
            SetCountText();
            StartCoroutine(DisplayMessage("You got 10 more points!"));
           
            // Increase the scale of the ball
            transform.localScale += new Vector3(scaleFactor, scaleFactor, scaleFactor);

            // Add the object to the set of counted objects.
            countedObjects.Add(collision.gameObject);
        } 
    }// End OnCollisionEnter


    IEnumerator DisplayMessage(string message)
    {
        messageDisplayObject.SetActive(true);
        messageDisplayObject.GetComponent<TextMeshProUGUI>().text = message;

        // Wait for 3 seconds
        yield return new WaitForSeconds(3f);

        // Hide the message
        messageDisplayObject.SetActive(false);

    }// End DisplayMessage

       void EndGame()
    {
        GetComponent<PlayerInput>().enabled = false;
    }

}// End the class