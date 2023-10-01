using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Inverter : MonoBehaviour
{
    public bool gravityInverted = false;
    public bool controlsInverted = false;

    private GameObject player;
    private GameObject boulder;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        boulder = GameObject.Find("Boulder");
    }

    public void InvertGravity()
    {
        if(gravityInverted)
        {
            Physics2D.gravity = new Vector2(0, -9.81f);
            gravityInverted = false;
        }
        else
        {
            Physics2D.gravity = new Vector2(0, 9.81f);
            gravityInverted = true;
        }
    }

    public void InvertControls()
    {
        var playerMovement = player.GetComponent<PlayerMovement>();
        var boulderMovement = boulder.GetComponent<BoulderMovement2>();

        if (controlsInverted)
        {
            controlsInverted = false;
            playerMovement.invertControls = false;
            boulderMovement.invertControls = false;
        }
        else
        {
            controlsInverted = true;
            playerMovement.invertControls = true;
            boulderMovement.invertControls = true;
        }
    }
}
