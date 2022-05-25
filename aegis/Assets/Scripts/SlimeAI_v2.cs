using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAI_v2 : MonoBehaviour
{
  UnityEngine.AI.NavMeshAgent agent;
  GameObject player;
  private float obstacleRange = 3.0f;
  private float changeDirectionTimer = 1.2f;
  private float timer = 0.0f;

  // Values to compare against player distance to determine the
  // state the agent should be in
  public float ChaseDist = 15.0f; // Distance at which agent chases Player
  public float AttackDist = 0.5f; // Distance at which agent attacks Player
  public float WanderMax = 45.0f; // Max Dist from player where the enemy will still wander

  // States
  private bool _rest;
  private bool _chase;
  private bool _attack;
  private bool _wander;

  public Transform target;
    void Start()
    {
      agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
      player = GameObject.FindGameObjectWithTag("Player");

      // Set States
      _rest = true;
      _chase = false;
      _attack = false;
      _wander = false;
    }

    void Update()
    {
      var distToPlayer = Vector3.Distance(player.transform.position, this.transform.position);
      // Check if the enemy is within range to see the player
      if((distToPlayer <= ChaseDist) & (distToPlayer > AttackDist))
      {
        // If the enemy is close enough to see the player we want it
        // enter the chase state
        _rest = false;
        _chase = true;
        _attack = false;
        //Debug.Log("Enemy in Chase State");
      }
      else if(distToPlayer <= AttackDist)
      {
        // If the enemy is in range to attack the player we want it
        // to enter the attack state while also remaining in the chase
        // state.
        _rest = false;
        _chase = true;
        _attack = true;
        //Debug.Log("Enemy in Attack State");
      }
      else if((distToPlayer > ChaseDist) & (distToPlayer <= WanderMax))
      {
        // If the enemy is outside the chase distance range but has not reached
        // the wander max distance (meaning the player is still close enough to
        // the enemy that the enemy will wander and not rest) then we set the
        // Wander state
        _rest = false;
        _chase = false;
        _attack = false;
        _wander = true;
        Debug.Log("Enemy in Wander State");
      }
      else
      {
        // If the enemy is outside of the chase distance and greater than the
        // WanderMax then we want the enemy to stop moving entirely and rest
        // until the player once again enters the wander range or chase range.
        _rest = true;
        _chase = false;
        _attack = false;
        Debug.Log("Enemy in Rest State");
      }

      // Determine if enemy is close enough to player to chase
      if(_chase)
      {
        this.transform.LookAt(player.transform);
        agent.SetDestination(player.transform.position);
      }

      if(_wander)
      {
        // logic to wander the map
        transform.Translate(0, 0, agent.speed * Time.deltaTime);
        var dist = Vector3.Distance(player.transform.position, transform.position);
        Ray lookAhead = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.SphereCast(lookAhead, 0.75f, out hit))
        {
            if (hit.distance < obstacleRange)
            {
                float angle = Random.Range(-90, 90);
                transform.Rotate(0, angle, 0);
            }
            timer = changeDirectionTimer;
        }
        else
        {
            timer -= Time.deltaTime;
            if (timer > 0)
                return;
            else
            {
                float angle = Random.Range(-110, 110);
                transform.Rotate(0, angle, 0);
            }
        }
      }

    }
}
