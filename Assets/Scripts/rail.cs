using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rail : MonoBehaviour {

	float x;
	float y;
	float min;
	float max;
	public float speed;
	public bool orientation;
	private bool touch;
	public bool startpos;

	// Use this for initialization
	void Start () {
		if (speed == 0) {
			speed = Random.Range (1f, 7f);
		}
		this.touch = false;

        if (this.orientation)
        {
			min = transform.parent.position.x;
			max = min + transform.parent.GetComponent<SpriteRenderer>().bounds.size.x / 2;

			y = transform.parent.position.y;

            if (this.startpos)
            {
				x = max;
            }
            else
            {
				x = min;
			}
			

			this.transform.position = new Vector2(min, y);

			min = min - transform.parent.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        }
        else
        {
			min = transform.parent.position.y;
			max = min + transform.parent.GetComponent<SpriteRenderer>().bounds.size.y / 2;

			x = transform.parent.position.x;

			if (this.startpos)
			{
				y = max;
			}
			else
			{
				y = min;
			}

			this.transform.position = new Vector2(x, min);

			min = min - transform.parent.GetComponent<SpriteRenderer>().bounds.size.y / 2;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
        if (this.orientation)
        {
            if (this.touch)
            {
				if (x < min)
				{
					speed *= -1;
					this.touch = false;
				}

            }
            else
            {
				if (x > max)
				{
					speed *= -1;
					this.touch = true;
				}

			}
			x += speed * Time.deltaTime;

		}
        else
        {
			if (this.touch)
			{
				if (y < min)
				{
					speed *= -1;
					this.touch = false;
				}

			}
			else
			{
				if (y > max)
				{
					speed *= -1;
					this.touch = true;
				}

			}

			y += speed * Time.deltaTime;
		}
		


		this.transform.Rotate (0,0,2);
		
		this.transform.position = new Vector2 (x, y);
	}
}
