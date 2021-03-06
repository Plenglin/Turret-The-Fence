﻿using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace CompleteProject
{
    public class EnemyHealth : MonoBehaviour
    {

        public const float BURN_DELAY = 0.25f;

        public int startingHealth = 100;            // The amount of health the enemy starts the game with.
        public int currentHealth;                   // The current health the enemy has.
        public float sinkSpeed = 2.5f;              // The speed at which the enemy sinks through the floor when dead.
        public int scoreValue = 10;                 // The amount added to the player's score when the enemy dies.
        public AudioClip deathClip;                 // The sound to play when the enemy dies.
        public float burning = 0;
        private float nextBurnTick = 0;

        public GameObject drop;

        public int money = 10;                      // Money to drop when dead


        Animator anim;                              // Reference to the animator.
        AudioSource enemyAudio;                     // Reference to the audio source.
        ParticleSystem hitParticles;                // Reference to the particle system that plays when the enemy is damaged.
        CapsuleCollider capsuleCollider;            // Reference to the capsule collider.
        public bool isDead;                                // Whether the enemy is dead.
        bool isSinking;  
		public bool IsSpawner = false;// Whether the enemy has started sinking through the floor.
        private bool addedMoney = false;
        public ParticleSystem fireParticles;

        void Start() {
            StartCoroutine("Burn");
        }

        void Awake ()
        {
            // Setting up the references.
            anim = GetComponent <Animator> ();
			if(!anim && !IsSpawner)anim = GetComponentInChildren<Animator> ();
            enemyAudio = GetComponent <AudioSource> ();
            hitParticles = GetComponentInChildren <ParticleSystem> ();
            capsuleCollider = GetComponent <CapsuleCollider> ();

            // Setting the current health when the enemy first spawns.
            currentHealth = startingHealth;
        }


        void Update ()
        {
            if (burning > 0 && Time.time >= nextBurnTick) {
                TakeDamage(2, transform.position);
                nextBurnTick = Time.time + BURN_DELAY;
                burning -= BURN_DELAY;
                fireParticles.Play();
            }

            // If the enemy should be sinking...
            if (isSinking)
            {
                // ... move the enemy down by the sinkSpeed per second.
                transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
            }
        }

        IEnumerable Burn() {
            while (true) {
                Debug.Log(burning);
                if (burning >= 0) {
                    TakeDamage(2, transform.position);
                    Debug.Log("fdssnig");
                }
                yield return new WaitForSeconds(0.75f);
            }
        }


        public void TakeDamage (int amount, Vector3 hitPoint)
        {
            // If the enemy is dead...
            if(isDead)
                // ... no need to take damage so exit the function.
                return;

            // Play the hurt sound effect.
            enemyAudio.Play ();

            // Reduce the current health by the amount of damage sustained.
            currentHealth -= amount;
            
            // Set the position of the particle system to where the hit was sustained.
            hitParticles.transform.position = hitPoint;

            // And play the particles.
            hitParticles.Play();

            // If the current health is less than or equal to zero...
            if(currentHealth <= 0)
            {
                // ... the enemy is dead.
                Death ();
            }
        }


        void Death ()
        {
            if (!addedMoney) {
                addedMoney = true;
                GameObject loot = Instantiate(drop);
                loot.GetComponent<MoneyDrop>().money = this.money;
                loot.transform.position = transform.position;
            }
            // The enemy is dead.
            isDead = true;

            // Turn the collider into a trigger so shots can pass through it.
            capsuleCollider.isTrigger = true;

            // Tell the animator that the enemy is dead.
            if(anim && !IsSpawner)anim.SetTrigger ("Dead");

            // Change the audio clip of the audio source to the death clip and play it (this will stop the hurt clip playing).
            enemyAudio.clip = deathClip;
            enemyAudio.Play ();
			StartSinking ();
        }


        public void StartSinking ()
        {
			//Debug.Log("StartSinking");
            // Find and disable the Nav Mesh Agent.
			if (!IsSpawner)	GetComponent <NavMeshAgent> ().enabled = false;
	
            // Find the rigidbody component and make it kinematic (since we use Translate to sink the enemy).
            GetComponent <Rigidbody> ().isKinematic = true;

            // The enemy should no sink.
            isSinking = true;

            // Increase the score by the enemy's score value.
			//Debug.Log("Making it to score");
			ScoreManager.score += scoreValue;

            // After 2 seconds destory the enemy.
            Destroy (gameObject, 2f);
        }
    }
}