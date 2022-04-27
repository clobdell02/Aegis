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
    { }

    // Update is called once per frame
    void Update()
    {
        var distance = Vector3.Distance(Player.position, transform.position);
        if (distance <= 5)
        {
            slime.SetDestination(Player.position);
        }
    }
}

//public class SelectableEnemies : MonoBehaviour
//{
    // used for shockwave powerup
//}