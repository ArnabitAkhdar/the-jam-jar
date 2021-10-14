using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(interactable))]
public class interactableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        interactable interactable_ = (interactable)target;

        // canPickUp, canInspect, canTalkTo, canUse
        GUILayout.Label("Type of Interaction\n-------------------");
        interactable_.canPickUp = EditorGUILayout.Toggle("Can Pick Up?", interactable_.canPickUp);
        
        if (interactable_.canPickUp)
        {
            interactable_.canInspect = false;
            interactable_.canTalkTo = false;
            interactable_.canUse = false;

            interactable_.isAPlant = EditorGUILayout.Toggle("     - Is A Plant?", interactable_.isAPlant);

            if(interactable_.isAPlant)
            {
                interactable_.isEdible = EditorGUILayout.Toggle("          Is Edible?", interactable_.isEdible);
                interactable_.isFlower = EditorGUILayout.Toggle("          Is Flower?", interactable_.isFlower);
                interactable_.isHerb = EditorGUILayout.Toggle("          Is Herb?", interactable_.isHerb);
            }
        }

        interactable_.canInspect = EditorGUILayout.Toggle("Can Inspect?", interactable_.canInspect);

        if (interactable_.canInspect)
        {
            interactable_.canPickUp = false;
            interactable_.canTalkTo = false;
            interactable_.canUse = false;
        }

        interactable_.canTalkTo = EditorGUILayout.Toggle("Can Talk To?", interactable_.canTalkTo);

        if (interactable_.canTalkTo)
        {
            interactable_.canPickUp = false;
            interactable_.canInspect = false;
            interactable_.canUse = false;
        }

        interactable_.canUse = EditorGUILayout.Toggle("Can Use?", interactable_.canUse);

        if (interactable_.canUse)
        {
            interactable_.canPickUp = false;
            interactable_.canInspect = false;
            interactable_.canTalkTo = false;
        }

        if(interactable_.canPickUp || interactable_.canInspect || interactable_.canTalkTo || interactable_.canUse)
        {
            // triggerDialogue, triggerMapUpdate, triggerJournalUpdate, triggerDexUpdate
            GUILayout.Label("\nInteraction Trigger\n-------------------");

            interactable_.triggerDialogue = EditorGUILayout.Toggle("Will Trigger Dialogue?", interactable_.triggerDialogue);

            if (interactable_.triggerDialogue)
            {
                interactable_.triggerDialogueBasedOnInteractableCollected = EditorGUILayout.Toggle("     - Based on Item Collected?", interactable_.triggerDialogueBasedOnInteractableCollected);

                if(!interactable_.triggerDialogueBasedOnInteractableCollected) { interactable_.dialogue = (dialogueScript)EditorGUILayout.ObjectField("     Dialogue", interactable_.dialogue, typeof(dialogueScript), true); }
                else 
                {
                    interactable_.item = (GameObject)EditorGUILayout.ObjectField("          Item Needed", interactable_.item, typeof(GameObject), true);

                    interactable_.itemAcquiredDialogue = (dialogueScript)EditorGUILayout.ObjectField("          Item Acquired Dialogue", interactable_.itemAcquiredDialogue, typeof(dialogueScript), true);
                    interactable_.itemUnacquiredDialogue = (dialogueScript)EditorGUILayout.ObjectField("          Item Unacquired Dialogue", interactable_.itemUnacquiredDialogue, typeof(dialogueScript), true);
                }
                                
                if (GUILayout.Button("Add Dialogue Script", GUILayout.Height(25f))) { interactable_.gameObject.AddComponent<dialogueScript>(); }

                GUILayout.Label("\n");
            }

            // If canTalkTo is marked true, triggers will occur after end of dialogue
            interactable_.triggerMapUpdate = EditorGUILayout.Toggle("Will Trigger Map Update?", interactable_.triggerMapUpdate);
            
            interactable_.triggerJournalUpdate = EditorGUILayout.Toggle("Will Trigger Journal Update?", interactable_.triggerJournalUpdate);
            
            if(interactable_.triggerJournalUpdate) { interactable_.journalEntryNumber = EditorGUILayout.IntField("     Journal Entry Number", interactable_.journalEntryNumber); }

            interactable_.triggerDexUpdate = EditorGUILayout.Toggle("Will Trigger Dex Update?", interactable_.triggerDexUpdate);

            if (interactable_.triggerDexUpdate) { interactable_.dexEntryNumber = EditorGUILayout.IntField("     Dex Entry Number", interactable_.dexEntryNumber); }

            if (interactable_.triggerMapUpdate && interactable_.triggerJournalUpdate && interactable_.triggerDexUpdate) 
            {
                GUILayout.Label("----------------------------");

                interactable_.displayDexJournalAndMapButton = EditorGUILayout.Toggle("     - Display Dex/Journal/Map Button?", interactable_.displayDexJournalAndMapButton); 
            
                if(interactable_.displayDexJournalAndMapButton)
                {
                    interactable_.displayDuringDialogue = EditorGUILayout.Toggle("          Display During Dialogue?", interactable_.displayDuringDialogue);

                    if(interactable_.displayDuringDialogue) 
                    { 
                        interactable_.atDialogueString = EditorGUILayout.IntField("               At Dialogue String #", interactable_.atDialogueString);

                        interactable_.botanicalDexJournalMapButton = (GameObject)EditorGUILayout.ObjectField("               Botanical Dex/Journal/Map Button", interactable_.botanicalDexJournalMapButton, typeof(GameObject), true);

                        interactable_.dialogue = (dialogueScript)EditorGUILayout.ObjectField("               Dialogue", interactable_.dialogue, typeof(dialogueScript), true);
                    }
                }
            }
        }
    }
}
