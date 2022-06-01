using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SniperAi : MonoBehaviour
{
    public NavMeshAgent snips;
    public Transform Player;

    [SerializeField] private GameObject ProjectilePrefab;
    private GameObject _Projectile;
    private GameObject _Projectile1;
    private GameObject _Projectile2;
    private float cooldownTimer;

    // Start is called before the first frame update
    void Start()
    { }

    // Update is called once per frame
    void Update()
    {
        var distance = Vector3.Distance(Player.position, transform.position);

        // check for firing range
        if (distance < 45.0f)
        {
            transform.LookAt(Player);
            if (cooldownTimer <= 0)
            {
                cooldownTimer = 2.5f;
                ShootAtPlayer();
            }
            cooldownTimer -= Time.deltaTime;
        }
    }

    void ShootAtPlayer()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.SphereCast(ray, 0.75f, out hit))
        {
            _Projectile = Instantiate(ProjectilePrefab) as GameObject;
            _Projectile.transform.position = transform.TransformPoint(Vector3.forward * 1.9f);
            _Projectile.transform.rotation = transform.rotation;

            _Projectile1 = Instantiate(ProjectilePrefab) as GameObject;
            _Projectile1.transform.position = transform.TransformPoint(Vector3.forward * 1.2f);
            _Projectile1.transform.rotation = transform.rotation;

            _Projectile2 = Instantiate(ProjectilePrefab) as GameObject;
            _Projectile2.transform.position = transform.TransformPoint(Vector3.forward * 0.5f);
            _Projectile2.transform.rotation = transform.rotation;
        }
    }
}
