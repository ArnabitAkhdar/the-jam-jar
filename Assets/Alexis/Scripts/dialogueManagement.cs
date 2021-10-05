using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class dialogueManagement : MonoBehaviour
{
    #region Private
    private dialogueResponseHandler referenceToDialogueResponseHandler;

    private dialogueScript referenceToSelectedDialogueScript;

    private userInterfaceManagement referenceToUserInterfaceManagement;
    #endregion

    #region Public
    public float charBufferSpeed = 0f;
    #endregion

    void Start()
    {
        charBufferSpeed = 50f;

        referenceToDialogueResponseHandler = GetComponent<dialogueResponseHandler>();

        referenceToSelectedDialogueScript = GetComponent<dialogueScript>();

        referenceToUserInterfaceManagement = GameObject.FindGameObjectWithTag("UserInterface").GetComponent<userInterfaceManagement>();
    }

    void Update()
    {
        
    }

    private IEnumerator typeText(string _textToType, TMP_Text _textLabel)
    {
        _textLabel.text = string.Empty;

        float t = 0f;
        int charIndex = 0;

        while (charIndex < _textToType.Length)
        {
            t += Time.deltaTime * charBufferSpeed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, _textToType.Length);

            if (Input.GetKeyDown(KeyCode.Space)) { break; }

            _textLabel.text = _textToType.Substring(0, charIndex);

            yield return null;
        }

        _textLabel.text = _textToType;
    }

    private void closeDialogueScript(dialogueScript _dialogue)
    {
        referenceToUserInterfaceManagement.dialogueUserInterface.SetActive(false);

        referenceToUserInterfaceManagement.dialogueText.text = string.Empty;

        GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>().isAbleToMove = true;
    }

    public Coroutine run(string _textToType, TMP_Text _textLabel) { return StartCoroutine(typeText(_textToType, _textLabel)); }

    public IEnumerator stepThroughDialogue(dialogueScript _dialogue)
    {
        referenceToUserInterfaceManagement = GameObject.FindGameObjectWithTag("UserInterface").GetComponent<userInterfaceManagement>();

        for (int indexCounter = 0; indexCounter < _dialogue.dialogue.Length; indexCounter++)
        {
            string dialogue = _dialogue.dialogue[indexCounter];

            yield return run(dialogue, referenceToUserInterfaceManagement.dialogueText);

            if (indexCounter == _dialogue.dialogue.Length - 1 /*&& _dialogue.hasResponses*/) { break; }

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        }
        
        if (!_dialogue.hasResponses) { yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return)); closeDialogueScript(_dialogue); }
        else { referenceToDialogueResponseHandler.showResponses(_dialogue.responses); }

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
    }
}