using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreventCameraClipping : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [HideInInspector]
    public bool cameraViewObstructed = false;
    [HideInInspector]
    public bool obstacleBehindCamera = false;

    private RaycastHit hit;

    [SerializeField]
    private LayerMask layerMask;

    void Update()
    {
        if (Physics.Raycast(transform.position, player.position - transform.position, out hit, Vector3.Distance(transform.position, player.position), layerMask))
        {
            cameraViewObstructed = true;
        }
        else
        {
            cameraViewObstructed = false;
        }

        if (Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(0.0f, 0.0f, 0.2f)), transform.TransformDirection(-Vector3.forward), out hit, Vector3.Distance(transform.position, transform.TransformDirection(-Vector3.forward)), layerMask))
        {
            obstacleBehindCamera = true;
        }
        else
        {
            obstacleBehindCamera = false;
        }
    }
}
