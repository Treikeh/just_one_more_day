using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using Unity.VisualScripting;
// CREDITS: Comp-3 Interactive - Youtube: https://www.youtube.com/watch?v=1EdLTF43d70


//This class creates a DialogueObject for each row in the DialogueDatabase


// DONE: Create scriptable objects from csv/tsv files
// DONE: Automaticly add portraits to scriptable object based on the name. (Just needs the names and character portraits)
// FIXED: Dialogue objects are removed form game objects when generating new dialogue, even if the objects hasn't been changed
// TODO: Add support for creating and saving DialogueObjects into subfolders by adding a "/" in the file name/ID (ex. Lab/lab_banter_01, Lab/lab_banter_02)

public class TsvToSo
{
    private static string dialogueTsvPath = "/Resources/Dialogue/DialogueDatabase.tsv";

    [MenuItem("Uitlities/Genertate Dialogue")]
    public static void GenerateDialogue()
    {
        string[] allLines = File.ReadAllLines(Application.dataPath + dialogueTsvPath);
        foreach(string s in allLines)
        {
            // Split data in rows by tab spaces
            string[] splitData = s.Split("\t");

            // Check that the file has the right amount of values
            if(splitData.Length < 3)
            {
                Debug.LogWarning("Dialogue doesn't have less than 3 values! If this is a mistake contact the programmer to fix it!");
                return;
            }

            // Update/Create dialogue objects
            DialogueSO dialogueObject = AssetDatabase.LoadAssetAtPath<DialogueSO>($"Assets/Resources/Dialogue/{splitData[0]}.asset");
            if(dialogueObject) // Update dialogue object if it exists
            {
                UpdateDialogueObject(dialogueObject ,splitData);
            }
            else // Create a new dialogue object if it doesn't exist
            {
                dialogueObject = ScriptableObject.CreateInstance<DialogueSO>();
                UpdateDialogueObject(dialogueObject, splitData);
                // Add object to asset folder
                AssetDatabase.CreateAsset(dialogueObject, $"Assets/Resources/Dialogue/{splitData[0]}.asset");
            }
            // Set asset as dirty to make sure that the Sentence list is actually saved (When object is dirty it loses support for undo)
            // Might also be a good idea to use Undo.RecordObject() on the object to allow add support for undo
            // Docs: https://docs.unity3d.com/6000.1/Documentation/ScriptReference/EditorUtility.SetDirty.html
            EditorUtility.SetDirty(dialogueObject);
        }
        // Save assets
        AssetDatabase.SaveAssets();
    }

    // Update values in dialogue object
    public static void UpdateDialogueObject(DialogueSO dialogueObject, string[] dialogueData)
    {
        dialogueObject.characterProfile = GetProfile(dialogueData[1]);
        // Get character name form profile
        dialogueObject.characterName = dialogueObject.characterProfile.characterName;
        dialogueObject.characterPortrait = GetPortrait(dialogueData[1]);
        dialogueObject.sentences = GetSentences(dialogueData[2]);
        dialogueObject.textSpeed = GetTextSpeed(dialogueData[3]);
    }

    // Load a protrait using the characters name as identifier.
    // If no name is given or the name isn't in the function, load the Default portrait.
    public static Sprite GetPortrait(string characterName)
    {
        // Return the characters portrait. If the character isn't found return the default protrait
        Debug.Log(characterName);
        // The "??" executes the second AssetDatabase if the first AssetDatabase is null. Basicly the same as a if (asset == null) but in 1 line
        return AssetDatabase.LoadAssetAtPath<Sprite>($"Assets/Characters/{characterName}/{characterName}_Portrait.png") ?? AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Characters/Default_Portrait.png");
    }

    // Does the same as GetPortrait only that it gets a character profile instead of a sprite
    public static CharacterProfileSO GetProfile(string characterName)
    {
        return AssetDatabase.LoadAssetAtPath<CharacterProfileSO>($"Assets/Characters/{characterName}/{characterName}_Profile.asset") ?? AssetDatabase.LoadAssetAtPath<CharacterProfileSO>("Assets/Characters/Default_Profile.asset");
    }

    // Turn text field in database into list of sentences
    public static List<string> GetSentences(string dialogueText)
    {
        List<string> sentences = new();

        // Split dialogue into the different sentences
        string[] sentenceSplit = dialogueText.Split(" | ");

        // Add each new line into the sentenceQueue
        foreach(string sentence in sentenceSplit)
        {
            sentences.Add(sentence);
        }

        return sentences;
    }

    // Get the text speed from the dialogue data
    public static float GetTextSpeed(string speed)
    {
        // This was a switch statement
        return speed switch
        {
            "Fast" => 0.01f,
            "Slow" => 0.1f,
            _ => 0.05f,
        };
    }
}
