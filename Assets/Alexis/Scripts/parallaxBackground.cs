using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallaxBackground : MonoBehaviour
{
    #region Private
    private float length;
    private float startingPosition;
    #endregion

    #region Public
    public float parallaxEffect;

    public GameObject cam;
    #endregion

    void Start()
    {
        length = GetComponent<SpriteRenderer>().bounds.size.x;

        startingPosition = transform.position.x;
    }

    private void FixedUpdate()
    {
        float distance = cam.transform.position.x * parallaxEffect;
        float temp = cam.transform.position.x * (1 - parallaxEffect);

        transform.position = new Vector3(startingPosition + distance, transform.position.y, transform.position.z);

        if (temp > startingPosition + length) { startingPosition += length; }
        else if (temp < startingPosition - length) { startingPosition -= length; }
    }
}
