using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class water : MonoBehaviour
{
    private int lives;

    // Start is called before the first frame update
    void Start()
    {
        lives = PlayerControllerv2.lives;
    }

    // Update is called once per frame
    void Update()
    { }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            lives = 0;
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
}
