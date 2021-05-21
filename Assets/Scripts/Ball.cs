﻿
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
	[HideInInspector] public Rigidbody2D rb;
	[HideInInspector] public CircleCollider2D col;

	[HideInInspector] public Vector3 pos { get { return transform.position; } }

	[HideInInspector] public bool touchGround = true;

	private Vector3 spawn;
	private float timer = 0.0f;
	private bool InCup = false;
	private Color BaseColor;

	public GameObject blood;

	GameObject cup = null;

	

	void Awake ()
	{
		this.BaseColor = this.gameObject.GetComponent<SpriteRenderer>().color;
		rb = GetComponent<Rigidbody2D> ();
		col = GetComponent<CircleCollider2D> ();
		this.spawn = this.transform.position;
		this.touchGround = true;
	}

	public void Push (Vector2 force)
	{
		rb.AddForce (force, ForceMode2D.Impulse);
		this.AddTry();
		this.ChangeColor(this.BaseColor);
		
	}

	public void AddTry()
    {
		if (GameObject.FindGameObjectWithTag("ui") != null)
		{
			GameObject.FindGameObjectWithTag("ui").GetComponent<ui>().AddTry();
		}
	}

	public void ActivateRb ()
	{
		rb.isKinematic = false;
	}

	public void DesactivateRb ()
	{
		rb.velocity = Vector3.zero;
		rb.angularVelocity = 0f;
		rb.isKinematic = true;
	}

    private void Update()
    {
        if (this.InCup)
        {
            
			this.timer += Time.deltaTime;
            if (this.timer >= 2 )
            {
				if (this.cup == null)
				{
					GameObject.FindGameObjectWithTag("ui").GetComponent<ui>().NextLevel();
				}
                else
                {
					this.cup.GetComponent<cupLevelManager>().Choosen();
                }
				
			}
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
			GameObject.FindGameObjectWithTag("ui").GetComponent<ui>().ReloadLevel();
		}
    }

	private void Respawn()
    {
		this.DesactivateRb();
		this.transform.position = this.spawn;
		this.touchGround = true;
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
		string s = collision.gameObject.tag;

		Color c;
		try { 
			c = collision.gameObject.GetComponent<SpriteRenderer>().color;
		}
        catch{
			c = this.BaseColor;
        }
		

		if (s == "Respawn" || s == "Pic")
        {
			Instantiate(blood, new Vector3(this.transform.position.x, this.transform.position.y, 0), new Quaternion());
			this.Respawn();
        }
		else if (s == "cup")
        {
			this.cup = null;
			this.InCup = true;
        }
		else if (s == "cupLevel")
        {
			this.cup = collision.gameObject;
			this.InCup = true;
        }
		else if (s == "cupHome")
		{
			this.Respawn();
			GameObject.FindGameObjectWithTag("ui").GetComponent<ui>().GoBack();
		}
		else if (s == "speed")
        {
			this.ChangeColor(c);
			GameObject.Find("GameManager").GetComponent<GameManager>().SpeedUp();
		}
		else if (s == "slow")
		{
			this.ChangeColor(c);
			GameObject.Find("GameManager").GetComponent<GameManager>().SlowDown();
		}
		else if (s == "rotation")
        {
			this.ChangeColor(c);
			GameObject.FindGameObjectWithTag("ui").GetComponent<ui>().ChangeRotation();
		}
		else if (s == "gravity")
		{
			this.ChangeColor(c);
			this.gameObject.GetComponent<Rigidbody2D>().gravityScale *= -1;
		}
		else if (s == "cupgravity")
		{
			this.ChangeColor(c);
			GameObject.FindGameObjectWithTag("cup").GetComponent<Rigidbody2D>().gravityScale *= -1;
		}
		else if(s == "star")
        {
			this.ChangeColor(c);
			this.touchGround = true;
			GameObject.Find("Deletable Walls").GetComponent<DeletableWallManager>().Activate();
		}
		else if(s == "portal_in" || s == "portal_out")
        {
			this.ChangeColor(c);
		}
		else
        {
			this.touchGround = true;
		}

		
	}

    private void OnCollisionExit2D(Collision2D collision)
    {
		if (collision.gameObject.tag == "cup" || collision.gameObject.tag == "cupLevel")
		{
			this.InCup = false;
			this.timer = 0.0f;
		}
	}

	public void ChangeColor(Color c)
    {
		this.gameObject.GetComponent<SpriteRenderer>().color = c;
    }
}
