using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Debug.Log((float)_playerSkills.mana / _playerSkills.maxMana);
        this.transform.localScale = new Vector3((float)_playerSkills.mana / _playerSkills.maxMana, 1f, 1f );
    }
}
