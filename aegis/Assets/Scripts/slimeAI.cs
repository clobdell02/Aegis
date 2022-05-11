using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class slimeAI : MonoBehaviour
{
    public NavMeshAgent slime;
    public Transform Player;

    private float speed = 0.0f;
    private bool _rest;
    private bool _awake;
    //private bool _attack;

    // Start is called before the first frame update
    void Start()
    {
        _rest = true;
        _awake = false;
        //_attack = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, speed * 1.25f * Time.deltaTime);
        var closeness = Vector3.Distance(Player.position, transform.position);
        // _rest state logic
        if (_rest)
        {
            //Debug.Log("Resting");
            if (closeness < 10)
            {
                //Debug.Log("Time to chase");
                _rest = false;
                _awake = true;
                speed = 2.35f;
            }
        }
        // _chase state logic
        if (_awake)
        {
            /*if (distance <= 2)
            {
                _attack = true;
                _chase = false;
            }
            else*/
            if (closeness > 40)
            {
                //Debug.Log("Time to sleep");
                _awake = false;
                _rest = true;
                speed = 0.0f;
            }
            else
            {
                transform.LookAt(Player);
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
