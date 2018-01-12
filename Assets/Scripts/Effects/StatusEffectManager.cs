using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurretTheFence.StatusEffect {

    public class StatusEffectManager : MonoBehaviour {

        public List<StatusEffect> statuses = new List<StatusEffect>();

        void Update() {
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

        void Apply(StatusEffect status) {
            statuses.Add(status);
        }

        void Refresh(string tag) {
            statuses.FindAll(x => x.tag == tag).ForEach(x => x.Refresh());
        }

    }

}