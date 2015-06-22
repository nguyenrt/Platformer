using UnityEngine;
using System.Collections;

public class AvatarControls : MonoBehaviour {
	private const int MAXHEALTH = 6;
	private const int FACINGLEFT = 180;
	private const int FACINGRIGHT = 0;
	
	public float speed;
	public GameObject bullet;
	public static int health;
	public float camSpeed = 1.0f;
	
	private bool isHit = false;
	private bool isShooting = false;
	private bool isBoosting = false;
	private bool isJumping = false;
	private GameObject mainCamera;
	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		animator.SetInteger("health", health);
		mainCamera = (GameObject)GameObject.FindWithTag("MainCamera");
		health = MAXHEALTH;	
	}
	
	// Update is called once per frame
	void Update () {
		animator.SetFloat("speed", Mathf.Abs(Input.GetAxisRaw("Horizontal")));
		animator.SetFloat("velocity", Mathf.Abs(rigidbody2D.velocity.y * 10));
		animator.SetInteger("health", health);
	
		// Restart level
		if(Input.GetKeyDown(KeyCode.Q))
			Application.LoadLevel(Application.loadedLevel);
		
		// Player is dead
		if(health == 0) {
			gameObject.layer = LayerMask.NameToLayer("Enemy");
			return;
		}
		
		// Camera follows player
		//if(transform.position.x >= 0)
		//	mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, mainCamera.transform.position.z);
		mainCamera.transform.position = Vector3.Lerp (mainCamera.transform.position, 
													  new Vector3(transform.position.x, transform.position.y, mainCamera.transform.position.z), 
													  camSpeed * Time.deltaTime);
		
	
		// Jumping
		if(Input.GetKeyDown(KeyCode.Space))
			if(!isJumping) {
				rigidbody2D.AddForce(new Vector2(0, 2.0f), ForceMode2D.Impulse);
				isJumping = true;
			}
		
		if(isJumping && !isBoosting)
			if(Input.GetKeyDown(KeyCode.A)) {
				float force = 2.0f;
			
				if((int)transform.eulerAngles.y == FACINGLEFT)
					rigidbody2D.AddForce(new Vector2(-force, 0), ForceMode2D.Impulse);
				else
					rigidbody2D.AddForce(new Vector2(force, 0), ForceMode2D.Impulse);
			
				isBoosting = true;		
			}
		
		if(!isShooting)
			if(Input.GetKeyDown(KeyCode.E))
				StartCoroutine(PlayerIsShooting());
		
		// Moving Right
		if(Input.GetAxisRaw("Horizontal") > 0) {
			transform.Translate(Vector3.right * speed * Time.deltaTime);
			transform.transform.eulerAngles = new Vector2(0, FACINGRIGHT);
			transform.GetChild(0).transform.eulerAngles = new Vector2(0, 0);
		}
			
		// Moving Left
		if(Input.GetAxisRaw("Horizontal") < 0) {
			transform.Translate(Vector3.right * speed * Time.deltaTime);
			transform.transform.eulerAngles = new Vector2(0, FACINGLEFT);
			transform.GetChild(0).transform.eulerAngles = new Vector2(0, 0);
		}	
	}
	
	void OnCollisionEnter2D(Collision2D c) {
		switch(c.gameObject.tag) {
		case "Ground":
			isJumping = false;
			isBoosting = false;
			break;
		case "Enemy":
			if(!isHit)
				StartCoroutine(BlinkPlayer(c));
				
			break;
		case "Elevator":
			transform.parent = GameObject.FindWithTag("Elevator").transform;
			break;
		}

	}
	
	// Player will be frozen while shooting
	IEnumerator PlayerIsShooting() {
		isShooting = true;
		GameObject  b;
		animator.SetTrigger("shoot");
		
		if((int)transform.eulerAngles.y == FACINGLEFT)
			b = (GameObject)Instantiate(bullet, transform.position - new Vector3(0.15f, 0, 0), Quaternion.identity);
		else
			b = (GameObject)Instantiate(bullet, transform.position + new Vector3(0.15f, 0, 0), Quaternion.identity);
			
		b.transform.eulerAngles = transform.eulerAngles;
		animator.SetTrigger("shoot");
		yield return new WaitForSeconds(0.1f);
		isShooting = false;
	}
	
	// During this time, player is invulnerable
	IEnumerator BlinkPlayer(Collision2D c) {			
		health--;
		isHit = true;
		float force = 1.0f;
			
		if(transform.position.x < c.transform.position.x)
			force *= -1;
			
		rigidbody2D.AddForce(new Vector2(force, 0), ForceMode2D.Impulse);
		gameObject.layer = LayerMask.NameToLayer("Enemy");
			
		for(int i = 0; i < 12; i++) {
			renderer.enabled = !renderer.enabled;
			yield return new WaitForSeconds(0.1f);
		}
		
		gameObject.layer = LayerMask.NameToLayer("Default");
		renderer.enabled = true;
		isHit = false;
	}
}
