using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineShake : MonoBehaviour
{
    public float intensity = 0.1f;

    public float minIntensity = 0.01f;
    public float maxIntensity = 0.1f;
    public float maxDistance = 30f;

    private GameObject _player;
    private GameObject _boulder;
    private CinemachineVirtualCamera _cinemachineVirtuelCamera;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player");
        _boulder = GameObject.Find("Boulder");
        _cinemachineVirtuelCamera = GetComponent<CinemachineVirtualCamera>();

        if (_player == null || _boulder == null)
        {
            Debug.LogError("Player or Boulder not found!");
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(_player.transform.position, _boulder.transform.position);
        intensity = Mathf.Clamp((1f - (distance / maxDistance)) * 3f, minIntensity, maxIntensity);

        _cinemachineVirtuelCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = intensity;
    }
}
