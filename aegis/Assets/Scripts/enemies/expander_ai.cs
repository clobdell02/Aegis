using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class expander_ai : MonoBehaviour
{
    public Transform player;

    private float cool_down_timer;
    private float detection_range = 10.5f;
    private float expand_range = 5.0f;

    private bool _resting;
    private bool _expanded;

    // Start is called before the first frame update
    void Start()
    {
        // states the agent can be in
        _resting = true;
        _expanded = false;
    }

    // Update is called once per frame
    void Update()
    {
        // get the distance to the player
        var distToPlayer = Vector3.Distance(player.transform.position, this.transform.position);

        // in rest state 
        if(_resting)
        {
            // if player is in attacking (expanding) range
            if (distToPlayer < expand_range)
            {
                //  enter exapnded state
                _resting = false;
                _expanded = true;
                // increase the scale of the agent
                transform.localScale = new Vector3(1.75f, 1.75f, 1.75f);
                cool_down_timer = 5.0f;
            }
            else if (distToPlayer < detection_range)
            {
                transform.LookAt(player);
            }
        }
        if(_expanded)
        {
            cool_down_timer -= Time.deltaTime;
            if (cool_down_timer <= 3.0f)
            {
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
            if (cool_down_timer <= 0.0f)
            {
                _expanded = false;
                _resting = true;
            }
        }
    }
}
