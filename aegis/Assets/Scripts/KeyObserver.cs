using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyObserver : MonoBehaviour
{
    static float keyCount;
    public float keysNeeded = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if(KeyPickUp.keyCount >= keysNeeded)
        {
          Destroy(this.gameObject);
        }
    }
}
