using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectivesLevelOne : MonoBehaviour
{
    #region Private
    private bool collectedPlantPot = false;
    private bool hasCompletedAllObjectives = false;
    private bool hasTalkedToElder = false;

    private dialogueScript dialogue;

    private sceneTransition referenceToSceneTransition;

    private userInterfaceManagement referenceToUserInterfaceManagement;
    #endregion

    #region Public
    public dialogueScript plantPotDialogue;

    public GameObject plantPot;
    #endregion

    void Start() 
    { 
        dialogue = GetComponent<dialogueScript>();

        referenceToSceneTransition = GetComponent<sceneTransition>();

        referenceToUserInterfaceManagement = GameObject.FindGameObjectWithTag("UserInterface").GetComponent<userInterfaceManagement>();
    }
    void Update() 
    { 
        if (collectedPlantPot && hasTalkedToElder) { hasCompletedAllObjectives = true; }

        if (!plantPot.activeSelf) { collectedPlantPot = true; }

        if(referenceToUserInterfaceManagement.referenceToDialogueManagement.currentDialogueScript == plantPotDialogue) { hasTalkedToElder = true; }
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    { 
        if (collision.CompareTag("Player")) 
        {
            if (!hasCompletedAllObjectives) { referenceToUserInterfaceManagement.displayDialogueUserInterface(dialogue); }
            else { referenceToSceneTransition.transitionScene(referenceToSceneTransition.transitionToScene); } 
        }
    }
}