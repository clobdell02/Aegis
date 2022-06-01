using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float speed = 10.0f;

    // Start is called before the first frame update
    void Start()
    { }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit Player");
            Destroy(this.gameObject);
        }
        else if (other.gameObject.CompareTag("Shield") || other.gameObject.CompareTag("CollEvnt"))
        {
            Debug.Log("Hit non player");
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject, 2.5f);
        }
    }
}