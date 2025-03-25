using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataContainer", menuName = "ScriptableObjects/DataContainer", order = 1)]
public class DataContainerScriptable : ScriptableObject
{
    public CharacterScriptable character;
    public void SetCharacter(CharacterScriptable character) => this.character = character;
}
