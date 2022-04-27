using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Rigidbody rb;
    private Vector3 playerVelocity;
    private bool inDash = false;
    private float dashTime = 0.3f;
    private float currTime = 0.0f;
    public float gravityValue = -9.8f;
    private float playerSpeed = 4.0f;
    private int lives = 3;
    public TextMeshProUGUI livesText;

    // powerup variables:
    [SerializeField] private List<SelectableEnemies> selectables;
    private bool shockwave = false;

    void SetLivesText()
    {
        livesText.text = $"Lives: {lives}";
    }
    
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        SetLivesText();
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
                controller.Move(move / 16);
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

        // shockwave power up attack
        if (Input.GetKeyDown("p") && (shockwave == true))
        {
            Debug.Log("P pushed");
            // force for pushing back
            float force = 750.0f;

            // detect enemies in a given range of the player
            float range = 2.5f;
            for (int i = 0; i < selectables.Count; i++)
            {
                var distance = Vector3.Distance(selectables[i].transform.position, transform.position);
                Debug.Log(distance);
                if (distance <= range)
                {
                    // Calculate Angle Between the collision point and the player
                    Vector3 dir = selectables[i].transform.position - transform.position;
                    Debug.Log(dir);

                    // get the opposite (-Vector3) and normalize it
                    //dir = -dir.normalized;
                    //Debug.Log(dir);

                    // add force in the direction of dir and multiply it by force.
                    selectables[i].rb.AddForce(dir * force);
                    Debug.Log(dir * force);
                }
            }
            shockwave = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Shockwave"))
        {
            other.gameObject.SetActive(false);
            shockwave = true;
        }

        if (other.gameObject.CompareTag("enemy"))
        {
            Debug.Log("Hit enemy!");
            if (lives > 0)
            {
                lives -= 1;
                SetLivesText();
            }
        }
    }

}
