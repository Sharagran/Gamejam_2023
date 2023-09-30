using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ManaLevel : MonoBehaviour
{
    private PlayerSkills _playerSkills;
    // Start is called before the first frame update
    void Start()
    {
        _playerSkills = GameObject.Find("Player").GetComponent<PlayerSkills>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log((float)_playerSkills.mana / _playerSkills.maxMana);
        this.GetComponent<Image>().fillAmount = (float)_playerSkills.mana / _playerSkills.maxMana;
    }
}
