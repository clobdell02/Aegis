using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HitByPlayer : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject key;
    static bool enemyHit;

    void OnCollisionEnter(Collision collision)
    {
        if(KnockbackOnCollision.enemyHit)
        {
            if(collision.collider.CompareTag("CollEvnt") || collision.collider.CompareTag("enemy"))
            {
                if(agent.gameObject.CompareTag("Key Enemy"))
                {
                    onDeath();
                    Destroy(gameObject);
                    Debug.Log("Key Enemy Destroyed");
                }
                else
                {
                    Destroy(gameObject);
                    Debug.Log("Normal Enemy Destroyed");
                }
                KnockbackOnCollision.enemyHit = false;
            }
        }
    }

    void onDeath()
    {
        Instantiate(key, transform.position, key.transform.rotation);
        Debug.Log("Key available");
    }
}
