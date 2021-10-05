using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogueScript : MonoBehaviour
{
    [SerializeField] [TextArea] public string[] dialogue;

    public bool hasResponses = false;

    public dialogueResponse[] responses;

    void Start() { }

    void Update() { }
}
