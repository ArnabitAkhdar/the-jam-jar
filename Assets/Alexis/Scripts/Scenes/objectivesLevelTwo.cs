using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectivesLevelTwo : MonoBehaviour
{
    #region Private
    private bool hasCollectedAJournal = false, hasCollectedAPlant = false;
    private bool hasCompletedAllObjectives = false;

    private dialogueScript dialogue;

    private sceneTransition referenceToSceneTransition;

    private userInterfaceManagement referenceToUserInterfaceManagement;
    #endregion

    #region Public
    public GameObject journal;
    #endregion

    void Start()
    {
        dialogue = GetComponent<dialogueScript>();

        referenceToSceneTransition = GetComponent<sceneTransition>();

        referenceToUserInterfaceManagement = GameObject.FindGameObjectWithTag("UserInterface").GetComponent<userInterfaceManagement>();
    }
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<botanicalDexJournal>().flowersPickedUp != 0 && hasCollectedAJournal) { hasCompletedAllObjectives = true; }

        if (!journal.activeSelf) { hasCollectedAJournal = true; }
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