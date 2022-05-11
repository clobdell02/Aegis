using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// From 5 Head Games: https://www.youtube.com/watch?v=vTNWUbGkZ58
public class ThirdPersonDash : MonoBehaviour
{
    PlayerControllerv2 moveScript;
    public float dashSpeed;
    public float dashTime;
    static public bool inDash = false;

    // Start is called before the first frame update
    void Start()
    {
        moveScript = GetComponent<PlayerControllerv2>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
          StartCoroutine(Dash());
        }
        inDash = false;
    }

    IEnumerator Dash()
    {

      float startTime = Time.time;

      while(Time.time < startTime + dashTime)
      {
        inDash = true;
        moveScript.controller.Move(moveScript.moveDir * dashSpeed * Time.deltaTime);
        yield return null;
      }

      inDash = false;
    }
}
