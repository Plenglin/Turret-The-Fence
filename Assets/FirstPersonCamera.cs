using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour {

    public float sensitivity = 1;

    public float upperYLimit = 90, lowerYLimit = -90;

    private float xRot, yRot;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        xRot += Input.GetAxis("Mouse X") * sensitivity;
        yRot = Mathf.Clamp(yRot + Input.GetAxis("Mouse Y") * sensitivity, lowerYLimit, upperYLimit);
        //yRot = yRot + Input.GetAxis("Mouse Y") * sensitivity;
        //Logger.

        transform.rotation = Quaternion.identity * Quaternion.Euler(-yRot, xRot, 0);
    }
}
