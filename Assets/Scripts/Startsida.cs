using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Startsida : MonoBehaviour
{

    public Vector3 fp; // First touch position
    public Vector3 lp; // Last touch position
    public Vector3 swipe;
    private float dragDistance; // minimum distance for a swipe to be registered

    // Start is called before the first frame update
    void Start()
    {
        dragDistance = Screen.height * 15 / 100; // Dragdistance is 15% height of the screen
    }

    // Update is called once per frame
    void Update()
    {

        /*if (Input.GetMouseButtonDown(0)) // user is touching the screen with a single touch 
        {
            fp = Input.mousePosition;

        }

        else if (Input.GetMouseButtonUp(0)) // Check if the finger is removed from the screen
        {
            lp = Input.mousePosition;
            swipe = lp - fp;

            if (swipe.x >= dragDistance) // Positive value, swiping right
            {
                SceneManager.LoadScene(1);
            }

            if (swipe.x <= -dragDistance) // Negative value, swiping left
            {
                SceneManager.LoadScene(2);
            }
        }
        }*/
        if (Input.touchCount == 1) // user is touching the screen with a single touch 
        {
            Touch touch1 = Input.GetTouch(0); // Get the touch
            if (touch1.phase == TouchPhase.Began) // Check for the first touch
            {
                fp = touch1.position;
            }

            else if (touch1.phase == TouchPhase.Ended) // Check if the finger is removed from the screen
            {
                lp = touch1.position;
                swipe = lp - fp;
                if (swipe.x >= dragDistance) // Positive value, swiping right
                {
                    SceneManager.LoadScene(1);
                }
                if (swipe.x <= -dragDistance) // Negative value, swiping left
                {
                    SceneManager.LoadScene(2);
                }
            }
        }
    }
}

