using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCreator : MonoBehaviour
{
    // This script is designed to be attached to something in the scene that acts as
    // an observer. I simply create an empty object and attach the script to it
    
    public int enemiesNeeded; // Number of gold enemies the player needs to defeat to spawn object (set in inspector)
    static public int enemiesDefeated; // Number of enemies the player has defeated
    private GameObject victoryItem; // Actual victoryItem to be spawned

    void Start()
    {
      victoryItem = GameObject.FindGameObjectWithTag("Key");
      // Initialize set the victory item to false
      victoryItem.SetActive(false);

      // Instantiate the number of enemies defeated to 0
      enemiesDefeated = 0;
    }

    void Update()
    {
      if((enemiesDefeated >= enemiesNeeded))
      {
        Debug.Log("Victory Item Spawned");
        victoryItem.gameObject.SetActive(true);
      }
    }
}
