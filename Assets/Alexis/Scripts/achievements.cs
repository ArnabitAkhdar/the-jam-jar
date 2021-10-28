using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class achievement
{
    public bool hasAchievementBeenAchieved = false;

    public string name;

    [SerializeField] [TextArea] public string description;
}

public class achievements : MonoBehaviour
{
    #region Private
    private achievement previousAchievement;

    private float achievementDisplayTimer = 5f;

    private List<achievement> achievementListBacklog = new List<achievement>();

    private userInterfaceManagement referenceToUserInterfaceManagement;
    #endregion

    #region Public
    public GameObject achievementUI;

    public List<achievement> achievementList = new List<achievement>();
    public List<achievement> miscellaneousAchievements = new List<achievement>();
    #endregion

    void Start() { achievementUI.SetActive(false); referenceToUserInterfaceManagement = GameObject.FindGameObjectWithTag("UserInterface").GetComponent<userInterfaceManagement>(); }

    void Update()
    {
        displayAchievement(achievementListBacklog);

        // Miscellaneous Achievements
        // Bon Appétit 
        if (PlayerPrefs.GetInt("Bon Appétit") != 1 && PlayerPrefs.GetInt("ediblesPickedUp") == 1) 
        {
            if (!miscellaneousAchievements[0].hasAchievementBeenAchieved)
            {
                miscellaneousAchievements[0].hasAchievementBeenAchieved = true;

                achievementListBacklog.Add(miscellaneousAchievements[0]);

                PlayerPrefs.SetInt("Bon Appétit", 1);
            }
        }
        // Inner Peas
        else if (PlayerPrefs.GetInt("Inner Peas") != 1 && PlayerPrefs.GetInt("ediblesPickedUp") == 3)
        {
            if (!miscellaneousAchievements[1].hasAchievementBeenAchieved)
            {
                miscellaneousAchievements[1].hasAchievementBeenAchieved = true;

                achievementListBacklog.Add(miscellaneousAchievements[1]);

                PlayerPrefs.SetInt("Inner Peas", 1);
            }
        }
        // Vitamin C-eeing the World
        else if (PlayerPrefs.GetInt("Vitamin C-eeing the World") != 1 && PlayerPrefs.GetInt("ediblesPickedUp") == 5)
        {
            if (!miscellaneousAchievements[2].hasAchievementBeenAchieved)
            {
                miscellaneousAchievements[2].hasAchievementBeenAchieved = true;

                achievementListBacklog.Add(miscellaneousAchievements[2]);

                PlayerPrefs.SetInt("Vitamin C-eeing the World", 1);
            }
        }

        // Early Bloomer
        if(PlayerPrefs.GetInt("Early Bloomer") != 1 && PlayerPrefs.GetInt("flowersPickedUp") == 1)
        {
            if (!miscellaneousAchievements[3].hasAchievementBeenAchieved)
            {
                miscellaneousAchievements[3].hasAchievementBeenAchieved = true;

                achievementListBacklog.Add(miscellaneousAchievements[3]);

                PlayerPrefs.SetInt("Early Bloomer", 1);
            }
        }
        // Rosey Cheeks
        else if (PlayerPrefs.GetInt("Rosey Cheeks") != 1 && PlayerPrefs.GetInt("flowersPickedUp") == 5)
        {
            if (!miscellaneousAchievements[4].hasAchievementBeenAchieved)
            {
                miscellaneousAchievements[4].hasAchievementBeenAchieved = true;

                achievementListBacklog.Add(miscellaneousAchievements[4]);

                PlayerPrefs.SetInt("Rosey Cheeks", 1);
            }
        }
        // Unbeleafable!
        else if (PlayerPrefs.GetInt("Unbeleafable!") != 1 && PlayerPrefs.GetInt("flowersPickedUp") == 10)
        {
            if (!miscellaneousAchievements[5].hasAchievementBeenAchieved)
            {
                miscellaneousAchievements[5].hasAchievementBeenAchieved = true;

                achievementListBacklog.Add(miscellaneousAchievements[5]);

                PlayerPrefs.SetInt("Unbeleafable!", 1);
            }
        }

        // Encourage-Mint
        if(PlayerPrefs.GetInt("Encourage-Mint") != 1 && PlayerPrefs.GetInt("herbsPickedUp") == 1)
        {
            if (!miscellaneousAchievements[6].hasAchievementBeenAchieved)
            {
                miscellaneousAchievements[6].hasAchievementBeenAchieved = true;

                achievementListBacklog.Add(miscellaneousAchievements[6]);

                PlayerPrefs.SetInt("Encourage-Mint", 1);
            }
        }
        // Not Mossing Around
        else if (PlayerPrefs.GetInt("Not Mossing Around") != 1 && PlayerPrefs.GetInt("herbsPickedUp") == 3)
        {
            if (!miscellaneousAchievements[7].hasAchievementBeenAchieved)
            {
                miscellaneousAchievements[7].hasAchievementBeenAchieved = true;

                achievementListBacklog.Add(miscellaneousAchievements[7]);

                PlayerPrefs.SetInt("Not Mossing Around", 1);
            }
        }
        // One More Thyme
        else if (PlayerPrefs.GetInt("One More Thyme") != 1 && PlayerPrefs.GetInt("herbsPickedUp") == 5)
        {
            if (!miscellaneousAchievements[8].hasAchievementBeenAchieved)
            {
                miscellaneousAchievements[8].hasAchievementBeenAchieved = true;

                achievementListBacklog.Add(miscellaneousAchievements[8]);

                PlayerPrefs.SetInt("One More Thyme", 1);
            }
        }

        if (SceneManager.GetActiveScene().name == "Level 1 - Homestead") 
        {
            // New Leaf
            if (GetComponent<dialogueManagement>().previousDialogueScript == GameObject.Find("Elder").GetComponent<interactable>().itemAcquiredDialogue) 
            {
                if(!achievementList[0].hasAchievementBeenAchieved)
                {
                    achievementList[0].hasAchievementBeenAchieved = true;

                    achievementListBacklog.Add(achievementList[0]);

                    PlayerPrefs.SetInt("Bon Appétit", 1);
                }
            }

            // Sync’d Up!
            if (GameObject.FindGameObjectWithTag("UserInterface").GetComponent<userInterfaceManagement>().isBotanicalDexAndJournalSectionOpened) 
            {
                if (!achievementList[1].hasAchievementBeenAchieved)
                {
                    achievementList[1].hasAchievementBeenAchieved = true;

                    achievementListBacklog.Add(achievementList[1]);

                    PlayerPrefs.SetInt("Bon Appétit", 1);
                }
            }
        }
    }

    public void displayAchievement(List<achievement> _achievementListBacklog)
    {
        if(_achievementListBacklog.Count != 0)
        {
            achievementDisplayTimer -= Time.deltaTime;

            if (achievementUI.GetComponent<Animator>().GetBool("isExiting")) { achievementUI.GetComponent<Animator>().SetBool("isExiting", false); }

            achievementUI.SetActive(true);

            // Title
            achievementUI.transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>().text = _achievementListBacklog[0].name;
            // Description
            achievementUI.transform.GetChild(1).GetChild(1).GetComponent<TMP_Text>().text = _achievementListBacklog[0].description;

            if (achievementDisplayTimer <= 0f)
            {
                achievementDisplayTimer = 5f;

                _achievementListBacklog.RemoveAt(0);

                if (_achievementListBacklog.Count == 0) { /*achievementUI.SetActive(false);*/ achievementUI.GetComponent<Animator>().SetBool("isExiting", true); }
            }
        }
    }
}