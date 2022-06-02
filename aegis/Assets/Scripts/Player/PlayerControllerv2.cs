using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

// Movement from Brackeys: https://www.youtube.com/watch?v=4HpC--2iowE
public class PlayerControllerv2 : MonoBehaviour
{

  public CharacterController controller;
  public Transform cam;
  public float speed = 6f;
  public float smoothTurn = 0.1f;
  public static float invincTime = 1.5f; 
  public static float currInvincTime = 0.0f; 
  public static float flashTime = 0.0f; 
  float turnSmoothVelocity;
  public Vector3 moveDir;
  public TextMeshProUGUI livesText;
  public List<Renderer> renderers;
  [SerializeField] private List<SelectableEnemies> selectables;
  private bool shockwave = false;
  public static bool enemyHit;
  public static bool invincible = false;
  public static int lives = 3;
  private Animator animator;
  static float keyCount;

    public void SetLivesText()
    {
      // sets the livesText
      livesText.text = $"Lives: {lives}\nPower Up: {(shockwave ? "Shockwave" : "None")}\n";
    }

    void Start()
    {
      lives = 3;
      livesText = GameObject.Find("UIPrefab").GetComponent<TextMeshProUGUI>();
      animator = GetComponent<Animator>();
      var renders = GetComponentsInChildren<Renderer>();
      // renders[1] is the body, renders[2] is the head
      renderers.Add(renders[1]);
      renderers.Add(renders[2]);
      SetLivesText();
    }

    // Update is called once per frame
    void Update()
    {
      // Locks cursor once the screen is clicked.
      Cursor.lockState = CursorLockMode.Locked;

      float horizontal = Input.GetAxisRaw("Horizontal");
      float vertical = Input.GetAxisRaw("Vertical");

      bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
      bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
      bool isWalking = (hasHorizontalInput || hasVerticalInput) && !ThirdPersonDash.inDash;

      animator.SetBool("IsWalking", isWalking);

      Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

      float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
      float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, smoothTurn);
      transform.rotation = Quaternion.Euler(0f, angle, 0f);

      if(direction.magnitude >= 0.1f)
      {
        /*
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, smoothTurn);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
        */

        moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        Vector3 mvmt = moveDir.normalized * speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Debug.Log("Left shift pressed!");
            mvmt *= 1.8f;
        }
        controller.Move(mvmt);
      }

      // adds 1.5 seconds of i-frames when hit
      if (invincible)
      {
          currInvincTime += Time.deltaTime;
          if (currInvincTime >= invincTime)
          {
              invincible = false;
              currInvincTime = 0;
          }
      }

      // shockwave power up attack
      if (Input.GetMouseButtonDown(1) && (shockwave == true))
      {
          Debug.Log("Shockwave used!");
          shockwave = false;
          SetLivesText();
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

      // Return to menu
      if(Input.GetKey(KeyCode.Backslash))
      {
        SceneManager.LoadScene("Title");
      }
    }

    // function ran when you take damage
    private void TakeDamage()
    {
        Debug.Log("Hit by enemy!");
        if(lives > 0 && !invincible)
        {
            flashTime = 0;
            // flash Shieldon a color to signify damage (coroutine since flashing is independent of game statee)
            StartCoroutine(DamageFlash());
            invincible = true;
            lives -= 1;
            SetLivesText();
        }
    }

    IEnumerator DamageFlash()
    {
        // t is the normalized value of time based on time range, since we want it to flash red then white then red
        float t;
        while (flashTime < invincTime)
        {
            // add deltaTime to flashTime
            flashTime += Time.deltaTime;
            // flash red to white
            if (flashTime < 0.5f)
            {
                // normalize flashTime depending on the range
                t = flashTime * 2;
                // lambda expression to lerp for each renderer (body and head )
                renderers.ForEach(r => r.material.color = Color.Lerp(Color.red, Color.white, t));
            }
            // flash white to red
            else if (flashTime >= 0.5 && flashTime <= 1)
            {
                  t = (flashTime - 0.5f) * 2; 
                  renderers.ForEach(r => Color.Lerp(Color.white, Color.red, t));
            }
            // flash red to white
            else
            {
                t = (flashTime - 1f) * 2; 
                renderers.ForEach(r => r.material.color = Color.Lerp(Color.red, Color.white, t));
            }
            yield return null;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Shockwave"))
        {
            other.gameObject.SetActive(false);
            shockwave = true;
            SetLivesText();
        }

        if (other.gameObject.CompareTag("Health "))
        {
            other.gameObject.SetActive(false);
            lives += 1;
            SetLivesText();
        }

        if(other.gameObject.CompareTag("Key"))
        {
            SetLivesText();
        }

        // Projectiles use trigger colliders so they have to be tracked in
        // OnTriggerEnter
        if(other.gameObject.CompareTag("Projectile"))
        {
            TakeDamage();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
      if (other.gameObject.CompareTag("enemy"))
      {
            TakeDamage();
      }
    }
}
