using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class slimeAI : MonoBehaviour
{
    public NavMeshAgent slime;
    public Transform Player;

    private float speed = 2.75f;
    private bool _rest;
    private bool _chase;
    //private bool _attack;

    // Start is called before the first frame update
    void Start()
    {
        _rest = true;
        _chase = false;
        //_attack = false;
    }

    // Update is called once per frame
    void Update()
    {
        var distance = Vector3.Distance(Player.position, transform.position);
        // _rest state logic
        if (_rest)
        {
            if (distance <= 5)
            {
                _chase = true;
                _rest = false;
            }
            /*if (distance <= 2)
            {
                _attack = true;
                _rest = false;
            }*/
        }
        // _chase state logic
        if (_chase)
        {
            /*if (distance <= 2)
            {
                _attack = true;
                _chase = false;
            }
            else*/
            if (distance > 10)
            {
                _rest = true;
                _chase = false;
            }
            else
            {
                transform.LookAt(Player);
                slime.transform.position = transform.TransformPoint(Vector3.forward * 0.05f);
                slime.transform.rotation = transform.rotation;
                //slime.SetDestination(Player.position);
            }
        }
        // needed later for animations
        /*if (_attack)
        {
            if (3 <= distance < 12)
            {
                _chase = true;
                _attack = false;
            }
            else if (distance > 12)
            {
                _rest = true;
                _attack = false;
            }
            else
            {
                // animate attack on player
            }
        }*/
    }
}
