using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGravity : MonoBehaviour
{
    private PlayerSkills _playerSkills;

    private void Start()
    {
        _playerSkills = GameObject.Find("Player").GetComponent<PlayerSkills>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerSkills.ResetGravityToNormal();
            Destroy(gameObject);
        }
    }
}
