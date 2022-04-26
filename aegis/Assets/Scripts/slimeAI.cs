using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class slimeAI : MonoBehaviour
{
    public NavMeshAgent slime;
    public Transform Player;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        slime.SetDestination(Player.position);
    }
}