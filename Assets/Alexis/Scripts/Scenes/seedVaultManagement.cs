using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class quizEntry
{    
    public dialogueScript quizDialogue;

    public int entryIndex;
}

public class seedVaultManagement : MonoBehaviour
{
    #region Private
    private bool hasChosenCorrectAnswer = false;
    private bool hasGoneThroughDialogue = false;
    private bool hasTransitionBeenSetup = false;
    private bool hasQuizBeenCompleted = false;

    private dialogueScript dialogue;

    private int currentQuestion = 0;

    private List<quizEntry> quizEntries = new List<quizEntry>();
    #endregion

    #region Public
    public dialogueManagement referenceToDialogueManagement;

    public GameObject dialogueUserInterface;

    public Image fadePanel;
    #endregion

    void Start() 
    { 
        quizEntries.Clear();

        setUpQuiz();
    }

    void Update()
    {
        if(referenceToDialogueManagement.currentDialogueScript == GetComponent<dialogueScript>())
        {
            hasGoneThroughDialogue = true;

            if(!hasTransitionBeenSetup)
            {
                fadePanel.gameObject.SetActive(true);
                fadePanel.color = new Color(fadePanel.color.r, fadePanel.color.g, fadePanel.color.b, 1f);
                fadePanel.canvasRenderer.SetAlpha(0f);

                hasTransitionBeenSetup = true;
            }

            if (referenceToDialogueManagement.atDialogueString == 1) { fadePanel.CrossFadeAlpha(1f, 1f, false); }
            else if (referenceToDialogueManagement.atDialogueString == 2) 
            {
                dialogueUserInterface.transform.GetChild(0).GetComponent<RectTransform>().position = new Vector3(dialogueUserInterface.transform.GetChild(0).GetComponent<RectTransform>().position.x, 279f, dialogueUserInterface.transform.GetChild(0).GetComponent<RectTransform>().position.z);
                dialogueUserInterface.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);

                dialogueUserInterface.transform.GetChild(1).GetComponent<RectTransform>().offsetMax = new Vector2(dialogueUserInterface.transform.GetChild(1).GetComponent<RectTransform>().offsetMax.x, 50f);
                dialogueUserInterface.transform.GetChild(1).GetComponent<RectTransform>().offsetMin = new Vector2(dialogueUserInterface.transform.GetChild(1).GetComponent<RectTransform>().offsetMin.x, -50f);
            }
        }
        else if(referenceToDialogueManagement.currentDialogueScript == null && hasGoneThroughDialogue) { SceneManager.LoadScene(0); }

