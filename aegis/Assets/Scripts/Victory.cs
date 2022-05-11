using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Victory : MonoBehaviour
{
  void OnTriggerEnter(Collider other)
  {
    SceneManager.LoadScene("Victory");
  }
}
