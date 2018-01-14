using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TurretTheFence.Effect {
    public abstract class StatusEffect {

        public StatusState state = StatusState.PRE_START;
        public GameObject receiver;
        public string tag;
        public readonly int maxStacks = 1;
        
        public int stacks = 1;

        private float duration, endTime;

        public StatusEffect(string tag, float duration) {
            this.tag = tag;
            this.duration = duration;
        }

        internal void Start(GameObject receiver) {
            this.receiver = receiver;
            state = StatusState.ACTIVE;
            Refresh();
            OnStart();
        }

        /**
         * <summary>
         * Applies the status effect every time Update is called.
         * </summary>
         * <returns>
         * If we should remove the effect
         * </returns> 
         */
        internal bool Loop() {
            return OnLoop() || Time.time >= endTime;
        }

        internal void End() {
            OnEnd();
            state = StatusState.ENDED;
        }

        public void Refresh() {
            endTime = Time.time + duration;
        }

        protected abstract void OnStart();

        /**
         * <summary>
         * Applies the status effect every time Update is called.
         * </summary>
         * <returns>
         * If we should remove the effect
         * </returns>
         */
        protected abstract bool OnLoop();

        protected abstract void OnEnd();

    }

    public enum StatusState {
        PRE_START, ACTIVE, ENDED
    }

    public abstract class TickingStatusEffect : StatusEffect {

        private float tickDelay, nextTick;

        public TickingStatusEffect(string tag, float tickDelay, float duration) : base(tag, duration) {
            this.tickDelay = tickDelay;
        }

        internal new void Start(GameObject receiver) {
            base.Start(receiver);
            nextTick = Time.time + tickDelay;
        }

        protected override bool OnLoop() {
            if (Time.time >= nextTick) {
                nextTick += tickDelay;
                OnTick();
            }
            return false;
        }

        protected abstract void OnTick();
        
    }

}