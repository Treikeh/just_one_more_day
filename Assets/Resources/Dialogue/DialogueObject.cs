using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewDialogueObject", menuName = "Data/Dialogue", order = 1)]
public class DialogueObject : ScriptableObject
{
    public string characterName;
    public Sprite characterPortrait;
    public List<string> sentences;
}
