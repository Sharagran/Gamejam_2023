using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health = 3;
    public float invincibilityTime = 1f;

    public Image[] ui_hearts;
    public Sprite ui_heart_full;
    public Sprite ui_heart_empty;

    private bool _isDead = false;
    private float _invincibilityTimer = 0f;

    public void ReceiveDamage(float damageValue)
    {
        if (_invincibilityTimer > 0)
            return;

        if (_isDead)
            return;

        Debug.Log("Player received " + damageValue + " damage");
        health -= damageValue;
        StartCoroutine(invincibilityEffect());
        _invincibilityTimer = invincibilityTime;

        if (health <= 0)
            Die();
    }

    IEnumerator invincibilityEffect()
    {
        GetComponent<Animator>().SetLayerWeight(1, 1f);
        yield return new WaitForSeconds(invincibilityTime);
        GetComponent<Animator>().SetLayerWeight(1, 0f);
    }

    private void Update()
    {
        if (_invincibilityTimer > 0)
            _invincibilityTimer -= Time.deltaTime;

        if (_isDead && Input.GetKeyDown(KeyCode.R))
        {
            // reload scene
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }

        // update UI
        for (int i = 0; i < ui_hearts.Length; i++)
        {
            if (i < health)
                ui_hearts[i].sprite = ui_heart_full;
            else
                ui_hearts[i].sprite = ui_heart_empty;

            continue;
            if (i < Mathf.CeilToInt(health))
                ui_hearts[i].enabled = true;
            else
                ui_hearts[i].enabled = false;
        }
    }

    public void Die()
    {
        _isDead = true;
        Debug.Log("Player died");
        // disable player movement
        GetComponent<PlayerMovement>().enabled = false;
        // disable player skills
        GetComponent<PlayerSkills>().enabled = false;

        GetComponent<Animator>().Play("Player_Death");
        AudioManager.instance.Play("gameover");

        // death screen
        GameObject.Find("DeathScreen").GetComponent<Canvas>().enabled = true;
    }
}
