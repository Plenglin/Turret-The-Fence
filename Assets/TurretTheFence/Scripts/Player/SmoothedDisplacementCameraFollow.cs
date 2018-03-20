using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothedDisplacementCameraFollow : MonoBehaviour {

    public Transform target;
    public int bufferLength;
    public float delay = 0.01f;

    private Vector3 displacement;
    private int index;
    private Vector3[] positions;
    private Vector3 positionSum;

    // Use this for initialization
    void Start() {
        positions = new Vector3[bufferLength];
        displacement = transform.position - target.position;
        positionSum = transform.position * bufferLength;
        index = 0;
        for (int i = 0; i < bufferLength; i++) {
            positions[i] = transform.position;
        }
    }

    void Update() {
        //transform.LookAt(target);
        Vector3 oldPos = positions[index];
        Vector3 newPos = target.position + displacement;
        positions[index] = newPos;
        positionSum += newPos - oldPos;
        transform.position = positionSum / bufferLength;
        index = (index + 1) % bufferLength;
    }
}
