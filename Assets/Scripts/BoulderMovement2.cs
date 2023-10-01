using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RaycastController;
using UnityEngine.InputSystem.HID;

public class BoulderMovement2 : MonoBehaviour
{
    public float speed = 1f; // Geschwindigkeit
    public float rotationSpeed = 1f; // Rotationsgeschwindigkeit
    public bool invertControls = false; // Invertiere die Steuerung

    public bool keepRotating = false; // Rotiere weiterhin

    //Store transform of raycastOrigin object. Used for raycast in GroundCheckMethod()
    [SerializeField] private Transform rayCastOrigin;

    //Store playerFeet transform to allow us to adjust our players position in GroundCheckMethod()
    [SerializeField] private Transform playerFeet;

    //LayerMask used in raycast to make raycast more Performant
    //The raycast will ignore all objects outside of the layermask we select.
    [SerializeField] private LayerMask layerMask;

    private RaycastHit2D Hit2D;

    // Update is called once per frame
    void Update()
    {
        MoveLogic();
        GroundCheckMethod();
    }

    private void MoveLogic()
    {
        Vector2 moveDirectionVector = invertControls ? Vector2.left : Vector2.right;
        Vector3 rotationDirectionVector = invertControls ? Vector3.forward : Vector3.back;

        float rotateSpeed = speed * Time.deltaTime * 360f * rotationSpeed;

        //Create temp vector2 which stores the x value of player input and 0 for y. 
        Vector2 temp = new Vector2(speed * Time.deltaTime * 5, 0);
        temp = temp * moveDirectionVector;
        //translate our player transform via the temp vector2 amount
        //transform.Translate(temp);
        transform.position += (Vector3)temp;

        if (keepRotating)
            transform.Rotate(rotationDirectionVector * rotateSpeed);
    }

    private void GroundCheckMethod()
    {
        //store the raycast hit info/The raycast arguements are (origin of ray, direction of ray, length of ray, what layer mask to check against)
        Hit2D = Physics2D.Raycast(rayCastOrigin.position, -Vector2.up, 100f, layerMask);

        //Performant check to see if raycast hit has any data, if so, run the code
        if (Hit2D != false)
        {
            //create temp vector2 to store playerFeet position
            Vector2 temp = playerFeet.position;
            //We get the y position of our raycast hit/ and set the y value of our temp vector2
            temp.y = Hit2D.point.y;
            //we can now directly set our players position by setting it to our temp vector2 value that we adjusted.
            playerFeet.position = temp;
        }
    }
}
