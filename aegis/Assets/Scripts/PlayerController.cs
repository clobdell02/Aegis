using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    public float gravityValue = -9.8f;
    public float playerSpeed = 6.0f;

    void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
    }

    private void Update()
    {
      Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

      controller.Move(move * Time.deltaTime * playerSpeed);

      if(move != Vector3.zero)
      {
        gameObject.transform.forward = move;
      }

      playerVelocity.y += gravityValue * Time.deltaTime;
      controller.Move(playerVelocity * Time.deltaTime);
    }

    /*
    private void FixedUpdate()
    {

    }
    */
}
