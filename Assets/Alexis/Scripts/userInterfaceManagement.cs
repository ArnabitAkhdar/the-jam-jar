using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class userInterfaceManagement : MonoBehaviour
{
    #region Public
    public bool isBotanicalDexTabOpened = false, isJournalTabOpened = false;
    public bool isBotanicalDexAndJournalSectionOpened = false;

    public dialogueScript selectedDialogueScript;

    public dialogueManagement referenceToDialogueManagement;

    public GameObject botanicalDexInformationalUI, botanicalDexScrollArea;
    public GameObject dialogueUserInterface;

    public TMP_Text dialogueText;
    #endregion

    void Start() 
    {
        botanicalDexInformationalUI.transform.GetChild(1).gameObject.SetActive(false);
        botanicalDexInformationalUI.transform.GetChild(2).gameObject.SetActive(false);
        botanicalDexInformationalUI.transform.GetChild(3).gameObject.SetActive(false);

        botanicalDexScrollArea.SetActive(false);

        // displayDialogueUserInterface(selectedDialogueScript);
    }

    public void displayTab(int _selectedTab)
    {
        switch(_selectedTab)
        {
            // Botanical Dex
            case 0:
                if (isJournalTabOpened) 
                {
                    isJournalTabOpened = false;

                    // Disable the Journal ScrollArea here when created
                }

                isBotanicalDexTabOpened = true;

                botanicalDexScrollArea.SetActive(true);
                break;
            // Journal
            case 1:
                if (isBotanicalDexTabOpened)
                {
                    isBotanicalDexTabOpened = false;

                    botanicalDexScrollArea.SetActive(false);
                }

                isJournalTabOpened = true;

                // Enable the Journal ScrollArea here when created
                break;
        }
    }

    public void displayDialogueUserInterface(dialogueScript _dialogue)
    {
        dialogueUserInterface.SetActive(true);

        GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>().isAbleToMove = false;

        StartCoroutine(referenceToDialogueManagement.stepThroughDialogue(_dialogue));        
    }

    public void updateItemUserInterface(bool _value, string _case) { switch (_case) { case "Collectable": transform.GetChild(4).gameObject.SetActive(_value); break; } }

    public void updateBotanicalDexInformationalUI(int _index)
    {
        botanicalDex referenceToBotanicalDex = GameObject.FindGameObjectWithTag("Player").GetComponent<botanicalDex>();

        // Item Description
        botanicalDexInformationalUI.transform.GetChild(1).gameObject.SetActive(true);

        // Item Image
        botanicalDexInformationalUI.transform.GetChild(2).gameObject.SetActive(true);

        // Item Name
        botanicalDexInformationalUI.transform.GetChild(3).gameObject.SetActive(true);

        if (!referenceToBotanicalDex.dexEntries[_index].entryCollected)
        {            
            botanicalDexInformationalUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "?";
            
            botanicalDexInformationalUI.transform.GetChild(2).GetComponent<Image>().sprite = null;
            
            botanicalDexInformationalUI.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "???";
        }
        else
        {
            
            botanicalDexInformationalUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = referenceToBotanicalDex.dexEntries[_index].entryDescription;
            
            botanicalDexInformationalUI.transform.GetChild(2).GetComponent<Image>().sprite = referenceToBotanicalDex.dexEntries[_index].entrySprite;

            botanicalDexInformationalUI.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = referenceToBotanicalDex.dexEntries[_index].entryName;
        }
    }
}
