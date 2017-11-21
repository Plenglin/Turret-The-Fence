using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycle : MonoBehaviour {

    public GameObject sun;
    public List<Light> lamps;
    public List<SolarControl> solars;

    public float dayLength = 0;
    public float time = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime / dayLength;
        bool isDay = time < 0.5;
        foreach (SolarControl s in solars) {
            s.enabled = isDay;
        }
        foreach (Light l in lamps) {
            l.enabled = !isDay;
        }
        Vector3 rot = sun.transform.rotation.eulerAngles;
        sun.transform.rotation.eulerAngles.Set(rot.x, time * 360, rot.y);
	}
}
