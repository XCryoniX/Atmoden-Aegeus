using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Code Block A - Moves character's rigidbody. W moves character forward, A moves left, D moves right, S moves backwards, A + Left Shift runs forward.
 * Code Block B - Clamps maximum and minimum y-value peaks the camera is allowed to move to. Code Block B also prevents camera from going through obstacles and blocking the players view.
 * Code Block C - Rotates player so that its rotation matches the camera.
 */

[RequireComponent(typeof(Rigidbody))]
public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField]
    private new Transform camera;
    private PreventCameraClipping preventCameraClipping;

    [SerializeField]
    private Transform cameraTarget;

    [SerializeField]
    private float normalSpeed = 1.0f;
    [SerializeField]
    private float runSpeed = 2.0f;

    [SerializeField]
    private float sensitivity = 1.0f;

    private float x;
    private float y;

    private Rigidbody rb;

    private float cameraZDistance = -3.0f;

    private Vector3 vecToClamp;
    private float clampY;
    private Vector3 clampedVec;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        preventCameraClipping = camera.GetComponent<PreventCameraClipping>();
    }

    void Update()
    {
        x = Input.GetAxis("Mouse X") * sensitivity;
        y = Input.GetAxis("Mouse Y") * sensitivity;

        //Placed Code Block A below to prevent jittery movement from player
        //Code Block A Start
        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                rb.MovePosition(transform.position + transform.forward * runSpeed * Time.deltaTime);
            }
            else
            {
                rb.MovePosition(transform.position + transform.forward * normalSpeed * Time.deltaTime);
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.MovePosition(transform.position - transform.forward * normalSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.MovePosition(transform.position + transform.right * normalSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rb.MovePosition(transform.position - transform.right * normalSpeed * Time.deltaTime);
        }
        //Code Block A End

        //Placed Code Block B below to prevent jittery movement from player
        //Code Block B Start
        if (preventCameraClipping.cameraViewObstructed)
        {
            cameraZDistance += 0.05f;
        }
        else
        {
            if (cameraZDistance > -3.0f && !preventCameraClipping.obstacleBehindCamera)
            {
                cameraZDistance -= 0.05f;
            }
        }

        vecToClamp = cameraTarget.position + camera.TransformDirection(new Vector3(-x, -y, cameraZDistance));
        clampY = Mathf.Clamp(vecToClamp.y, 0.5f, 4.0f);
        clampedVec = new Vector3(vecToClamp.x, clampY, vecToClamp.z);

        camera.position = clampedVec;
        camera.LookAt(cameraTarget.position);
        //Code Block B End

        //Placed Code Block C below to prevent jittery rotation from player
        //Code Block C Start
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            transform.eulerAngles = new Vector3(0.0f, camera.eulerAngles.y, 0.0f);
        }
        //Code Block C End
    }
}
