using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement_JumpMechanic : MonoBehaviour
{
    private float y;

    private void Update()
    {
        y = Input.GetAxis("Mouse X");

        transform.eulerAngles += new Vector3(0.0f, y, 0.0f);
    }
}
