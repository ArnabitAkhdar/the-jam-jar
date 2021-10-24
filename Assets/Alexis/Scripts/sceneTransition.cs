using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class sceneTransition : MonoBehaviour
{
    #region Private
    private bool beginFadeIn = false, beginFadeOut = false;
    private bool hasAlphaChannelBeenSetUp = false;

    private float alphaChannel = 255f;

    private int selectedSceneIndex = -1;
    private int waitForSeconds = 3;
    #endregion

    #region Public
    public bool hasTransitionFinished = false;

    // If no objective, enable bool to let player go through
    public bool overrideAndLetPlayerTriggerTransition = false;

    public Image panel;

    public int transitionToScene = -1;
    #endregion

    private void Start() 
    {
        if (SceneManager.GetActiveScene().name != "MainMenu") { beginFadeOut = true; }
        else { PlayerPrefs.DeleteAll(); }

        panel.gameObject.SetActive(false); 
    }

    private void FixedUpdate() 
    {
        if (beginFadeIn) { fadeIn(selectedSceneIndex); }

        if (beginFadeOut && SceneManager.GetActiveScene().name != "MainMenu") { fadeOut(); } 
    }

    private void OnTriggerEnter2D(Collider2D collision) { if (collision.CompareTag("Player") && overrideAndLetPlayerTriggerTransition) { transitionScene(transitionToScene); } }

    public void fadeIn(int _sceneBuildIndex)
    {
        if(!hasTransitionFinished)
        {
            if(!hasAlphaChannelBeenSetUp)
            {
                hasAlphaChannelBeenSetUp = true;

                if(GameObject.FindGameObjectWithTag("Player") != null) { GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>().isAbleToMove = false; }

                panel.gameObject.SetActive(true);
                panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, alphaChannel);
                panel.canvasRenderer.SetAlpha(0f);
                panel.CrossFadeAlpha(1f, (float)waitForSeconds - 1f, false);
            }

            // FIGURE OUT WHY THE SCENE DOES NOT FADE INTO BLACK WHEN YOU REACH THE END.

            if (panel.canvasRenderer.GetAlpha() > 0.90f) 
            {
                beginFadeIn = false;

                hasAlphaChannelBeenSetUp = false;

                hasTransitionFinished = true;

                SceneManager.LoadScene(_sceneBuildIndex);
            }
        }
    }
    public void fadeInButton(int _sceneBuildIndex)
    {
        beginFadeIn = true;

        hasTransitionFinished = false;

        selectedSceneIndex = _sceneBuildIndex;
    }

    public void fadeOut()
    {
        if (!hasTransitionFinished)
        {
            if (!hasAlphaChannelBeenSetUp)
            {
                hasAlphaChannelBeenSetUp = true;

                if (GameObject.FindGameObjectWithTag("Player") != null) { GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>().isAbleToMove = false; }

                panel.gameObject.SetActive(true);
                panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, alphaChannel);
                panel.canvasRenderer.SetAlpha(1f);
                panel.CrossFadeAlpha(0f, (float)waitForSeconds, false);
            }

            if (panel.canvasRenderer.GetAlpha() < .10f * (float)waitForSeconds) 
            {
                hasTransitionFinished = true;

                if (GameObject.FindGameObjectWithTag("Player") != null) { if (!GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>().isAbleToMove) { GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>().isAbleToMove = true; } }

                panel.gameObject.SetActive(false);
            }
        }
    }

    public void transitionScene(int _sceneBuildIndex) 
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<botanicalDexJournal>().saveDexJournalInformation();

        SceneManager.LoadScene(_sceneBuildIndex); 
    }
}