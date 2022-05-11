using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Rigidbody rb;
    private Vector3 playerVelocity;
    static public bool inDash = false;
    private float dashTime = 0.3f;
    private float currTime = 0.0f;
    public float gravityValue = -9.8f;
    private float playerSpeed = 4.0f;
    static public int lives = 3;
    private int invincibilityFrames = 120;
    private int invincibilityCount = 0;
    private bool isHit = false;

    public TextMeshProUGUI livesText;
    public static bool enemyHit;
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
            //Debug.Log("Dashing!");
            // dash for dashTime seconds
            currTime += Time.deltaTime;
            if (currTime < dashTime)
            {
                // this value determines how fast the player dashes
                controller.Move(move / 10);
            }
            // if we have been dashing for dashTime seconds then we are no longer dashing
            else
            {
                //Debug.Log("Done dashing.");
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

        if (isHit)
        {
            if (invincibilityCount < invincibilityFrames) 
            {
                invincibilityCount += 1;
            }
            else 
            {
                invincibilityCount = 0;
                isHit = false;
            }
        }

        // shockwave power up attack
        if (Input.GetKeyDown("p") && (shockwave == true))
        {
            Debug.Log("Shockwave used!");
            // force for pushing back
            float force = 750.0f;

            // detect enemies in a given range of the player
            float range = 5.0f;
            for (int i = 0; i < selectables.Count; i++)
            {
                // check for null
                if (selectables[i] == null)
                {
                    selectables.Remove(selectables[i]);
                }
                else
                {
                    var distance = Vector3.Distance(selectables[i].transform.position, transform.position);
                    if (distance <= range)
                    {
                        // Calculate Angle Between the collision point and the player
                        Vector3 dir = selectables[i].transform.position - transform.position;

                        // add force in the direction of dir and multiply it by force.
                        selectables[i].rb.AddForce(dir * force);
                    }
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
            //Debug.Log("Hit enemy!");
            if (lives > 0 && !isHit)
            {
                lives -= 1;
                SetLivesText();
            }
            isHit = true;
        }
    }

}
