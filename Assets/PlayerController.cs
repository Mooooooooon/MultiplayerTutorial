using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

	public GameObject bulletPrefab;
	public Transform bulletSpawn;

	
	void Update()
	{
		if (!isLocalPlayer) {
			return;
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			CmdFire ();
		}

		var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
		var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

		if (Input.GetKey (KeyCode.A)) {
			Debug.Log ("A");
			transform.Rotate (0, 150.0f * Time.deltaTime, 0);
		}
		if (Input.GetKey (KeyCode.D)) {
			Debug.Log ("A");
			transform.Rotate (0, -150.0f * Time.deltaTime, 0);
		}
		transform.Rotate(0, 5, 0);
		transform.Translate(0, 0, z);
	}




	[Command]
	void CmdFire()
	{
		// Create the Bullet from the Bullet Prefab
		var bullet = (GameObject)Instantiate(
			bulletPrefab,
			bulletSpawn.position,
			bulletSpawn.rotation);

		// Add velocity to the bullet
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

		// Spawn the bullet on the Clients
		NetworkServer.Spawn(bullet);

		// Destroy the bullet after 2 seconds
		Destroy(bullet, 2.0f);
	}
	public override void OnStartLocalPlayer(){
		GetComponent<MeshRenderer> ().material.color = Color.blue;
	}
}
