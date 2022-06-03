using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperProjectile : MonoBehaviour
{
    private float speed = 14.0f;
    private float travel_time = 3.5f;

    // Start is called before the first frame update
    void Start()
    { }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
        if (travel_time <= 0.0f)
        {
            Destroy(this.gameObject);
            //travel_time = 2.5f;
        }
        else
        {
            travel_time -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }
}
