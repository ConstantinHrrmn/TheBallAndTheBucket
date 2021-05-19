
using UnityEngine;

public class GameManager : MonoBehaviour
{
	#region Singleton class: GameManager

	public static GameManager Instance;

	void Awake ()
	{
		if (Instance == null) {
			Instance = this;
		}
	}

	#endregion

	Camera cam;

	public Ball ball;
	public Trajectory trajectory;
	[SerializeField] float basePushForce = 4f;
	[SerializeField] float pushForce = 4f;
	

	bool isDragging = false;

	Vector2 startPoint;
	Vector2 endPoint;
	Vector2 direction;
	Vector2 force;
	float distance;

	//---------------------------------------
	void Start ()
	{
		cam = Camera.main;
		ball.DesactivateRb ();
	}

	void Update ()
	{
		//Debug.Log(this.ball.gameObject.GetComponent<Ball>().touchGround);

        if (this.ball.gameObject.GetComponent<Ball>().touchGround)
        {
			if (Input.GetMouseButtonDown(0))
			{
				isDragging = true;
				OnDragStart();
			}
			if (Input.GetMouseButtonUp(0))
			{
				isDragging = false;
				OnDragEnd();
				this.ball.gameObject.GetComponent<Ball>().touchGround = false;
				
			}

			if (isDragging)
			{
				
				OnDrag();

			}
		}
		
	}

	//-Drag--------------------------------------
	void OnDragStart ()
	{
		ball.DesactivateRb ();
		startPoint = cam.ScreenToWorldPoint (Input.mousePosition);

		trajectory.Show ();
	}

	void OnDrag ()
	{
		endPoint = cam.ScreenToWorldPoint (Input.mousePosition);
		distance = Vector2.Distance (startPoint, endPoint);
		direction = (startPoint - endPoint).normalized;
		force = direction * distance * pushForce;

		trajectory.UpdateDots (ball.pos, force);
	}

	void OnDragEnd ()
	{

		this.pushForce = this.basePushForce;

		ball.ActivateRb ();

		ball.Push (force);

		trajectory.Hide ();
	}

	public void SpeedUp()
    {
		this.pushForce = 15f;
    }

}
