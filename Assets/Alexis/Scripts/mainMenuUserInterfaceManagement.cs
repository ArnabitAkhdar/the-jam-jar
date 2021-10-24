using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class mainMenuUserInterfaceManagement : MonoBehaviour
{
    public AudioMixer soundMixer;

    // Button Functions
    public void backButton(GameObject _gameObject) { _gameObject.SetActive(false); }

    public void displayUserInterface(GameObject _gameObject) { _gameObject.SetActive(true); }

    public void exitApplication() { Application.Quit(); }

    // public void loadGame() { }

    public void newGame() { SceneManager.LoadScene(1); }

    public void setSoundLevel(float _value) { soundMixer.SetFloat("soundVolume", Mathf.Log10(_value) * 20); }
}
