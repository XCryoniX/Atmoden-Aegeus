using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimb : MonoBehaviour
{
    Rigidbody rb;
    private bool enableClimb = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enableClimb)
        {
            rb.useGravity = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);

        if (rb.velocity.y < 0.0f)
        {
            if (other.tag[0] == '1')
            {
                enableClimb = true;
                rb.Sleep();
            }
        }
    }
}
