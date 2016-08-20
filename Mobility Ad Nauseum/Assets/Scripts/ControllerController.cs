/* Created August 6, 2016 by Daniel Cox
 * Updated 8/6/2016
 * Purpose: Controller for the Controller, to Control the Controller. */
using UnityEngine;
using System.Collections;

public class ControllerController : MonoBehaviour {
	//Private variables

	bool toolsVSWeps = false;

	bool hookOut = false;

	float thrustAccel = 1f;

	float fireTimer = 0f;

	float healthTimer = 0f;

	//Serialized Variables
	[SerializeField]
	GameObject fullRig;
	[SerializeField]
	float health = 100f;
	[SerializeField]
	float maxHealth = 100f;
	[SerializeField]
	GameObject curTarget;

	//Weapon State Machine
	enum WeaponStates{
		SHOTGUN, ROCKET, SNIPER, AUTO,
		THRUSTER, HOOKSHOT, MINE, BOXING,
		NONE
	}
	WeaponStates curWeapon;

	//Fire: Large projectile, damage gradients from 100% to 0% based on distance, pushes user back.
	void ShotgunFire() {
		if (fireTimer == 0) {
			fireTimer = 1.5f;
			//Play firing animation
			//Fire projectile
			fullRig.GetComponent<Rigidbody> ().AddForce (-10 * transform.forward);
		}
	}
	//Fire: Exploding Missile, 50% damage AoE, pushes user back and blasts target stronger.
	void RocketFire() {
		if (fireTimer == 0) {
			fireTimer = 2f;
			//Play firing animation
			//Fire projectile
			fullRig.GetComponent<Rigidbody> ().AddForce (-10 * transform.forward);
		}
	}
	//Fire: Small Projectile, 33% damage, 100% on headshot, dampens current velocity.
	void SniperFire() {
		if (fireTimer == 0) {
			fireTimer = 1.5f;
			//Play firing animation
			//Fire projectile
			fullRig.GetComponent<Rigidbody> ().velocity.Scale (new Vector3 (.5f, .5f, .5f));
			if (fullRig.GetComponent<Rigidbody> ().velocity.magnitude < 1f) {
				fullRig.GetComponent<Rigidbody> ().velocity.Set (0, 0, 0);
			}
		}
	}
	//Fire: Small Projectiles, Constant stream, 100% damage in 2 seconds, thrusts backward.
	void AutoFire() {
		if (fireTimer == 0) {
			fireTimer = .1f;
			//Play firing animation
			//Fire projectile
			fullRig.GetComponent<Rigidbody> ().AddForce (-1 * transform.forward);
		}
	}
	//Fire: Thrusts forward.
	void ThrusterFire() {
		if (fireTimer == 0) {
			fireTimer = .1f;
			//Play firing animation
			fullRig.GetComponent<Rigidbody> ().AddForce (transform.forward);
		}
	}
	//Fire: Sends hook forward, or pulls it back. Detaches if unsuccessful. Sends target and user towards each other.
	void HookshotFire() {
		if (hookOut) {
			//Play retract animation
			if (curTarget != null) { //AddForce(MoveTowards(opposite participant)) twice;
				curTarget.GetComponent<Rigidbody> ().AddForce (Vector3.MoveTowards(curTarget.transform.position,transform.position,5f));
				fullRig.GetComponent<Rigidbody> ().AddForce (Vector3.MoveTowards(fullRig.transform.position,curTarget.transform.position,5f));
			} 
			else {
				SetWeaponState (WeaponStates.NONE);
			}
		}
	}
	//Fire: Plants air or landmine. 100% AoE on contact with enemy.
	void MineFire() {
		
	}
	//Fire: Grabs onto target. Releasing gives target and user velocity equal to movement of controller.
	void BoxingFire() {
		
	}
	//Switch states
	void SetWeaponState(WeaponStates state) {
		curWeapon = state;
	}

	//Start is called as soon as the scene is finished loading
	void Start() {
		
	}

	//OnTriggerEnter is called on collision
	void OnTriggerEnter(Collider other) {
		
	}
	
	//Update is called once per frame
	void Update() {
		
	}
}
