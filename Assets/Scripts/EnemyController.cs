using UnityEngine;
using System.Collections;
//using Pathfinding;

//[RequireComponent (typeof(Rigidbody2D))]
//[RequireComponent (typeof (Seeker))]
public class EnemyController : MonoBehaviour {
	public int life;
	public float speed, leftEndPoint, rightEndPoint;
	
	void Start() {
	
	}
	
	void Update() {
		if(transform.position.x <= leftEndPoint)
			transform.transform.eulerAngles = new Vector2(0, 180);
		else if(transform.position.x >= rightEndPoint)
			transform.transform.eulerAngles = new Vector2(0, 0);

		transform.Translate(Vector3.left * speed * Time.deltaTime);
	}
	
	void OnCollisionEnter2D(Collision2D c) {
		if(c.gameObject.tag == "Attack")
			life--;
		
		if(life < 1)
			Destroy(this.gameObject);
	}
	
	/***********USE FOR A* PATHFINDING*********/
	/*public float updateRate = 2f;
	
	// Target to chase
	public Transform target;
	
	// Calculated path
	public Path path;
	
	// Enemy's speed
	public float speed = 300f;
	public ForceMode2D fMode;
	
	// Max distance from enemy to waypoint until it moves onto next waypoint
	public float nextWayPointDistance = 3;
	
	[HideInInspector]
	public bool pathIsEnded = false;

	// Caching
	private Seeker seeker;
	private Rigidbody2D rb;
	
	// Waypoint that enemy is moving towards
	private int currentWaypoint = 0;

	// Use this for initialization
	void Start () {
		seeker = GetComponent<Seeker>();
		rb = GetComponent<Rigidbody2D>();
		
		if(target == null) {
			Debug.LogError("No Player Found");
			return;
		}
		
		seeker.StartPath(transform.position, target.position, OnPathComplete);
		
		StartCoroutine(UpdatePath());
	}
	
	public void OnPathComplete(Path p){
		Debug.Log("We got a path " + p.error);
		
		if(!p.error) {
			path = p;
			currentWaypoint = 0;
		}
	}
	
	IEnumerator UpdatePath() {
		if(target == null) {
			// TODO: Insert player search here
			return false;
		}
		
		seeker.StartPath(transform.position, target.position, OnPathComplete);
		
		yield return new WaitForSeconds(1f / updateRate);
		StartCoroutine(UpdatePath ());
	}
	
	void FixedUpdate() {
		if(target == null) {
			// TODO: Insert player search here
			return;
		}
		
		// TODO: Always look at player
		
		if(path == null)
			return;
			
		if(currentWaypoint >= path.vectorPath.Count) {
			if(pathIsEnded)
				return;
				
			Debug.Log("End of path reached");
			pathIsEnded = true;
			return;
		}
		
		pathIsEnded = false;
		
		// Direction to the next waypoint
		Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
		dir *= speed * Time.fixedDeltaTime;
		
		// Move the enemy
		rb.AddForce(dir, fMode);
		
		if(Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]) < nextWayPointDistance) {
			currentWaypoint++;
			return;
		}
	}*/
	

}
