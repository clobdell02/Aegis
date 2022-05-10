using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;



public class Death : MonoBehaviour
{
    private int lives;

    void Start()
    {
        lives = PlayerController.lives;
    }

    // Update is called once per frame
    void Update()
    {
        int currentLives = PlayerController.lives;
        if (currentLives != lives)
        {
            Debug.Log("Lives changed!");
            lives = currentLives;
        }
        if (lives <= 0)
        {
            Debug.Log("Returning to title, game over!");
            ReturnToTitle();
        }
    }

    void ReturnToTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
