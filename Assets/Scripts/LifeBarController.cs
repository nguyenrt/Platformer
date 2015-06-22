using UnityEngine;
using System.Collections;

public class LifeBarController : MonoBehaviour {
	SpriteRenderer spriteRenderer;
	public Sprite[] LifeBar;
	
	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if(AvatarControls.health >= 0)
			spriteRenderer.sprite = LifeBar[AvatarControls.health];
	}
}
