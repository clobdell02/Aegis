using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShooterAI : MonoBehaviour
{
    public NavMeshAgent turtle;
    public Transform Player;

    [SerializeField] private GameObject ProjectilePrefab;
    private GameObject _Projectile;
    private float cooldownTimer;
    private float restTimer = 5.0f;

    private float speed = 2.8f;
    private float obstacleRange = 3.0f;
    private float changeDirectionTimer = 1.2f;
    private float timer = 0.0f;

    private bool _rest;
    private bool _wander;
    private bool _engaged;

    // Start is called before the first frame update
    void Start()
    {
        _rest = true;
        _wander = false;
        _engaged = false;
    }

    // Update is called once per frame
    void Update()
    {
        var distance = Vector3.Distance(Player.position, transform.position);
        // rest state logic
        if (_rest)
        {
            if (distance <= 10)
            {
                _wander = true;
                _rest = false;
            }
            else
            {
                restTimer -= Time.deltaTime;
                if (restTimer <= 0)
                {
                    _wander = true;
                    _rest = false;
                    restTimer = 5.0f;
                }
            }
        }
        // wandering state logic
        if (_wander)
        {
            if (distance <= 15)
            {
                _engaged = true;
                _wander = false;
            }
            if (distance >= 30)
            {
                _rest = true;
                _engaged = false;
                _wander = false;
            }
            // logic to wander the map
            transform.Translate(0, 0, speed * Time.deltaTime);
            var dist = Vector3.Distance(Player.position, transform.position);
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
        // engage state logic
        if (_engaged)
        {
            if (distance < 12.0f)
            {
                transform.LookAt(Player);
                ShootAtPlayer();
            }
            else
            {
                _wander = true;
                _engaged = false;
            }
        }
    }

    void ShootAtPlayer()
    {
        cooldownTimer -= Time.deltaTime;
        if (cooldownTimer <= 0)
        {
            cooldownTimer = 2.0f;
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.SphereCast(ray, 0.75f, out hit))
            {
                _Projectile = Instantiate(ProjectilePrefab) as GameObject;
                _Projectile.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
                _Projectile.transform.rotation = transform.rotation;
            }
        }
    }
}
