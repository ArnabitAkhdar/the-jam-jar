using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    #region Private
    private BoxCollider2D boxCollider2D;

    private userInterfaceManagement referenceToUserInterfaceManagement; 
    #endregion

    #region Public
    public bool canBePickedUp = false;

    public string description = "", itemName = "";
    #endregion

    void Start()
    {
        boxCollider2D = gameObject.AddComponent<BoxCollider2D>();
        boxCollider2D.isTrigger = true;
        boxCollider2D.size *= 2;

        referenceToUserInterfaceManagement = GameObject.FindGameObjectWithTag("UserInterface").GetComponent<userInterfaceManagement>();
    }

    private void OnTriggerEnter2D(Collider2D collision) { if (collision.CompareTag("Player")) { referenceToUserInterfaceManagement.updateItemUserInterface(true, "Collectable"); } }

    private void OnTriggerExit2D(Collider2D collision) { if (collision.CompareTag("Player")) { referenceToUserInterfaceManagement.updateItemUserInterface(false, "Collectable"); } }

    private void OnTriggerStay2D(Collider2D collision) { if (collision.CompareTag("Player")) { if (Input.GetKeyDown(KeyCode.Return)) { if (canBePickedUp) { pickupItem(); } } } }

    private void pickupItem() 
    {
        botanicalDex bD = GameObject.FindGameObjectWithTag("Player").GetComponent<botanicalDex>();
        bD.addToDex(this);

        referenceToUserInterfaceManagement.updateItemUserInterface(false, "Collectable");

        gameObject.SetActive(false);
    }

    
}
