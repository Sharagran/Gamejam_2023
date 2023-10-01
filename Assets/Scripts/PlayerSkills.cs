using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkills : MonoBehaviour
{
    public int mana = 100;
    public int maxMana = 100;
    public float gravityInversionDuration = 5f;
    public float controlsInversionDuration = 5f;

    private Animator _animator;
    private Inverter _inverter;

    public bool startFlip = false;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _inverter = GameObject.Find("GameController").GetComponent<Inverter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.F))
            return;
        if (mana < maxMana)
            return;

        _animator.SetTrigger("Magic");
        mana -= 100;

        doInversion();
    }

    private void doInversion()
    {
        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        switch (sceneName)
        {
            case "level1":
            case "SampleScene":
                Invoke("RotateUpsideDown", 2f);
                Invoke("SpellBlink", 2f + gravityInversionDuration -1f);
                Invoke("RotateUpsideDown", 2f + gravityInversionDuration);
                break;

            case "level2gerade":
                Invoke("InvertControls", 2f);
                Invoke("SpellBlink", 2f + controlsInversionDuration - 1f);
                Invoke("InvertControls", 2f + controlsInversionDuration);
                break;
        }
    }

    public void RotateUpsideDown()
    {
        StartCoroutine(RotateUpsideDownCoroutine());
        _inverter.InvertGravity();
    }

    private void InvertControls()
    {
        _inverter.InvertControls();
    }

    private void SpellBlink()
    {
        StartCoroutine(SpellBlinkCoroutine());
    }

    private IEnumerator SpellBlinkCoroutine()
    {
        float t = 0;
        float duration = 0.5f;

        Image spellBackground = GameObject.Find("Spell").transform.Find("Background").GetComponent<Image>();

        while (t < duration)
        {
            t += Time.deltaTime;
            spellBackground.color = Color.Lerp(Color.white, Color.black, t / duration);
            yield return null;
        }

        t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            spellBackground.color = Color.Lerp(Color.black, Color.white, t / duration);
            yield return null;
        }

    }

    private IEnumerator RotateUpsideDownCoroutine()
    {
        float t = 0;
        float duration = 1f;
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = transform.rotation * Quaternion.Euler(0, 0, 180);

        while (t < duration)
        {
            t += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t / duration);
            yield return null;
        }
    }

    public void AddMana(float mana)
    {
        this.mana += (int)mana;
        if (this.mana > maxMana)
            this.mana = maxMana;
    }
}
