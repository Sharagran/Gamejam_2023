using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaCollectible : MonoBehaviour
{
    public float value = 25f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerSkills>().AddMana(value);
            AudioManager.instance.Play("collect");
            Destroy(gameObject);
        }
    }
}
