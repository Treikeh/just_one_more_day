using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewCharacterProfile", menuName = "Data/Character profile", order = 1)]
public class CharacterProfileSO : ScriptableObject
{
    public string characterName;
    [SerializeField] private Sprite characterPortrait;
    [TextArea]
    public string characterDescription;
}
