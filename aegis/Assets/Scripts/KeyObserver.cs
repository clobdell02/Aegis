using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyObserver : MonoBehaviour
{
    static float keyCount;
    public float keysNeeded = 3.0f;

    // Update is called once per frame
    void Update()
    {
        if(KeyPickUp.keyCount >= 3.0f)
        {
          // Open Door
        }
    }
}
