using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Victory : MonoBehaviour
{
  void OnTriggerEnter(Collider other)
  {
    
    Scene scene = SceneManager.GetActiveScene();

    switch(scene.name)
    {
      case "level1 Desert":
        SceneManager.LoadScene("level2 Forest");
        break;
      case "level2 Forest":
        SceneManager.LoadScene("level3 Cliffs");
        break;
      case "level3 Cliffs":
        SceneManager.LoadScene("Victory");
        break;
      default:
        SceneManager.LoadScene("Title");
        break;
    }
    
  }
}
