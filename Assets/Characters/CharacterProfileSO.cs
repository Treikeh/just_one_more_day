using UnityEngine;


[CreateAssetMenu(fileName = "NewCharacterProfile", menuName = "Data/Character profile", order = 1)]
public class CharacterProfileSO : ScriptableObject
{
    public string characterName;
    public Sprite characterPortrait;
    [TextArea]
    public string characterDescription;
}
