using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inverter : MonoBehaviour
{
    public bool invertControls = false; // Invertiere die Steuerung
    public bool invertGravity = false; // Invertiere die Schwerkraft

    private GameObject player;
    private GameObject boulder;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        boulder = GameObject.Find("Boulder");
    }

    private void FixedUpdate()
    {
        if (invertGravity)
        {
            Physics2D.gravity = new Vector2(0, 9.81f);
        }
        else
        {
            Physics2D.gravity = new Vector2(0, -9.81f);
        }


        player.GetComponent<PlayerMovement>().invertControls = invertControls;
        //boulder.GetComponent<BoulderMovement>().invertControls = invertControls;
    }
}
