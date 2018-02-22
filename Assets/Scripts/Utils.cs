using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurretTheFence.Utils {

    public class Constants {

        public static Dictionary<KeyCode, int> keyToNumber = new Dictionary<KeyCode, int>();

        static Constants() {
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

    }

    public class SmoothedAverage {

        private float[] items;
        private int bufferLength;
        private int index;

        private float _sum;
        public float sum {
            get {
                return _sum;
            }
            private set {
                _sum = value;
            }
        }

        public float avg {
            get {
                return sum / bufferLength;
            }
        }

        public SmoothedAverage(int bufferLength, float init) {
            items = new float[bufferLength];
            this.bufferLength = bufferLength;
            index = 0;
            sum = init * bufferLength;
            for (int i = 0; i < bufferLength; i++) {
                items[i] = init;
            }
        }

        public void push(float value) {
            float old = items[index];
            items[index] = value;
            sum += value - old;
            index = (index + 1) % bufferLength;
        }

    }

    public class PeriodicDelayer {

        private float nextEvent, delay;

        public PeriodicDelayer(float delay) {
            this.delay = delay;
        }

        public void Try(Action func) {
            if (Time.time >= nextEvent) {
                nextEvent = Time.time + delay;
                func.Invoke();
            }
        }

    }


    [System.Serializable]
    public class WeightedLootTable {

        public List<LootTableEntry> table;

        private int sum;
        private List<LootTableEntry> cumulativeTable;

        public void GenerateCumulativeTable() {
            sum = 0;
            foreach (LootTableEntry entry in table) {
                sum += entry.weight;
                cumulativeTable.Add(new LootTableEntry(entry.loot, sum));
            }
        }

        public GameObject Get() {
            int result = UnityEngine.Random.Range(0, sum);
            foreach (LootTableEntry entry in table) {
                if (entry.weight >= result) {
                    return entry.loot;
                }
            }
            throw new IndexOutOfRangeException();
        }

    }

    [System.Serializable]
    public class LootTableEntry {

        public GameObject loot;
        public int weight = 1;

        public LootTableEntry() { }

        public LootTableEntry(GameObject loot, int weight) {
            this.loot = loot;
            this.weight = weight;
        }
    }
}