using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class slimeAI : MonoBehaviour
{
    public NavMeshAgent slime;
    public Transform Player;

    private float speed = 2.45f;
    private float obstacleRange = 3.0f;

    // Start is called before the first frame update
    void Start()
    { }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
        
        var distToPlayer = Vector3.Distance(Player.position, transform.position);
        if (distToPlayer < 10)
        {
            huntPlayer();
        }
        else
        {
            Ray lookAhead = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.SphereCast(lookAhead, 0.75f, out hit))
            {
                if (hit.distance < obstacleRange)
                {
                    float angle = Random.Range(-110, 110);
                    transform.Rotate(0, angle, 0);
                }
            }
        }
    }

    void huntPlayer()
    {
        slime.SetDestination(Player.position);
    }
}