        if (!hasQuizBeenCompleted) 
        { 
            if (hasChosenCorrectAnswer || referenceToDialogueManagement.currentDialogueScript != null) { proceedQuiz(); } 
            
            if(!GameObject.FindGameObjectWithTag("UserInterface").GetComponent<userInterfaceManagement>().transform.GetChild(1).gameObject.activeSelf && referenceToDialogueManagement.currentDialogueScript == null) { GameObject.FindGameObjectWithTag("UserInterface").GetComponent<userInterfaceManagement>().transform.GetChild(1).gameObject.SetActive(true); }
        }
    }

    private void proceedQuiz()
    {
        if(hasChosenCorrectAnswer && referenceToDialogueManagement.currentDialogueScript == null)
        {
            currentQuestion += 1;

            if(currentQuestion != 3)
            {
                GameObject.FindGameObjectWithTag("UserInterface").GetComponent<userInterfaceManagement>().displayDialogueUserInterface(quizEntries[currentQuestion].quizDialogue);
                GetComponent<interactable>().dialogue = quizEntries[currentQuestion].quizDialogue;
            }
            else 
            { 
                hasQuizBeenCompleted = true;

                GameObject.FindGameObjectWithTag("UserInterface").GetComponent<userInterfaceManagement>().displayDialogueUserInterface(GetComponent<dialogueScript>());
                GetComponent<interactable>().dialogue = GetComponent<dialogueScript>();
            }

            hasChosenCorrectAnswer = false;
        }
        else if(referenceToDialogueManagement.currentDialogueScript.dialogue[0] == "That is correct! Next Question...") { hasChosenCorrectAnswer = true; }

        GameObject.FindGameObjectWithTag("UserInterface").GetComponent<userInterfaceManagement>().transform.GetChild(1).gameObject.SetActive(false);
    }

    private void setUpQuiz()
    {
        // List<int> temporarySelectedQuizEntryIndexes = new List<int>();

        // for(int indexCounter = 0; indexCounter < 3; indexCounter++) { temporarySelectedQuizEntryIndexes.Add(indexCounter); }

        for (int indexCounter = 0; indexCounter < 3; indexCounter++)
        {
            int randomSelectedNumber;

            quizEntry newQuizEntry = new quizEntry();

            newQuizEntry.entryIndex = indexCounter;

            newQuizEntry.quizDialogue = gameObject.AddComponent<dialogueScript>();
            
            newQuizEntry.quizDialogue.dialogue = new string[1];

            newQuizEntry.quizDialogue.hasResponses = true;
            
            newQuizEntry.quizDialogue.responses = new dialogueResponse[3];

            newQuizEntry.quizDialogue.responses[0] = gameObject.AddComponent<dialogueResponse>();
            newQuizEntry.quizDialogue.responses[0].dialogueScript = gameObject.AddComponent<dialogueScript>();
            newQuizEntry.quizDialogue.responses[0].dialogueScript.dialogue = new string[1];

            newQuizEntry.quizDialogue.responses[1] = gameObject.AddComponent<dialogueResponse>();
            newQuizEntry.quizDialogue.responses[1].dialogueScript = gameObject.AddComponent<dialogueScript>();
            newQuizEntry.quizDialogue.responses[1].dialogueScript.dialogue = new string[1];

            newQuizEntry.quizDialogue.responses[2] = gameObject.AddComponent<dialogueResponse>();
            newQuizEntry.quizDialogue.responses[2].dialogueScript = gameObject.AddComponent<dialogueScript>();
            newQuizEntry.quizDialogue.responses[2].dialogueScript.dialogue = new string[1];

            // randomSelectedNumber = temporarySelectedQuizEntryIndexes[Random.Range(0, temporarySelectedQuizEntryIndexes.Count)];
            if(indexCounter == 0) { randomSelectedNumber = Random.Range(0, 5); }
            else if(indexCounter == 1) { randomSelectedNumber = Random.Range(5, 10); }
            else { randomSelectedNumber = Random.Range(10, 15); }

            // Scene 1
            if (randomSelectedNumber == 0)
            {
                newQuizEntry.quizDialogue.dialogue[0] = "In which area of the world are Poppies mostly found?";

                newQuizEntry.quizDialogue.responses[0].responseText = "Southern Hemisphere";
                newQuizEntry.quizDialogue.responses[0].dialogueScript.dialogue[0] = "I'm sorry, but that is incorrect. Please try again!";

                newQuizEntry.quizDialogue.responses[1].responseText = "Equator";
                newQuizEntry.quizDialogue.responses[1].dialogueScript.dialogue[0] = "I'm sorry, but that is incorrect. Please try again!";

                newQuizEntry.quizDialogue.responses[2].responseText = "Northern Hemisphere";
                newQuizEntry.quizDialogue.responses[2].dialogueScript.dialogue[0] = "That is correct! Next Question...";
            }
            else if (randomSelectedNumber == 1)
            {
                newQuizEntry.quizDialogue.dialogue[0] = "How many species are in the Morning Glory family?";

                newQuizEntry.quizDialogue.responses[0].responseText = "2,400";
                newQuizEntry.quizDialogue.responses[0].dialogueScript.dialogue[0] = "I'm sorry, but that is incorrect. Please try again!";

                newQuizEntry.quizDialogue.responses[1].responseText = "1,600";
                newQuizEntry.quizDialogue.responses[1].dialogueScript.dialogue[0] = "That is correct! Next Question...";

                newQuizEntry.quizDialogue.responses[2].responseText = "900";
                newQuizEntry.quizDialogue.responses[2].dialogueScript.dialogue[0] = "I'm sorry, but that is incorrect. Please try again!";
            }
            else if (randomSelectedNumber == 2)
            {
                newQuizEntry.quizDialogue.dialogue[0] = "What colours are Dandelions?";

                newQuizEntry.quizDialogue.responses[0].responseText = "Green and Yellow";
                newQuizEntry.quizDialogue.responses[0].dialogueScript.dialogue[0] = "I'm sorry, but that is incorrect. Please try again!";

                newQuizEntry.quizDialogue.responses[1].responseText = "Green and White";
                newQuizEntry.quizDialogue.responses[1].dialogueScript.dialogue[0] = "I'm sorry, but that is incorrect. Please try again!";

                newQuizEntry.quizDialogue.responses[2].responseText = "All of the above";
                newQuizEntry.quizDialogue.responses[2].dialogueScript.dialogue[0] = "That is correct! Next Question...";
            }
            // Scene 2
            else if (randomSelectedNumber == 3)
            {
                newQuizEntry.quizDialogue.dialogue[0] = "Where are Water Lilies native to?";

                newQuizEntry.quizDialogue.responses[0].responseText = "tropical and temperate climates";
                newQuizEntry.quizDialogue.responses[0].dialogueScript.dialogue[0] = "That is correct! Next Question...";

                newQuizEntry.quizDialogue.responses[1].responseText = "Continental climates";
                newQuizEntry.quizDialogue.responses[1].dialogueScript.dialogue[0] = "I'm sorry, but that is incorrect. Please try again!";

                newQuizEntry.quizDialogue.responses[2].responseText = "Mediterranean climates";
                newQuizEntry.quizDialogue.responses[2].dialogueScript.dialogue[0] = "I'm sorry, but that is incorrect. Please try again!";
            }
            else if (randomSelectedNumber == 4)
            {
                newQuizEntry.quizDialogue.dialogue[0] = "What other plant do ramps look like?";

                newQuizEntry.quizDialogue.responses[0].responseText = "carrot";
                newQuizEntry.quizDialogue.responses[0].dialogueScript.dialogue[0] = "I'm sorry, but that is incorrect. Please try again!";

                newQuizEntry.quizDialogue.responses[1].responseText = "scallions";
                newQuizEntry.quizDialogue.responses[1].dialogueScript.dialogue[0] = "That is correct! Next Question...";

                newQuizEntry.quizDialogue.responses[2].responseText = "potatoes";
                newQuizEntry.quizDialogue.responses[2].dialogueScript.dialogue[0] = "I'm sorry, but that is incorrect. Please try again!";
            }
            else if (randomSelectedNumber == 5)
            {
                newQuizEntry.quizDialogue.dialogue[0] = "What is the ideal place for Watercress to grow?";

                newQuizEntry.quizDialogue.responses[0].responseText = "cool flowing streams";
                newQuizEntry.quizDialogue.responses[0].dialogueScript.dialogue[0] = "That is correct! Next Question...";

                newQuizEntry.quizDialogue.responses[1].responseText = "mineral rich soil";
                newQuizEntry.quizDialogue.responses[1].dialogueScript.dialogue[0] = "I'm sorry, but that is incorrect. Please try again!";

                newQuizEntry.quizDialogue.responses[2].responseText = "a pot on your window";
                newQuizEntry.quizDialogue.responses[2].dialogueScript.dialogue[0] = "I'm sorry, but that is incorrect. Please try again!";
            }
            // Scene 3
            else if (randomSelectedNumber == 6)
            {
                newQuizEntry.quizDialogue.dialogue[0] = "What’s one of the most cultivated fruits you encountered?";

                newQuizEntry.quizDialogue.responses[0].responseText = "Apples";
                newQuizEntry.quizDialogue.responses[0].dialogueScript.dialogue[0] = "That is correct! Next Question...";

                newQuizEntry.quizDialogue.responses[1].responseText = "Kiwi";
                newQuizEntry.quizDialogue.responses[1].dialogueScript.dialogue[0] = "I'm sorry, but that is incorrect. Please try again!";

                newQuizEntry.quizDialogue.responses[2].responseText = "Persimmons";
                newQuizEntry.quizDialogue.responses[2].dialogueScript.dialogue[0] = "I'm sorry, but that is incorrect. Please try again!";
            }
            else if (randomSelectedNumber == 7)
            {
                newQuizEntry.quizDialogue.dialogue[0] = "Where is the Tiger Flower native to?";

                newQuizEntry.quizDialogue.responses[0].responseText = "Hawaii";
                newQuizEntry.quizDialogue.responses[0].dialogueScript.dialogue[0] = "I'm sorry, but that is incorrect. Please try again!";

                newQuizEntry.quizDialogue.responses[1].responseText = "Mexico and Chile";
                newQuizEntry.quizDialogue.responses[1].dialogueScript.dialogue[0] = "That is correct! Next Question...";

                newQuizEntry.quizDialogue.responses[2].responseText = "Nepal and Tibet";
                newQuizEntry.quizDialogue.responses[2].dialogueScript.dialogue[0] = "I'm sorry, but that is incorrect. Please try again!";
            }
            else if (randomSelectedNumber == 8)
            {
                newQuizEntry.quizDialogue.dialogue[0] = "What family of flowers is the Pansy in?";

                newQuizEntry.quizDialogue.responses[0].responseText = "Poppies";
                newQuizEntry.quizDialogue.responses[0].dialogueScript.dialogue[0] = "I'm sorry, but that is incorrect. Please try again!";

                newQuizEntry.quizDialogue.responses[1].responseText = "Daisies";
                newQuizEntry.quizDialogue.responses[1].dialogueScript.dialogue[0] = "I'm sorry, but that is incorrect. Please try again!";

                newQuizEntry.quizDialogue.responses[2].responseText = "Violets";
                newQuizEntry.quizDialogue.responses[2].dialogueScript.dialogue[0] = "That is correct! Next Question...";
            }
            // Scene 4
            else if (randomSelectedNumber == 9)
            {
                newQuizEntry.quizDialogue.dialogue[0] = "What area of the world is Sage native to?";

                newQuizEntry.quizDialogue.responses[0].responseText = "Mediterranean";
                newQuizEntry.quizDialogue.responses[0].dialogueScript.dialogue[0] = "That is correct! Next Question...";

                newQuizEntry.quizDialogue.responses[1].responseText = "North America";
                newQuizEntry.quizDialogue.responses[1].dialogueScript.dialogue[0] = "I'm sorry, but that is incorrect. Please try again!";

                newQuizEntry.quizDialogue.responses[2].responseText = "Australia";
                newQuizEntry.quizDialogue.responses[2].dialogueScript.dialogue[0] = "I'm sorry, but that is incorrect. Please try again!";
            }
            else if (randomSelectedNumber == 10)
            {
                newQuizEntry.quizDialogue.dialogue[0] = "What country was Basil first found in?";

                newQuizEntry.quizDialogue.responses[0].responseText = "Greece";
                newQuizEntry.quizDialogue.responses[0].dialogueScript.dialogue[0] = "I'm sorry, but that is incorrect. Please try again!";

                newQuizEntry.quizDialogue.responses[1].responseText = "India";
                newQuizEntry.quizDialogue.responses[1].dialogueScript.dialogue[0] = "That is correct! Next Question...";

                newQuizEntry.quizDialogue.responses[2].responseText = "Japan";
                newQuizEntry.quizDialogue.responses[2].dialogueScript.dialogue[0] = "I'm sorry, but that is incorrect. Please try again!";
            }
            else if (randomSelectedNumber == 11)
            {
                newQuizEntry.quizDialogue.dialogue[0] = "How many herb species of Geranium are there?";

                newQuizEntry.quizDialogue.responses[0].responseText = "300";
                newQuizEntry.quizDialogue.responses[0].dialogueScript.dialogue[0] = "That is correct! Next Question...";

                newQuizEntry.quizDialogue.responses[1].responseText = "600";
                newQuizEntry.quizDialogue.responses[1].dialogueScript.dialogue[0] = "I'm sorry, but that is incorrect. Please try again!";

                newQuizEntry.quizDialogue.responses[2].responseText = "900";
                newQuizEntry.quizDialogue.responses[2].dialogueScript.dialogue[0] = "I'm sorry, but that is incorrect. Please try again!";
            }
            // Scene 5
            else if (randomSelectedNumber == 12)
            {
                newQuizEntry.quizDialogue.dialogue[0] = "What’s the cross between sweet cherries and sour cherries called?";

                newQuizEntry.quizDialogue.responses[0].responseText = "Counts";
                newQuizEntry.quizDialogue.responses[0].dialogueScript.dialogue[0] = "I'm sorry, but that is incorrect. Please try again!";

                newQuizEntry.quizDialogue.responses[1].responseText = "Dukes";
                newQuizEntry.quizDialogue.responses[1].dialogueScript.dialogue[0] = "That is correct! Next Question...";

                newQuizEntry.quizDialogue.responses[2].responseText = "Earls";
                newQuizEntry.quizDialogue.responses[2].dialogueScript.dialogue[0] = "I'm sorry, but that is incorrect. Please try again!";
            }
            else if (randomSelectedNumber == 13)
            {
                newQuizEntry.quizDialogue.dialogue[0] = "What vitamin are Strawberries rich in?";

                newQuizEntry.quizDialogue.responses[0].responseText = "Vitamin E";
                newQuizEntry.quizDialogue.responses[0].dialogueScript.dialogue[0] = "I'm sorry, but that is incorrect. Please try again!";

                newQuizEntry.quizDialogue.responses[1].responseText = "Vitamin D";
                newQuizEntry.quizDialogue.responses[1].dialogueScript.dialogue[0] = "I'm sorry, but that is incorrect. Please try again!";

                newQuizEntry.quizDialogue.responses[2].responseText = "Vitamin C";
                newQuizEntry.quizDialogue.responses[2].dialogueScript.dialogue[0] = "That is correct! Next Question...";
            }
            else
            {
                newQuizEntry.quizDialogue.dialogue[0] = "Where are Forget-Me-Nots mostly found?";

                newQuizEntry.quizDialogue.responses[0].responseText = "Mountains";
                newQuizEntry.quizDialogue.responses[0].dialogueScript.dialogue[0] = "That is correct! Next Question...";

                newQuizEntry.quizDialogue.responses[1].responseText = "Valleys";
                newQuizEntry.quizDialogue.responses[1].dialogueScript.dialogue[0] = "I'm sorry, but that is incorrect. Please try again!";

                newQuizEntry.quizDialogue.responses[2].responseText = "Oceanside";
                newQuizEntry.quizDialogue.responses[2].dialogueScript.dialogue[0] = "I'm sorry, but that is incorrect. Please try again!";
            }

            // foreach (int _int in temporarySelectedQuizEntryIndexes) { if (_int == randomSelectedNumber) { temporarySelectedQuizEntryIndexes.Remove(_int); break; } }

            quizEntries.Add(newQuizEntry);
        }

        GetComponent<interactable>().dialogue = quizEntries[currentQuestion].quizDialogue;
    }
}
