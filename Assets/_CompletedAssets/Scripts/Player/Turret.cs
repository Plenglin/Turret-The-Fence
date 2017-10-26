using UnityEngine;
using System.Collections;
namespace CompleteProject
{
public class Turret : MonoBehaviour {

	public float DistanceFromCastle,CoolDown;
	public GameObject enemy;
	public GameObject Bullet;
	public int protectionRadius,bulletSpeed;
	public float SPAWN_DISTANCE = .5f;
	public float Counter = 0f;

	// Use this for initialization
	void Start () 
	{


	}

	// Update is called once per frame
	void Update () {

			Counter += Time.deltaTime;
			enemy = FindClosestEnemy ();

		if(enemy != null)
		{
			DistanceFromCastle = Vector3.Distance(enemy.transform.position,gameObject.transform.position);
			transform.LookAt (enemy.transform);
			//transform.eulerAngles = new Vector3(0,transform.eulerAngles.y,0);
			//print (DistanceFromCastle);			

		}
			attackEnemy ();
	}
	void attackEnemy()
	{
			if (DistanceFromCastle <= protectionRadius && (enemy != null)) {
				
				Counter += Time.deltaTime;
				if (Counter >= CoolDown ) {
					EnemyHealth enemyHealth = enemy.GetComponent <EnemyHealth> ();
					if (enemyHealth.currentHealth > 0) {
						Debug.DrawLine (transform.position, enemy.transform.position, Color.red);

						GameObject bullet = Instantiate (Bullet, transform.position + SPAWN_DISTANCE * transform.forward, transform.rotation) as GameObject;

						bullet.GetComponent<Rigidbody> ().AddForce (transform.forward * bulletSpeed);

						print ("attack Enemy");
						Counter = 0;
					}
				}
			}

	}
		public GameObject FindClosestEnemy()
		{
			GameObject[] gos;
			gos = GameObject.FindGameObjectsWithTag("Enemy");
			GameObject closest = null;
			float distance = Mathf.Infinity;
			Vector3 position = transform.position;
			foreach (GameObject go in gos)
			{
				Vector3 diff = go.transform.position - position;
				float curDistance = diff.sqrMagnitude;
				if (curDistance < distance)
				{
					closest = go;
					distance = curDistance;
				}
			}
			return closest;
		}
}
}