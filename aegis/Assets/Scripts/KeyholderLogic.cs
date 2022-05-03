using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KeyholderLogic : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform Player;
    static bool hit;

    // Start is called before the first frame update
    void Start()
    { }

    // Update is called once per frame
    void Update()
    {
        var distance = Vector3.Distance(Player.position, transform.position);
        if (distance <= 5)
        {
            agent.SetDestination(-(Player.position));
        }
    }
}
