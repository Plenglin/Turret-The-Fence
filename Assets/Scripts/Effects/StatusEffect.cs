using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TurretTheFence.StatusEffect {
    public abstract class StatusEffect {

        public StatusState state = StatusState.PRE_START;
        public GameObject receiver;
        public string tag;

        public StatusEffect(string tag) {
            this.tag = tag;
        }

        internal void Start(GameObject receiver) {
            this.receiver = receiver;
            state = StatusState.ACTIVE;
            OnStart();
        }

        /**
         * <summary>
         * Applies the status effect every time Update is called.
         * </summary>
         * <returns>
         * If we should remove the effect
         * </returns>         * 
         */
        internal bool Loop() {
            return OnLoop();
        }

        internal void End() {
            OnEnd();
            state = StatusState.ENDED;
        }

        internal void Refresh() {

        }

        public abstract bool OnStart();

        /**
         * <summary>
         * Applies the status effect every time Update is called.
         * </summary>
         * <returns>
         * If we should remove the effect
         * </returns>
         */
        public abstract bool OnLoop();

        public abstract void OnEnd();

    }

    public abstract class TimedStatusEffect : StatusEffect {

        private float duration, endTime;

        public TimedStatusEffect(float duration) {
            this.duration = duration;
        }

        new void Start(GameObject receiver) {
            Refresh();
            base.Start(receiver);
        }

        new bool Loop() {
            return base.Loop() || Time.time >= endTime;
        }

        new void Refresh() {
            endTime = Time.time + duration;
        }

    }

    public enum StatusState {
        PRE_START, ACTIVE, ENDED
    }

}