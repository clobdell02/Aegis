using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class slimeAI : MonoBehaviour
{
    public Transform Player;
    public Rigidbody rb;
    public float speed = 2.45f;

    private bool _rest;
    private bool _chase;
    private bool _attack;
    private Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        _rest = true;
        _chase = false;
        _attack = false;
    }

    // Update is called once per frame
    void Update()
    {
        var distToPlayer = Vector3.Distance(Player.position, transform.position);
        Vector3 direction = Player.position - transform.position;
        float angle = (Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg);
        // _rest state logic
        if (_rest)
        {
            if (distToPlayer < 25)
            {
                _rest = false;
                _chase = false;
                _attack = false;
            }
        }
        // _chase state logic
        else if (_chase)
        {
            if (distToPlayer < 3)
            {
                _rest = false;
                _chase = false;
                _attack = true;
            }
            else
            {
                transform.Rotate(0, angle, 0);
                direction.Normalize();
                movement = direction;
            }
        }
        // _attack state logic
    }

    private void FixedUpdate()
    {
        moveCharacter(movement);
    }

    void moveCharacter(Vector3 direction)
    {
        rb.MovePosition(transform.position + (direction * speed * Time.deltaTime));
    }
}
