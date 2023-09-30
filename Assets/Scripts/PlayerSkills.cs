using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    public int mana = 100;
    public int maxMana = 100;
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
                break;

            case "Level2":
                break;
        }
    }

    public void RotateUpsideDown()
    {
        StartCoroutine(RotateUpsideDownCoroutine());
        _inverter.InvertGravity();
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
