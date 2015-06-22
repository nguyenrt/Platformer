using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {
	public int speed;

	// Use this for initialization
	void Start () {
		var player = GameObject.Find ("Player");
		Vector3 velocity = rigidbody2D.velocity;
		
		if((int)player.transform.eulerAngles.y == 180)
			velocity.x = -speed;
		else
			velocity.x = speed;
			
		rigidbody2D.velocity = velocity;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnBecameInvisible() {
		Destroy(gameObject);
	}
	
	void OnCollisionEnter2D(Collision2D c) {
		Destroy(gameObject);
	}
}
