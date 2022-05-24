using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownBar : MonoBehaviour
{

    GameObject bar;
    LineRenderer barRenderer;

    // Start is called before the first frame update
    void Start()
    {
        bar = GameObject.FindWithTag("CoolBar");
        barRenderer = bar.GetComponent<LineRenderer>();
        barRenderer.startColor = Color.red;
        barRenderer.endColor = Color.red;
        barRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (ThirdPersonDash.inCooldown)
        {
            Vector3 newScale = new Vector3(1, 1, ThirdPersonDash.currTime);
            bar.transform.localScale = newScale;
            barRenderer.enabled = true;
            barRenderer.startColor = Color.Lerp(Color.red, Color.green, ThirdPersonDash.currTime);
            barRenderer.endColor = Color.Lerp(Color.red, Color.green, ThirdPersonDash.currTime);  
        }
        else 
        {
            barRenderer.enabled = false;
        }
    }
}
