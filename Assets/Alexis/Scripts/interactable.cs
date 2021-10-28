using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class interactable : MonoBehaviour
{
    #region Private
    private bool hasBeenInteractedWith = false;

    private BoxCollider2D boxCollider2D;

    private playerController referenceToPlayerController;

    private userInterfaceManagement referenceToUserInterfaceManagement;
    #endregion

    #region Public
    // Type of Interaction
    public bool canPickUp = false, canInspect = false, canTalkTo = false, canUse = false;
    // Interaction Trigger
    public bool triggerDialogue = false, triggerMapUpdate = false, triggerJournalUpdate = false, triggerDexUpdate = false;
    public bool displayDexJournalAndMapButton = false;
    public bool displayDuringDialogue = false;
    public bool isEdible = false, isFlower = false, isHerb = false;
    public bool isAPlant = false;

    public dialogueScript dialogue;

    public GameObject botanicalDexJournalMapButton;
    public GameObject referenceToObjectivesLevelOne;

    public int atDialogueString = -1;
    public int selectedLevel = -1;

    // triggerDialogue Optional Variables
    public bool triggerDialogueBasedOnInteractableCollected = false;

    public dialogueScript itemAcquiredDialogue, itemUnacquiredDialogue;
    // ----------------------------------

    // triggerDexUpdate Optional Variables
    public int dexEntryNumber = -1;
    // ---------------------------------------

    // triggerJournalUpdate Optional Variables
    public int journalEntryNumber = -1;
    // ---------------------------------------

    public GameObject item;
    #endregion

    void Start() 
    { 
        if(!isAPlant)
        {
            boxCollider2D = gameObject.AddComponent<BoxCollider2D>();
            boxCollider2D.isTrigger = true;
            boxCollider2D.size *= 2;
        }

        referenceToPlayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();

        referenceToUserInterfaceManagement = GameObject.FindGameObjectWithTag("UserInterface").GetComponent<userInterfaceManagement>();
    }

    void Update() 
    { 
        if (triggerMapUpdate && triggerJournalUpdate && triggerDexUpdate) 
        { 
            if (displayDexJournalAndMapButton) 
            { 
                if (displayDuringDialogue) 
                { 
                    if (referenceToUserInterfaceManagement.dialogueUserInterface.activeSelf && referenceToUserInterfaceManagement.referenceToDialogueManagement.currentDialogueScript == dialogue && referenceToUserInterfaceManagement.referenceToDialogueManagement.atDialogueString == atDialogueString) 
                    { 
                        botanicalDexJournalMapButton.SetActive(true);

                        botanicalDexJournalMapButton.GetComponent<Button>().enabled = false;
                    }
                    else if (referenceToUserInterfaceManagement.referenceToDialogueManagement.currentDialogueScript != dialogue) { botanicalDexJournalMapButton.GetComponent<Button>().enabled = true; }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    { 
        if (collision.CompareTag("Player")) 
        {
            referenceToUserInterfaceManagement.interactableText.gameObject.SetActive(true);

            if (canPickUp) { referenceToUserInterfaceManagement.interactableText.text = /*[E/LMB]*/ "[E] - Pick Up"; }
            else if (canInspect) { referenceToUserInterfaceManagement.interactableText.text = /*[E/LMB]*/ "[E] - Inspect"; }
            else if (canTalkTo) { referenceToUserInterfaceManagement.interactableText.text = /*[E/LMB]*/ "[E] - Talk"; }
            else { referenceToUserInterfaceManagement.interactableText.text = /*[E/LMB]*/ "[E] - Use"; }
        } 
    }

    private void OnTriggerExit2D(Collider2D collision) { if (collision.CompareTag("Player")) { referenceToUserInterfaceManagement.interactableText.gameObject.SetActive(false); } }

    private void OnTriggerStay2D(Collider2D collision) 
    { 
        if (collision.CompareTag("Player") && /*(*/Input.GetKeyDown(KeyCode.E)/* || Input.GetMouseButtonDown(0))*/)
        {
            if (triggerDialogue)
            {
                if (!triggerDialogueBasedOnInteractableCollected) { referenceToUserInterfaceManagement.displayDialogueUserInterface(dialogue); }
                else
                {
                    if (!item.activeSelf) { referenceToUserInterfaceManagement.displayDialogueUserInterface(itemAcquiredDialogue); }
                    else { referenceToUserInterfaceManagement.displayDialogueUserInterface(itemUnacquiredDialogue); }
                }
            }

            if (triggerMapUpdate) { }

            if (triggerJournalUpdate) 
            {
                if(journalEntryNumber != -1)
                {
                    if (!referenceToPlayerController.GetComponent<botanicalDexJournal>().journalEntries[journalEntryNumber - 1].entryCollected)
                    {
                        referenceToPlayerController.GetComponent<botanicalDexJournal>().journalEntries[journalEntryNumber - 1].entryCollected = true;
                        referenceToPlayerController.GetComponent<botanicalDexJournal>().updateJournal(journalEntryNumber);
                    }
                }
            }
            
            if (triggerDexUpdate) 
            {
                if (dexEntryNumber != -1)
                {
                    if (!referenceToPlayerController.GetComponent<botanicalDexJournal>().dexEntries[dexEntryNumber].entryCollected)
                    {
                        referenceToPlayerController.GetComponent<botanicalDexJournal>().dexEntries[dexEntryNumber].entryCollected = true;
                        referenceToPlayerController.GetComponent<botanicalDexJournal>().updateDex(dexEntryNumber);
                    }
                }
            }

            if (canPickUp) 
            {
                if(isAPlant)
                {      
                    if (isEdible) { PlayerPrefs.SetInt("ediblesPickedUp", PlayerPrefs.GetInt("ediblesPickedUp") + 1); referenceToPlayerController.GetComponent<botanicalDexJournal>().ediblesPickedUp += 1; }
                    if (isFlower) { PlayerPrefs.SetInt("flowersPickedUp", PlayerPrefs.GetInt("flowersPickedUp") + 1); referenceToPlayerController.GetComponent<botanicalDexJournal>().flowersPickedUp += 1; }
                    if (isHerb) { PlayerPrefs.SetInt("herbsPickedUp", PlayerPrefs.GetInt("herbsPickedUp") + 1); referenceToPlayerController.GetComponent<botanicalDexJournal>().herbsPickedUp += 1; }
                }
                
                gameObject.SetActive(false); 
            }
            else if (canInspect) { hasBeenInteractedWith = true; }
            else if (canTalkTo) { hasBeenInteractedWith = true; }
            else { hasBeenInteractedWith = true; }
        }

        if (!hasBeenInteractedWith) { referenceToUserInterfaceManagement.interactableText.color = Color.white; }
        else { referenceToUserInterfaceManagement.interactableText.color = Color.grey; }
    }
}