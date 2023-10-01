using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOutsideOfCamera : MonoBehaviour
{
    private PlayerHealth _playerHealth;

    void Start()
    {
        _playerHealth = GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckIfOutsideOfCamera())
            _playerHealth.Die();
    }

    private bool CheckIfOutsideOfCamera()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
        return !onScreen;
    }
}
