using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoDamageOnTrigger : MonoBehaviour
{
    [SerializeField]
    float damageValue = 1f;
    public bool active = true;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!active) return;

        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.SendMessage("ReceiveDamage", damageValue);
        }
    }
}
