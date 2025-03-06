using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Transform playerTransform => GameManager.instance.playerTransform;
    void Start()
    {
        CharacterScriptable character = GameManager.instance.playerData;
        
        GameObject spritePrefab = Instantiate(character.spritePrefab);
        spritePrefab.transform.position = playerTransform.position;
        spritePrefab.transform.parent = playerTransform;
        spritePrefab.SetActive(true);
        
        GameObject startingWeapon = Instantiate(character.startingWeapon);
        startingWeapon.transform.position = playerTransform.position;
        startingWeapon.transform.parent = playerTransform;
        startingWeapon.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
