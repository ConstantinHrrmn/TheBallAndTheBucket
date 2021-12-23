using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager_multi : Photon.MonoBehaviour
{

	Camera cam;

	public ball_multi ball;
	public Trajectory trajectory;
	[SerializeField] float basePushForce = 4f;
	[SerializeField] float pushForce = 4f;

	public PhotonView photonView;
	public Text PlayerName;

	bool isDragging = false;

	Vector2 startPoint;
	Vector2 endPoint;
	Vector2 direction;
	Vector2 force;
	float distance;

	//---------------------------------------
	void Start()
	{
		cam = Camera.main;

		this.trajectory = this.gameObject.transform.Find("Trajectory").GetComponent<Trajectory>();

		ball.DesactivateRb();


	}

	void Update()
	{

        if (photonView.isMine)
        {
			this.CheckInput();
        }
	}

	private void CheckInput()
    {
		//Debug.Log(this.ball.gameObject.GetComponent<Ball>().touchGround);
		if (this.GetComponent<ball_multi>().touchGround)
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
				this.GetComponent<ball_multi>().touchGround = false;

			}

			if (isDragging)
			{

				OnDrag();

			}
		}
	}

	//-Drag--------------------------------------
	void OnDragStart()
	{
		ball.DesactivateRb();
		startPoint = cam.ScreenToWorldPoint(Input.mousePosition);

		trajectory.Show();
	}

	public void NewLevel()
	{
		this.cam = Camera.main;

	}

	void OnDrag()
	{
		endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
		distance = Vector2.Distance(startPoint, endPoint);
		direction = (startPoint - endPoint).normalized;
		force = direction * distance * pushForce;

		trajectory.UpdateDots(ball.pos, force);
	}

	void OnDragEnd()
	{

		this.pushForce = this.basePushForce;

		ball.ActivateRb();

		ball.Push(force);

		trajectory.Hide();
	}

	public void SpeedUp()
	{
		this.pushForce = 15f;
	}

	public void SlowDown()
	{
		this.pushForce = 1f;
	}
}
