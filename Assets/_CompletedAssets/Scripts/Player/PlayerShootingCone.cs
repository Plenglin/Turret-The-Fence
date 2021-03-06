using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;
using System.Collections;
using System.Collections.Generic;

namespace CompleteProject
{
    public class PlayerShootingCone : MonoBehaviour
    {
        public int damagePerShot = 20;                  // The damage inflicted by each bullet.
        public float timeBetweenBullets = 0.15f;        // The time between each shot.
       // public float range = 100f;                      // The distance the gun can fire.




        float timer;                                    // A timer to determine when to fire.
        //Ray shootRay;                                   // A ray from the gun end forwards.
      //  RaycastHit shootHit;                            // A raycast hit to get information about what was hit.
        int shootableMask;                              // A layer mask so the raycast only hits things on the shootable layer.
        ParticleSystem gunParticles;                    // Reference to the particle system.
       // LineRenderer gunLine;                           // Reference to the line renderer.
        AudioSource gunAudio;                           // Reference to the audio source.
        Light gunLight;                                 // Reference to the light component.
		public Light faceLight;								// Duh
        public float effectsDisplayTime = 0.2f;                // The proportion of the timeBetweenBullets that the effects will display for.
		public List<Collider> TriggerList = new List<Collider>();

        void Awake ()
        {
            // Create a layer mask for the Shootable layer.
            shootableMask = LayerMask.GetMask ("Shootable");

            // Set up the references.
            gunParticles = GetComponent<ParticleSystem> ();
            //gunLine = GetComponent <LineRenderer> ();
            gunAudio = GetComponent<AudioSource> ();
            gunLight = GetComponent<Light> ();
			//faceLight = GetComponentInChildren<Light> ();
			//GetComponent<CapsuleCollider>().enabled = false;
			//StartCoroutine(initializeCollider());



        }
		IEnumerator initializeCollider()
		{
			yield return null;
			GetComponent<CapsuleCollider>().enabled = true;
			yield return null;
		}
		void OnTriggerEnter(Collider other)
		{
			if ((shootableMask == (shootableMask | (1 << other.gameObject.layer))) && !TriggerList.Contains(other))
			{
				TriggerList.Add(other);
			}
		}

		void OnTriggerExit(Collider other)
		{
			if (shootableMask == (shootableMask | (1 << other.gameObject.layer)))
			{
				if (TriggerList.Contains(other))
				{
					TriggerList.Remove(other);
				}
			}
		}

        void Update ()
        {
            // Add the time since Update was last called to the timer.
            timer += Time.deltaTime;

#if !MOBILE_INPUT
            // If the Fire1 button is being press and it's time to fire...
			if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
            {
                // ... shoot the gun.
                Shoot ();
            }
#else
            // If there is input on the shoot direction stick and it's time to fire...
            if ((CrossPlatformInputManager.GetAxisRaw("Mouse X") != 0 || CrossPlatformInputManager.GetAxisRaw("Mouse Y") != 0) && timer >= timeBetweenBullets)
            {
                // ... shoot the gun
                Shoot();
            }
#endif
            // If the timer has exceeded the proportion of timeBetweenBullets that the effects should be displayed for...
            if(timer >= timeBetweenBullets * effectsDisplayTime)
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
            gunLight.enabled = false;
        }


        void Shoot ()
        {
            // Reset the timer.
            timer = 0f;

            // Play the gun shot audioclip.
            gunAudio.Play ();

            // Enable the lights.
            gunLight.enabled = true;
			faceLight.enabled = true;

            // Stop the particles from playing if they were, then start the particles.
            gunParticles.Stop ();
            gunParticles.Play ();

			foreach (Collider enemy in TriggerList){  
                // Try and find an EnemyHealth script on the gameobject hit.
                EnemyHealth enemyHealth = enemy.GetComponent <EnemyHealth> ();

                // If the EnemyHealth component exist...
	                if(enemyHealth != null)
	                {
	                    // ... the enemy should take damage.
					enemyHealth.TakeDamage (damagePerShot, enemy.transform.position);
	                }

                // Set the second position of the line renderer to the point the raycast hit.
				//gunLine.SetPosition (1, shootHit.point);
				}
            
            // If the raycast didn't hit anything on the shootable layer...
           
        }
    }
}