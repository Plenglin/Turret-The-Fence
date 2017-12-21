using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils {
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
}