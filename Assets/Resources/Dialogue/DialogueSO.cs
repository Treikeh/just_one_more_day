using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewDialogue", menuName = "Data/Dialogue", order = 1)]
public class DialogueSO : ScriptableObject
{
    public CharacterProfileSO characterProfile;
    public string characterName;
    public Sprite characterPortrait;
    public List<string> sentences;
    public float textSpeed = 0.05f;
    public bool endAutomatically = false;
}
