using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
// CREDITS: Comp-3 Interactive - Youtube: https://www.youtube.com/watch?v=1EdLTF43d70


//This class creates a DialogueObject for each row in the DialogueDatabase


// DONE: Create scriptable objects from csv/tsv files
// DONE: Automaticly add portraits to scriptable object based on the name. (Just needs the names and character portraits)
// FIXED: Dialogue objects are removed form game objects when generating new dialogue, even if the objects hasn't been changed
// TODO: Add support for creating and saving DialogueObjects into subfolders by adding a "/" in the file name/ID (ex. Lab/lab_banter_01, Lab/lab_banter_02)

public class TsvToSo
{
    private static string dialogueTsvPath = "/Data/Dialogue/DialogueDatabase.tsv";

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
            DialogueObject dialogueObject = AssetDatabase.LoadAssetAtPath<DialogueObject>($"Assets/Data/Dialogue/{splitData[0]}.asset");
            if(dialogueObject) // Update dialogue object if it exists
            {
                UpdateDialogueObject(dialogueObject ,splitData);
            }
            else // Create a new dialogue object if it doesn't exist
            {
                dialogueObject = ScriptableObject.CreateInstance<DialogueObject>();
                UpdateDialogueObject(dialogueObject, splitData);
                // Add object to asset folder
                AssetDatabase.CreateAsset(dialogueObject, $"Assets/Data/Dialogue/{splitData[0]}.asset");
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
    public static void UpdateDialogueObject(DialogueObject dialogueObject, string[] dialogueData)
    {
        dialogueObject.characterName = dialogueData[1];
        dialogueObject.sentences = GetSentences(dialogueData[2]);
        // Load character portrait
        dialogueObject.characterPortrait = GetPortrait(dialogueData[1]);
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

    // Load a protrait using the characters name as identifier.
    // If no name is given or the name isn't in the function, load the Default portrait.
    public static Sprite GetPortrait(string characterName)
    {
        // Return the characters portrait. If the character isn't found return the default protrait
        Debug.Log(characterName);
        return AssetDatabase.LoadAssetAtPath<Sprite>($"Assets/Characters/{characterName}/{characterName}_Portrait.png") ?? AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Characters/DefaultPortrait.png");
    }
}
