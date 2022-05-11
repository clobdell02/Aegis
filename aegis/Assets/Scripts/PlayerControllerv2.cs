using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Movement from Brackeys: https://www.youtube.com/watch?v=4HpC--2iowE
public class PlayerControllerv2 : MonoBehaviour
{

  public CharacterController controller;
  public Transform cam;
  public float speed = 6f;
  public float smoothTurn = 0.1f;
  float turnSmoothVelocity;
  public Vector3 moveDir;
  public TextMeshProUGUI livesText;
  [SerializeField] private List<SelectableEnemies> selectables;
  private bool shockwave = false;
  public static bool enemyHit;
  public static int lives = 3;
  private Animator animator;

    public void SetLivesText()
    {
      // sets the livesText
      livesText.text = $"Lives: {lives}\nPower Up: {(shockwave ? "Shockwave" : "None")}";
    }

    void Start()
    {
      livesText = GameObject.Find("UIPrefab").GetComponent<TextMeshProUGUI>();
      animator = GetComponent<Animator>();
      SetLivesText();
    }

    // Update is called once per frame
    void Update()
    {
      float horizontal = Input.GetAxisRaw("Horizontal");
      float vertical = Input.GetAxisRaw("Vertical");

      bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
      bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
      bool isWalking = (hasHorizontalInput || hasVerticalInput) && !ThirdPersonDash.inDash;

      animator.SetBool("IsWalking", isWalking);

      Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

      if(direction.magnitude >= 0.1f)
      {
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, smoothTurn);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        controller.Move(moveDir.normalized * speed * Time.deltaTime);
      }

      // shockwave power up attack
      if (Input.GetMouseButtonDown(1) && (shockwave == true))
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

                      // get the opposite (-Vector3) and normalize it
                      //dir = -dir.normalized;
                      //Debug.Log(dir);

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
            SetLivesText();
        }
        if (other.gameObject.CompareTag("Health"))
        {
            other.gameObject.SetActive(false);
            lives += 1;
            SetLivesText();
        }

            if (other.gameObject.CompareTag("enemy") || other.gameObject.CompareTag("Projectile"))
        {
            //Debug.Log("Hit enemy!");
            if (lives > 0)
            {
                lives -= 1;
                SetLivesText();
            }
        }
    }
}
