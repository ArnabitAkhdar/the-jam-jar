using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class userInterfaceManagement : MonoBehaviour
{
    #region Private
    private GameObject previousTab;
    #endregion

    #region Public
    public bool isBotanicalDexAndJournalSectionOpened = false;

    public dialogueScript selectedDialogueScript;

    public dialogueManagement referenceToDialogueManagement;

    public GameObject botanicalDex, journal, map;
    public GameObject dialogueUserInterface;
    public GameObject pauseMenuUI;

    public TMP_Text dialogueText, nameText;
    public TMP_Text interactableText;
    #endregion

    void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);

        dialogueUserInterface.SetActive(false);

        interactableText.gameObject.SetActive(false);

        pauseMenuUI.SetActive(false);
    }

    private void Update() 
    { 
        if(Input.GetKeyDown(KeyCode.P))
        {
            pauseMenuUI.SetActive(true);

            GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>().isAbleToMove = false;
        }
    }

    // Button Function
    public void displayBotanicalDexJournalAndMapUI(bool _value) 
    {
        if(!_value)
        {
            isBotanicalDexAndJournalSectionOpened = false;

            transform.GetChild(0).gameObject.SetActive(_value);

            transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            isBotanicalDexAndJournalSectionOpened = true;

            transform.GetChild(0).gameObject.SetActive(_value);

            if (previousTab != null) { previousTab.SetActive(_value); }
            else { botanicalDex.SetActive(_value); }

            transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    // Button Function
    public void displayTab(int _selectedTab)
    {
        if (previousTab != null) { previousTab.SetActive(false); }
        else { botanicalDex.SetActive(false); }

        switch (_selectedTab)
        {
            // Botanical Dex
            case 0:
                botanicalDex.SetActive(true);

                previousTab = botanicalDex;
                break;
            // Journal
            case 1:
                journal.SetActive(true);

                previousTab = journal;
                break;
            // Map
            case 2:
                map.SetActive(true);

                previousTab = map;
                break;
        }
    }

    public void displayDialogueUserInterface(dialogueScript _dialogue)
    {
        dialogueUserInterface.SetActive(true);

        /*Left rectTransform.offsetMin.x;*/
        /*Right rectTransform.offsetMax.x;*/
        /*Top rectTransform.offsetMax.y;*/
        /*Bottom rectTransform.offsetMin.y;*/
        if (!_dialogue.isASoloConversation) 
        {
            dialogueText.GetComponent<RectTransform>().offsetMax = new Vector2(dialogueText.GetComponent<RectTransform>().offsetMax.x, 390.13f);

            nameText.gameObject.SetActive(true);
        }
        else
        {
            dialogueText.GetComponent<RectTransform>().offsetMax = new Vector2(dialogueText.GetComponent<RectTransform>().offsetMax.x, 440.13f);

            nameText.gameObject.SetActive(false);
        }

        GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>().isAbleToMove = false;

        StartCoroutine(referenceToDialogueManagement.stepThroughDialogue(_dialogue));
    }
    public void displayDialogueUserInterface01(dialogueScript _dialogue)
    {
        dialogueUserInterface.SetActive(true);

        GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>().isAbleToMove = false;

        StartCoroutine(referenceToDialogueManagement.stepThroughDialogue(_dialogue));
    }

    public void loadGame() { SceneManager.LoadScene(1); }

    public void mainMenu () { SceneManager.LoadScene(0); }


    public void resumeGame() 
    {
        pauseMenuUI.SetActive(false);

        GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>().isAbleToMove = true;
    }

    public void updateItemUserInterface(bool _value, string _case) { switch (_case) { case "Collectable": transform.GetChild(4).gameObject.SetActive(_value); break; } }

    public void updateDexUI(int _index)
    {
        // Item Description
        botanicalDex.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);

        // Item Image
        botanicalDex.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);

        // Item Name
        botanicalDex.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(true);
        
        if(_index >= GameObject.FindGameObjectWithTag("Player").GetComponent<botanicalDexJournal>().dexEntries.Count)
        {
            botanicalDex.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";

            botanicalDex.transform.GetChild(0).transform.GetChild(1).GetComponent<Image>().sprite = null;
            botanicalDex.transform.GetChild(0).transform.GetChild(1).GetComponent<Image>().enabled = false;

            botanicalDex.transform.GetChild(0).transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "???";
        }
        else
        {
            if (!GameObject.FindGameObjectWithTag("Player").GetComponent<botanicalDexJournal>().dexEntries[_index].entryCollected)
            {
                botanicalDex.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";

                botanicalDex.transform.GetChild(0).transform.GetChild(1).GetComponent<Image>().sprite = null;
                botanicalDex.transform.GetChild(0).transform.GetChild(1).GetComponent<Image>().enabled = false;

                botanicalDex.transform.GetChild(0).transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "???";
            }
            else
            {
                botanicalDex.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = GameObject.FindGameObjectWithTag("Player").GetComponent<botanicalDexJournal>().dexEntries[_index].entryDescription;

                botanicalDex.transform.GetChild(0).transform.GetChild(1).GetComponent<Image>().enabled = true;
                botanicalDex.transform.GetChild(0).transform.GetChild(1).GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("Player").GetComponent<botanicalDexJournal>().dexEntries[_index].entrySprite;

                botanicalDex.transform.GetChild(0).transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = GameObject.FindGameObjectWithTag("Player").GetComponent<botanicalDexJournal>().dexEntries[_index].entryName;
            }
        }
    }

    public void updateJournalUI(int _index)
    {
        // Description
        journal.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);

        if (_index >= GameObject.FindGameObjectWithTag("Player").GetComponent<botanicalDexJournal>().journalEntries.Count) { journal.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "???"; }
        else
        {
            if (!GameObject.FindGameObjectWithTag("Player").GetComponent<botanicalDexJournal>().journalEntries[_index].entryCollected) { journal.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "???"; }
            else { journal.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = GameObject.FindGameObjectWithTag("Player").GetComponent<botanicalDexJournal>().journalEntries[_index].entryDescription; }
        }
    }
}
