using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitByPlayer : MonoBehaviour
{
    static bool enemyHit;
    void OnCollisionEnter(Collision collision)
    {
      if(KnockbackOnCollision.enemyHit)
      {
        if(collision.collider.CompareTag("CollEvnt") || collision.collider.CompareTag("enemy"))
        {
          Debug.Log("Object Destroyed");
          Destroy(gameObject);
          KnockbackOnCollision.enemyHit = false;
        }
      }
    }
}
