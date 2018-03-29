using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycle : MonoBehaviour {

    public GameObject sun;
    public List<Light> lamps;
    public List<SolarControl> solars;

    public float dayLength = 0;
    public float time = 0;
    public bool doDaylightCycle = true;

    private bool lastIsDay;

	// Use this for initialization
	void Start () {
        lastIsDay = false;
        if (time < 0.5) {
            StartCoroutine(OnDay());
        } else { 
            StartCoroutine(OnNight());
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (doDaylightCycle) {
            time = (time + Time.deltaTime / dayLength) % 1;
        }
        bool isDay = time < 0.5;
        if (!lastIsDay && isDay) {
            StartCoroutine(OnDay());
        } else if (lastIsDay && !isDay) {
            StartCoroutine(OnNight());
        }

        foreach (SolarControl s in solars) {
            s.enabled = isDay;
        }
        
        // sun.transform.rotation = Quaternion.Euler(time * 360, 0, 0);
        lastIsDay = isDay;
	}

    IEnumerator OnDay() {
        foreach (Light l in lamps) {
            l.enabled = false;
        }
        yield return new WaitUntil(() => { return true; });
    }

    IEnumerator OnNight() {
        yield return new WaitForSeconds(dayLength / 20);
        foreach (Light l in lamps) {
            StartCoroutine(FlickerLightOn(l));
        }
    }

    IEnumerator FlickerLightOn(Light light) {
        yield return new WaitForSeconds(Random.Range(0.2f, 0.5f));
        light.enabled = true;
        yield return new WaitForSeconds(Random.Range(0.2f, 0.5f));
        light.enabled = false;
        yield return new WaitForSeconds(Random.Range(0.2f, 0.5f));
        light.enabled = true;
    }
}
