using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Rigidbody rb;
    private Vector3 playerVelocity;
    private bool inDash = false;
    private float dashTime = 0.3f;
    private float currTime = 0.0f;
    public float gravityValue = -9.8f;
    public float playerSpeed = 6.0f;

    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    private void Update()
    {
      Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

      // if the space bar is pressed then we're going to dash, no airdashes and no infinite dashing (may want to incorporate a small cooldown?)
      if (Input.GetKeyDown("space") && controller.isGrounded && !inDash)
      {
        inDash = true;
      }

      // if we are currently dashing, hijack the update function to dash
      if (inDash)
      {
        Debug.Log("Dashing!");
        // dash for dashTime seconds
        currTime += Time.deltaTime;
        if (currTime < dashTime)
        {
          // this value determines how fast the player dashes
          controller.Move(move / 64);
        }
        // if we have been dashing for dashTime seconds then we are no longer dashing
        else
        {
          Debug.Log("Done dashing.");
          inDash = false;
          currTime = 0;
        }
      }
      
      if (!inDash)
      {      
        controller.Move(move * Time.deltaTime * playerSpeed);

        if(move != Vector3.zero)
        {
          gameObject.transform.forward = move;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
      }
    }

    /*
    private void FixedUpdate()
    {

    }
    */
}
