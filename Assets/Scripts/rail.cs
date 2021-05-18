using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rail : MonoBehaviour {

	float x;
	float y;
	float minX;
	float maxX;
	public float speed;
	// Use this for initialization
	void Start () {
		if (speed == 0) {
			speed = Random.Range (0.07f, 0.1f);
		}
		minX = transform.parent.position.x;
		maxX = minX + transform.parent.GetComponent<SpriteRenderer> ().bounds.size.x / 2;

		y = transform.parent.position.y;
		x = minX;
		this.transform.position = new Vector2 (minX, y);
		minX = minX - transform.parent.GetComponent<SpriteRenderer> ().bounds.size.x / 2;
	}
	
	// Update is called once per frame
	void Update () {
		if (x <= minX) {
			speed = speed * -1;
		}
		if (x >= maxX) {
			speed = speed *  -1;
		}
		this.transform.Rotate (0,0,2);
		x += speed * Time.deltaTime;
		this.transform.position = new Vector2 (x, y);
	}
}
