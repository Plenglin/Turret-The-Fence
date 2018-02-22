using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurretTheFence.Effect {

    public class StatusEffectManager : MonoBehaviour {

        public List<StatusEffect> statuses = new List<StatusEffect>();

        private void Update() {
            foreach (StatusEffect fx in statuses) {
                switch (fx.state) {
                    case StatusState.PRE_START:
                        fx.Start(gameObject);
                        goto case StatusState.ACTIVE;
                    case StatusState.ACTIVE:
                        bool shouldRemove = fx.Loop();
                        if (shouldRemove) {
                            fx.End();
                        }
                        break;
                }
            }

            statuses.RemoveAll((x) => x.state == StatusState.ENDED);
        }

        /// <summary>
        /// Applies a status effect to this object. If there already exists one with the same tag, increments that stack and refreshes
        /// the cooldown.
        /// </summary>
        /// <param name="status"></param>
        /// <param name="stacks"></param>
        public void Apply(StatusEffect status, int stacks) {
            StatusEffect found = statuses.Find(x => x.tag == status.tag);
            if (found != null) {
                found.stacks = Mathf.Max(found.stacks + stacks, found.maxStacks);
                found.Refresh();
            } else {
                statuses.Add(status);
            }
        }

        public void Apply(StatusEffect status) {
            Apply(status, 1);
        }

    }

}