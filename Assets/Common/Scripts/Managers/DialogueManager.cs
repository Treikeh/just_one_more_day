using System;
using System.Collections.Generic;
using UnityEngine.Events;

// // Don't like using MonoBehaviour manager classes since it means you need to drag and drop it into every scene where you use it's functionality.
// // Unfortunately it works very well, so here i am.
// // TODO: Find a way to load manager scripts automatically when running game in editor
// There was a bunch of code here earlier, but i moved most of it into the DialogueBox script.
// Now this scripts only acts as a messenger between the DialogeTrigger and the DialogueBox script.
// This was done to avoid hvaing both scripts be dependant on an Instance that might be missing.
// It also means we don't have to add a DialogueManager GameObject to almost every scene we're working.


// *The class should probably be renamed since it's no longer managing anyting anymore
public class DialogueManager
{
    public static Action<List<DialogueObject>, UnityEvent> startDialogue;
    public static Action dialogueStarted;
    public static Action dialogueFinished;
}
