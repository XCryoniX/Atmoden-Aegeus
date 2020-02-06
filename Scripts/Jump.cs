using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Jump : MonoBehaviour
{
    [SerializeField]
    private char platformTag;
    [SerializeField]
    private float platformForceMagnitude = 1.0f;
    [SerializeField]
    private float jumpForceMagnitude = 1.0f;
    [SerializeField]
    private float groundRayEnd;

    private Rigidbody rb;

    private bool isGrounded = true;

    private RaycastHit hit;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, -Vector3.up * groundRayEnd, Color.yellow);

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector3(0.0f, jumpForceMagnitude, 0.0f), ForceMode.Impulse);
        }

        if (Physics.Raycast(transform.position, -Vector3.up, out hit, groundRayEnd))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag[0] == platformTag)
        {
            rb.AddForce(new Vector3(0.0f, platformForceMagnitude, 0.0f), ForceMode.Impulse);
        }
    }
}
