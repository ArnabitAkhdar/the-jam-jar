using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class dialogueResponseHandler : MonoBehaviour
{
    private List<GameObject> tempResponseButtons = new List<GameObject>();

    private userInterfaceManagement referenceToUserInterfaceManagement;

    public RectTransform responseBox;
    public RectTransform responseButtonTemplate;
    public RectTransform responseContainer;

    private void Start() { referenceToUserInterfaceManagement = GameObject.FindGameObjectWithTag("UserInterface").GetComponent<userInterfaceManagement>(); }

    private void onPickedResponse(dialogueResponse _response)
    {
        responseBox.gameObject.SetActive(false);

        foreach (GameObject _button in tempResponseButtons) { Destroy(_button); }

        tempResponseButtons.Clear();

        referenceToUserInterfaceManagement.displayDialogueUserInterface(_response.dialogueScript);
    }

    public void showResponses(dialogueResponse[] _responses)
    {
        float responseBoxHeight = 0;

        foreach (dialogueResponse _response in _responses)
        {
            GameObject responseButton = Instantiate(responseButtonTemplate.gameObject, responseContainer);
            responseButton.gameObject.SetActive(true);
            responseButton.GetComponent<TMP_Text>().text = _response.responseText;
            responseButton.GetComponent<Button>().onClick.AddListener(() => onPickedResponse(_response));

            tempResponseButtons.Add(responseButton);

            responseBoxHeight += responseButtonTemplate.sizeDelta.y;
        }

        responseBox.sizeDelta = new Vector2(responseBox.sizeDelta.x, responseBox.sizeDelta.y);
        responseBox.gameObject.SetActive(true);
    }
}
