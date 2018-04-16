using System.Collections;
using System.Collections.Generic;
using TurretTheFence.Player;
using TurretTheFence.Turret;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace TurretTheFence.Player {
    public class TurretBuilderTool : MonoBehaviour {

        public float maxSlope;

        private GameObject ghostTurret, player, playerHead;
        private Transform cam;
        private MoneyControl balance;
        private int floorMask;
        private TurretMode turretMode;
        private TurretType currentTurret;
        private Text dataDisplay;
        private TurretManager turretMan;

        private void Awake() {
            dataDisplay = GameObject.FindGameObjectWithTag("AmmoIndicator").GetComponent<Text>();
            player = GameObject.FindGameObjectWithTag("Player");
            playerHead = GameObject.FindWithTag("PlayerHead");
            cam = GameObject.FindWithTag("Camera").transform;
            balance = player.GetComponent<MoneyControl>();
            floorMask = LayerMask.GetMask("Floor");
            turretMan = GameObject.FindWithTag("TurretManager").GetComponent<TurretManager>();
        }

        private void Start() {
            turretMode = playerHead.GetComponent<WeaponSwitcher>().turretMode;
        }

        private void OnEnable() {
            if (ghostTurret != null) {
                ghostTurret.SetActive(true);
            }
        }

        private void OnDisable() {
            if (ghostTurret != null) {
                ghostTurret.SetActive(false);
            }
        }

        public void OnTurretChange(TurretType newTurret) {
            if (ghostTurret != null) {
                Destroy(ghostTurret);
            }
            currentTurret = newTurret;
            ghostTurret = Instantiate(currentTurret.prefab);
            ghostTurret.GetComponent<TurretDirectionControl>().enabled = false;
            ghostTurret.GetComponent<TurretTargetingControl>().enabled = false;
            ghostTurret.GetComponent<NavMeshObstacle>().enabled = false;
            ghostTurret.GetComponent<Collider>().enabled = false;
        }

        void Update() {
            int price = turretMan.PriceOf(currentTurret);
            RaycastHit floorHit;
            Vector3 turretPosition;
            if (Physics.Raycast(cam.position, cam.forward, out floorHit, 100f, floorMask)) {
                turretPosition = floorHit.point;
                ghostTurret.transform.position = turretPosition;
            } else {
                return;
            }

            if (Input.GetMouseButtonDown(1) && balance.money >= price && IsValidTurretPos(turretPosition)) {
                GameObject newTurret = Instantiate(ghostTurret);
                newTurret.GetComponent<TurretDirectionControl>().enabled = true;
                newTurret.GetComponent<TurretTargetingControl>().enabled = true;
                newTurret.GetComponent<NavMeshObstacle>().enabled = true;
                newTurret.GetComponent<Collider>().enabled = true;
                balance.money -= price;
                turretMan.AddTurret(newTurret);
            }
            
            dataDisplay.text = string.Format("${0}\n{1}", price, currentTurret.name);
        }
        
        bool IsValidTurretPos(Vector3 pos) {
            RaycastHit hit;
            if (Physics.Raycast(pos + Vector3.up * 0.05f, Vector3.down, out hit, 0.1f)) {
                Debug.Log(Vector3.Dot(hit.normal, Vector3.up));
                if (Vector3.Dot(hit.normal, Vector3.up) < maxSlope) {
                    return false;
                }
                return true;
            } else {
                return false;
            }
        }

    }
}