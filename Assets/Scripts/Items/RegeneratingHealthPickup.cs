using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurretTheFence.Items {

    public class RegeneratingHealthPickup : MonoBehaviour {

        public float rotationPeriod, bobPeriod, bobAmplitude;
        public GameObject displayObject;

        private float bobOffset = 0;
        private Vector3 startingPos;

        // Use this for initialization
        void Start() {
            startingPos = displayObject.transform.position;
        }

        private void Update() {
            float offset = bobAmplitude * Mathf.Sin(Time.time * 2 * Mathf.PI / bobPeriod);
            float rot = Time.deltaTime * 360 / rotationPeriod;
            transform.Rotate(transform.up, rot);
            displayObject.transform.position = startingPos + new Vector3(0f, offset, 0f);
        }

    }
}