using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HitByPlayer : MonoBehaviour
{
    static bool enemyHit;

    void OnCollisionEnter(Collision collision)
    {
        if(KnockbackOnCollision.enemyHit)
        {
            if(collision.collider.CompareTag("CollEvnt") || collision.collider.CompareTag("enemy") || collision.collider.CompareTag("sniper")
                || collision.collider.CompareTag("shooter") || collision.collider.CompareTag("expander") || collision.collider.CompareTag("Key Enemy"))
            {
                Destroy(gameObject);
                Debug.Log("Normal Enemy Destroyed");
                KnockbackOnCollision.enemyHit = false;
            }
        }
    }
}
