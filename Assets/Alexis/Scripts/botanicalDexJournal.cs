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
    public int ediblesPickedUp = 0, flowersPickedUp = 0, herbsPickedUp = 0;

    public List<Button> dexEntryButtons = new List<Button>();
    public List<Button> journalEntryButtons = new List<Button>();

    public List<dexEntry> dexEntries = new List<dexEntry>();
    public List<journalEntry> journalEntries = new List<journalEntry>();

    public void updateDex(int _dexEntryIndex)
    {
        int indexCounter = 0;

        foreach (dexEntry _dexEntry in dexEntries)
        {
            if (_dexEntryIndex == _dexEntry.dexNumber /*&& !_journalEntry.entryCollected*/)
            {
                //_journalEntry.entryCollected = true;

                if (_dexEntryIndex <= 9) { dexEntryButtons[indexCounter].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "00" + _dexEntry.dexNumber + " - " + _dexEntry.entryName; }
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

                if (_journalEntryIndex <= 9) { journalEntryButtons[indexCounter].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "00" + _journalEntry.journalEntryNumber + " - " + _journalEntry.entryName; }
            }

            indexCounter += 1;
        }
    }
}
