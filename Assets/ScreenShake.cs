using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public bool startShake = true;
    public float intensity = 0.1f;
    public float minIntensity = 0.1f;
    public float intensityDivider = 5f;
    public float shakeDuration = 1f;

    private GameObject _player;
    private GameObject _boulder;

    private void Start()
    {
        _player = GameObject.Find("Player");
        _boulder = GameObject.Find("Boulder");
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(_player.transform.position, _boulder.transform.position);
        intensity = Mathf.Max((1 / distance / intensityDivider), minIntensity);
        if(startShake)
        {
            StartCoroutine(Shaking());
            //startShake = false;
        }
    }

    IEnumerator Shaking()
    {
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            elapsed += Time.deltaTime;
            transform.localPosition = originalPos + Random.insideUnitSphere * intensity;
            yield return null;
        }
        transform.localPosition = originalPos;
    }
}
