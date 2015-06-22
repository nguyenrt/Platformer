using UnityEngine;
using System.Collections;

public class CameraControls : MonoBehaviour {
	public float MoveDistance;
	public float leftLimit = 0;
	public float rightLimit = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
	}
}
