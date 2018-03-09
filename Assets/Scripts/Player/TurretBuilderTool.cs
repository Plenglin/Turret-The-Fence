using System.Collections;
using System.Collections.Generic;
using TurretTheFence.Player;
using TurretTheFence.Turret;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace TurretTheFence.Player {
    public class TurretBuilderTool : MonoBehaviour {

        private GameObject ghostTurret, player;
        private MoneyControl balance;
        private int floorMask;
        private TurretMode turretMode;
        private TurretType currentTurret;
        private Text dataDisplay;
        private TurretManager turretMan;

        private void Awake() {
            dataDisplay = GameObject.FindGameObjectWithTag("AmmoIndicator").GetComponent<Text>();
            player = GameObject.FindGameObjectWithTag("Player");
            turretMode = player.GetComponent<WeaponSwitcher>().turretMode;
            balance = player.GetComponent<MoneyControl>();
            floorMask = LayerMask.GetMask("Floor");
            turretMan = GameObject.FindWithTag("TurretManager").GetComponent<TurretManager>();
        }

        private void OnEnable() {
            ghostTurret.SetActive(true);
        }

        private void OnDisable() {
            ghostTurret.SetActive(false);
        }

        public void OnTurretChange(TurretType newTurret) {
            Destroy(ghostTurret);
            currentTurret = newTurret;
            ghostTurret = Instantiate(newTurret.prefab);
            ghostTurret.GetComponent<TurretDirectionControl>().enabled = false;
            ghostTurret.GetComponent<TurretTargetingControl>().enabled = false;
            ghostTurret.GetComponent<NavMeshObstacle>().enabled = false;
            ghostTurret.GetComponent<Collider>().enabled = false;
        }

        void Update() {
            int price = turretMan.PriceOf(currentTurret);
            RaycastHit floorHit;
            Ray ray = new Ray(player.transform.position, player.transform.forward);
            Vector3 turretPosition;
            if (Physics.Raycast(ray, out floorHit, 100f, floorMask)) {
                turretPosition = floorHit.point;
                ghostTurret.transform.position = turretPosition;
            }

            if (Input.GetMouseButtonDown(1)) {
                if (balance.money >= price) {
                    GameObject newTurret = Instantiate(ghostTurret);
                    newTurret.GetComponent<TurretDirectionControl>().enabled = true;
                    newTurret.GetComponent<TurretTargetingControl>().enabled = true;
                    newTurret.GetComponent<NavMeshObstacle>().enabled = true;
                    newTurret.GetComponent<Collider>().enabled = true;
                    balance.money -= price;
                    turretMan.AddTurret(newTurret);
                }
            }
            dataDisplay.text = string.Format("${0}\n{1}", price, currentTurret.name);
        }

    }
}