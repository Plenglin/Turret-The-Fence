using UnityEngine;
using CompleteProject;

namespace TurretTheFence.Effect
{
    public class BurningEffect : TickingStatusEffect
    {

        public const float tickRate = 0.1f;

        private GameObject burningEffect, burningEffectBase;
        private int damagePerTick;
        private float nextTick;
        private EnemyHealth health;

        public BurningEffect(int damagePerTick, float duration, GameObject burningEffectBase) : base("burning", tickRate, duration) {
            this.damagePerTick = damagePerTick;
            this.burningEffectBase = burningEffect;
        }

        protected override void OnStart() {
            health = receiver.GetComponent<EnemyHealth>();
            burningEffect = GameObject.Instantiate(burningEffectBase);
            burningEffect.transform.parent = receiver.transform;
        }

        protected override void OnTick() {
            health.TakeDamage(damagePerTick, receiver.transform.position);
            burningEffect.GetComponent<ParticleSystem>().Play();
        }

        protected override void OnEnd() {
            GameObject.Destroy(burningEffect);
        }
        
    }
}