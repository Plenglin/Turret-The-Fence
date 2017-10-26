using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBuildingControl : MonoBehaviour {

    public TurretCreator[] turrets;
    public int money;

    private Dictionary<KeyCode, int> keyToNumber = new Dictionary<KeyCode, int>();
    private bool lastClicked = false;

	// Use this for initialization
	void Start () {

        keyToNumber.Add(KeyCode.Alpha1, 1);
        keyToNumber.Add(KeyCode.Alpha2, 2);
        keyToNumber.Add(KeyCode.Alpha3, 3);
        keyToNumber.Add(KeyCode.Alpha4, 4);
        keyToNumber.Add(KeyCode.Alpha5, 5);
        keyToNumber.Add(KeyCode.Alpha6, 6);
        keyToNumber.Add(KeyCode.Alpha7, 7);
        keyToNumber.Add(KeyCode.Alpha8, 8);
        keyToNumber.Add(KeyCode.Alpha9, 9);
        keyToNumber.Add(KeyCode.Alpha0, 10);

    }

    // Update is called once per frame
    void Update () {
	    
        foreach (KeyValuePair<KeyCode, int> pair in keyToNumber) {
            KeyCode code = pair.Key;
            int index = pair.Value - 1;
            if (index < turrets.Length && Input.GetKeyDown(code)) {

                TurretCreator turret = turrets[index];
                //Vector3 position = 

                if (!lastClicked && Input.GetMouseButtonDown(0)) {
                    lastClicked = true;
                }
            }
        }

	}
}
