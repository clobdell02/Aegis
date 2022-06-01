using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableEnemies : MonoBehaviour
{
    public Rigidbody rb;
    [HideInInspector] public float LookPercentage;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
}
