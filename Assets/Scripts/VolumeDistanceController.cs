using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeDistanceController : MonoBehaviour
{
    public float maxDistance = 30f;
    public float minVolume = 0.05f;
    public float volume = 1f;

    private AudioSource _audioSource;
    private GameObject _player;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(_player.transform.position, transform.position);
        volume = 1f - (distance / maxDistance);
        volume = Mathf.Clamp(volume, minVolume, 1f);
        _audioSource.volume = volume;
    }
}
