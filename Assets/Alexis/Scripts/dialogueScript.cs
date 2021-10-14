using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogueScript : MonoBehaviour
{
    [SerializeField] [TextArea] public string[] dialogue;

    public bool hasResponses = false;

    public dialogueResponse[] responses;

    public bool isASoloConversation = true;

    public string[] characterNames;

    void Start() { }

    void Update() { }
}
