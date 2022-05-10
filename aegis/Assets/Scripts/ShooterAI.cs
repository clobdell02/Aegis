using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShooterAI : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent turtle;
    public Transform Player;
    public Transform Projectile;
    public float enemyBulletSpeed = 10.0f;
    [SerializeField] private float cooldown = 3;
    private float cooldownTimer;

    // Start is called before the first frame update
    void Start()
    { }

    // Update is called once per frame
    void Update()
    {
        var distance = Vector3.Distance(Player.position, transform.position);
        if (distance <= 10)
        {
            //slime.SetDestination(Player.position);
            ShootAtPlayer();
        }
    }

    void ShootAtPlayer()
    {
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer > 0) return;

        cooldownTimer = cooldown;

        /*GameObject tempBullet = Instantiate(Projectile, transform.position, transform.rotation) as GameObject; //shoots from enemies eyes
        Rigidbody tempRigidBodyBullet = tempBullet.GetComponent<Rigidbody>();
        tempRigidBodyBullet.AddForce(tempRigidBodyBullet.transform.forward * enemyBulletSpeed);
        Destroy(tempBullet, 0.1f);*/
    }
}
