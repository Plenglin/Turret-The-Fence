using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBuildingControl : MonoBehaviour {

    public TurretType[] turrets;
    public int money;

    private GameObject ghostTurret;
    private int floorMask;
    private Dictionary<KeyCode, int> keyToNumber = new Dictionary<KeyCode, int>();
    private TurretType currentTurret = null;

	// Use this for initialization
	void Start () {

        floorMask = LayerMask.GetMask("Floor");

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
            if (index < turrets.Length) {
                if (Input.GetKeyDown(code)) {
                    currentTurret = turrets[index];
                    ghostTurret = Instantiate(currentTurret.turret);
                    ghostTurret.GetComponent<TurretTargetingControl>().enabled = false;
                }
                if (Input.GetKeyUp(code)) {
                    currentTurret = null;
                    Destroy(ghostTurret);
                    ghostTurret = null;
                }
            }
        }

        if (currentTurret != null) {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit floorHit;

            Vector3 turretPosition;

            if (Physics.Raycast(mouseRay, out floorHit, 100f, floorMask)) {
                turretPosition = floorHit.point;
                ghostTurret.transform.position = turretPosition;
            }

            if (Input.GetMouseButtonDown(0)) {
                GameObject newTurret = Instantiate(ghostTurret);
                newTurret.GetComponent<TurretTargetingControl>().enabled = true;
                //newTurret.GetComponent<TurretBuildingControl>()
                
            }
        }

	}
}
