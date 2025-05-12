using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewCharacterProfile", menuName = "Data/Character profile", order = 1)]
public class CharacterProfileSO : ScriptableObject
{
    [SerializeField] private string characterName;
    [SerializeField] private Sprite characterPortrait;
    [TextArea]
    [SerializeField] private string characterDescription;
}
