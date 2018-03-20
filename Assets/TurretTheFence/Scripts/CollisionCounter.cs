using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Keeps track of what's in this trigger.
/// </summary>
public class CollisionCounter : MonoBehaviour {

    public List<Collider> inTrigger = new List<Collider>();

    private void OnTriggerEnter(Collider other) {
        inTrigger.Add(other);
    }

    private void OnTriggerExit(Collider other) {
        inTrigger.Remove(other);
    }

}
