using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class dexEntry
{
    #region Public
    public bool entryCollected = false;
    
    public int dexNumber;

    public Sprite entrySprite;

    [SerializeField] [TextArea] public string entryDescription; 
    public string entryName;
    #endregion
}

[System.Serializable]
public class journalEntry
{
    #region Public
    public bool entryCollected = false;

    public int journalEntryNumber;

    [SerializeField] [TextArea] public string entryDescription;
    public string entryName;
    #endregion
}


public class botanicalDexJournal : MonoBehaviour
{
    private bool hasUnlockedAllDexEntries = false;

    public int ediblesPickedUp = 0, flowersPickedUp = 0, herbsPickedUp = 0;

    public List<Button> dexEntryButtons = new List<Button>();
    public List<Button> journalEntryButtons = new List<Button>();

    public List<dexEntry> dexEntries = new List<dexEntry>();
    public List<journalEntry> journalEntries = new List<journalEntry>();

    void Update()
    {
        if(!hasUnlockedAllDexEntries && Input.GetKey(KeyCode.M) && Input.GetKey(KeyCode.T))
        {
            foreach(dexEntry _dexEntry in dexEntries) 
            { 
                _dexEntry.entryCollected = true;

                updateDex(_dexEntry.dexNumber);

                GameObject.FindGameObjectWithTag("UserInterface").GetComponent<userInterfaceManagement>().updateDexUI(_dexEntry.dexNumber);
            }

            hasUnlockedAllDexEntries = true;
        }
    }

    public void loadDexJournalInformation()
    {
        for (int indexCounter = 0; indexCounter < dexEntries.Count; indexCounter++) 
        {
            if(PlayerPrefs.GetInt("dexEntries" + indexCounter) != 0) 
            { 
                dexEntries[indexCounter].entryCollected = true;

                GameObject.FindGameObjectWithTag("UserInterface").GetComponent<userInterfaceManagement>().updateDexUI(dexEntries[indexCounter].dexNumber);

                updateDex(dexEntries[indexCounter].dexNumber);
            }
        }
    }

    public void saveDexJournalInformation()
    {
        for(int indexCounter = 0; indexCounter < dexEntries.Count; indexCounter++)
        {
            if (!dexEntries[indexCounter].entryCollected) { PlayerPrefs.SetInt("dexEntries" + indexCounter, 0); }
            else { PlayerPrefs.SetInt("dexEntries" + indexCounter, 1); }
        }
    }

    public void updateDex(int _dexEntryIndex)
    {
        int indexCounter = 0;

        foreach (dexEntry _dexEntry in dexEntries)
        {
            if (_dexEntryIndex == _dexEntry.dexNumber /*&& !_journalEntry.entryCollected*/)
            {
                //_journalEntry.entryCollected = true;

                dexEntryButtons[indexCounter].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _dexEntry.dexNumber + " - " + _dexEntry.entryName;
            }

            indexCounter += 1;
        }
    }

    public void updateJournal(int _journalEntryIndex)
    {
        int indexCounter = 0;

        foreach (journalEntry _journalEntry in journalEntries)
        {
            if (_journalEntryIndex == _journalEntry.journalEntryNumber /*&& !_journalEntry.entryCollected*/)
            {
                //_journalEntry.entryCollected = true;

                journalEntryButtons[indexCounter].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _journalEntry.journalEntryNumber + " - " + _journalEntry.entryName;
            }

            indexCounter += 1;
        }
    }
}
