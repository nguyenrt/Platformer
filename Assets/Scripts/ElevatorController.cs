using UnityEngine;
using System.Collections;

public class ElevatorController : MonoBehaviour {
	public float endPoint;
	public float speed;
	private bool isActive = false;
	private bool isReturning = false;
	private Vector3 originalPos;
	
	// Use this for initialization
	void Start () {
		originalPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(isActive) {
			transform.Translate(new Vector3(0, speed));
			
			if(transform.position.y > endPoint)
				StartCoroutine(ReturnToOriginalPos());
		}
		
		if(isReturning) {
			transform.Translate(new Vector3(0, -speed));
			
			if(originalPos == transform.position)
				isReturning = false;
		}
	}
	
	void OnCollisionEnter2D(Collision2D c) {
		if(c.gameObject.tag == "Player" && transform.position.y < endPoint) {
			isActive = true;
		}
	}
	
	IEnumerator ReturnToOriginalPos() {
		isActive = false;
		yield return new WaitForSeconds(2.0f);
		isReturning = true;
	}
}
