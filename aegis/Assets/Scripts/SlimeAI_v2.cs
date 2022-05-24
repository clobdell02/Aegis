using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAI_v2 : MonoBehaviour
{
  UnityEngine.AI.NavMeshAgent agent;
  GameObject player;

  // Values to compare against player distance to determine the
  // state the agent should be in
  public float ChaseDist = 6.0f; // Distance at which agent chases Player
  public float AttackDist = 0.5f; // Distance at which agent attacks Player

  // States
  private bool _rest;
  private bool _chase;
  private bool _attack;

  public Transform target;
    void Start()
    {
      agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
      player = GameObject.FindGameObjectWithTag("Player");

      // Set States
      _rest = true;
      _chase = false;
      _attack = false;
    }

    void Update()
    {
      var distToPlayer = Vector3.Distance(player.transform.position, transform.position);
      // Check if the enemy is within range to see the player
      if((distToPlayer <= ChaseDist) & (distToPlayer > AttackDist))
      {
        // If the enemy is close enough to see the player we want it
        // enter the chase state
        _rest = false;
        _chase = true;
        _attack = false;
        Debug.Log("Enemy in Chase State");
      }
      else if(distToPlayer <= AttackDist)
      {
        // If the enemy is in range to attack the player we want it
        // to enter the attack state while also remaining in the chase
        // state.
        _rest = false;
        _chase = true;
        _attack = true;
        Debug.Log("Enemy in Attack State");
      }
      else
      {
        // If the enemy can no longer see the player then we want it to
        // enter the rest state again
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

    }
}
