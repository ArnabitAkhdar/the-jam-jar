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
    
    public int entryNumber;

    public Sprite entrySprite;

    public string entryDescription, entryName;
    #endregion
}

public class botanicalDex : MonoBehaviour
{
    public List<Button> dexEntryButtons = new List<Button>();

    public List<dexEntry> dexEntries = new List<dexEntry>();

    void Start() { }

    public void addToDex(item _item)
    {
        int indexCounter = 0;

        foreach(dexEntry _dexEntry in dexEntries)
        {
            if(_item.itemName == _dexEntry.entryName && !_dexEntry.entryCollected)
            {
                _dexEntry.entryCollected = true;

                if (_dexEntry.entryNumber <= 9) { dexEntryButtons[indexCounter].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "00" + _dexEntry.entryNumber + " - " + _item.itemName; }
            }

            indexCounter += 1;
        }
    }
}
