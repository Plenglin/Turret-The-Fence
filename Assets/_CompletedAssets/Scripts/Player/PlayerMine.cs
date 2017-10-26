using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CompleteProject

{
public class PlayerMine : MonoBehaviour {
	public int damage = 100;
	public 	int damageTimes = 1;
	ParticleSystem gunParticles;                    // Reference to the particle system.
	// LineRenderer gunLine;                           // Reference to the line renderer.
	AudioSource gunAudio;                           // Reference to the audio source.
	public int shootableMask;                              // A layer mask so the raycast only hits things on the shootable layer.
	public Light faceLight;								// Duh
	public float effectsDisplayTime =.3f;                // The proportion of the timeBetweenBullets that the effects will display for.
	float timer; 
		// Use this for initialization

	
	void Start () {
			gunParticles = GetComponent<ParticleSystem> ();
			//gunLine = GetComponent <LineRenderer> ();
			gunAudio = GetComponent<AudioSource> ();
			shootableMask = LayerMask.GetMask ("Shootable");

	}
	
	// Update is called once per frame
	void Update () {
			timer += Time.deltaTime;
			if(timer >=  effectsDisplayTime)
			{
				// ... disable the effects.
				DisableEffects ();
			}
	}


		public void DisableEffects ()
		{
			// Disable the line renderer and the light.
			//gunLine.enabled = false;
			faceLight.enabled = false;

			if (damageTimes <= 0) {
				Destroy (gameObject.transform.root.gameObject);
			}

		}



	void OnTriggerEnter(Collider enemy)
	{
			// Reset the timer.
			if (shootableMask == (shootableMask | (1 << enemy.gameObject.layer))){
			timer = 0f;

			// Play the gun shot audioclip.
			gunAudio.Play ();

			// Enable the lights.

			faceLight.enabled = true;

			// Stop the particles from playing if they were, then start the particles.
			gunParticles.Stop ();
			gunParticles.Play ();
			EnemyHealth enemyHealth = enemy.GetComponent <EnemyHealth> ();
			// If the EnemyHealth component exist...
			if(enemyHealth != null)
			{
				// ... the enemy should take damage.
				enemyHealth.TakeDamage (damage, enemy.transform.position);
			}
				damageTimes--;
			
			}

	}
}
}