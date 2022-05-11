using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KeyholderLogic : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform Player;
    private float speed = 3.5f;
    private float obstacleRange = 3.0f;
    private float changeDirectionTimer = 1.2f;
    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    { }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
        var dist = Vector3.Distance(Player.position, transform.position);
        Ray lookAhead = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.SphereCast(lookAhead, 0.75f, out hit))
        {
            if(hit.distance < obstacleRange)
            {
                float angle = Random.Range(-90, 90);
                transform.Rotate(0, angle, 0);
            }
            if (dist <= obstacleRange && hit.collider.gameObject.CompareTag("Player"))
            {
                float angle = Random.Range(170, 190);
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
