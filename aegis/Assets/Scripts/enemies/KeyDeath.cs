using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KeyDeath : MonoBehaviour
{
    public NavMeshAgent agent;
    static bool enemyHit;
    static int enemiesDefeated;

    void OnCollisionEnter(Collision collision)
    {
        if (KnockbackOnCollision.enemyHit)
        {
            if (collision.collider.CompareTag("CollEvnt") || collision.collider.CompareTag("enemy"))
            {
                if (agent.gameObject.CompareTag("Key Enemy"))
                {
                    KeyCreator.enemiesDefeated += 1;
                    Destroy(gameObject);
                    //Debug.Log("Key Enemy Destroyed");
                    Debug.Log("Enemies defeated: " + KeyCreator.enemiesDefeated);
                }
                else
                {
                    Destroy(gameObject);
                    //Debug.Log("Normal Enemy Destroyed");
                }
                KnockbackOnCollision.enemyHit = false;
            }
        }
    }
}
