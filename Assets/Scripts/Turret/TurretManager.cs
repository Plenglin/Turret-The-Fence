using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TurretTheFence.Turret {
    public class TurretManager : MonoBehaviour {

        public TurretType[] turrets;
        public float buildIncreaseRate = 1;
        private List<TurretType> boughtCache = new List<TurretType>();
        private List<GameObject> currentTurrets = new List<GameObject>();

        public int Count {
            get {
                return boughtCache.Count;
            }
        }

        private void Awake() {
            RefreshTurretCache();
        }

        public float GetTurretSpawnMultiplier() {
            return Mathf.Pow(buildIncreaseRate, currentTurrets.Count);
        }

        public int PriceOf(TurretType type) {
            float baseCost = GetTurretSpawnMultiplier() * type.baseBuildCost;
            return Mathf.RoundToInt(baseCost / 5) * 5;  // Round down to lowest 5 so it looks good
        }

        public void AddTurret(GameObject turret) {
            currentTurrets.Add(turret);
        }

        public void RefreshTurretCache() {
            boughtCache.Clear();
            foreach (TurretType t in turrets) {
                if (t.bought) {
                    boughtCache.Add(t);
                }
            }
        }

        public TurretType this[int index] {
            get {
                return boughtCache[index];
            }
        }

    }
}