using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    void Start()
    {
        CharacterScriptable character = GameManager.character;
        
        GameObject spritePrefab = Instantiate(character.spritePrefab);
        spritePrefab.transform.position = transform.position;
        spritePrefab.transform.parent = transform;
        spritePrefab.SetActive(true);
        
        GameObject startingWeapon = Instantiate(character.startingWeapon);
        startingWeapon.transform.position = transform.position;
        startingWeapon.transform.parent = transform;
        startingWeapon.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
