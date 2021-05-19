
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

	public GameObject blood;

	void Awake ()
	{
		rb = GetComponent<Rigidbody2D> ();
		col = GetComponent<CircleCollider2D> ();
		this.spawn = this.transform.position;
		this.touchGround = true;
	}

	public void Push (Vector2 force)
	{
		rb.AddForce (force, ForceMode2D.Impulse);
		this.AddTry();
		
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
				GameObject.FindGameObjectWithTag("ui").GetComponent<ui>().NextLevel();
			}	
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
			GameObject.FindGameObjectWithTag("ui").GetComponent<ui>().ReloadLevel();
		}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Respawn" || collision.gameObject.tag == "Pic")
        {
			Instantiate(blood, new Vector3(this.transform.position.x, this.transform.position.y, 0), new Quaternion());
			this.DesactivateRb();
			this.transform.position = this.spawn;
			this.touchGround = true;
        }
		else if (collision.gameObject.tag == "cup")
        {
			this.InCup = true;
        }
        else
        {
			this.touchGround = true;
        }
	}

    private void OnCollisionExit2D(Collision2D collision)
    {
		if (collision.gameObject.tag == "cup")
		{
			this.InCup = false;
			this.timer = 0.0f;
		}
	}
}
