using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowObserver : MonoBehaviour
{
    static float keyCount;
    public float keysNeeded = 0.0f;
    private GameObject arrow;
    // Start is called before the first frame update
    void Start()
    {
        arrow = GameObject.FindGameObjectWithTag("arrow");
        arrow.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(KeyPickUp.keyCount >= keysNeeded)
        {
          arrow.gameObject.SetActive(true);
        }
    }
}
