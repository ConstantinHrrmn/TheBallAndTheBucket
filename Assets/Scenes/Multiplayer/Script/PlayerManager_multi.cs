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

	private PlayerData pd;
	private int animate = 0;

	//---------------------------------------
	void Start()
	{
		cam = Camera.main;

		this.trajectory = this.gameObject.transform.Find("Trajectory").GetComponent<Trajectory>();
		ball.DesactivateRb();

        if (photonView.isMine)
        {
			// Setting player data
			this.pd = this.gameObject.GetComponent<PlayerData>();

			this.pd.SetColor();

			this.gameObject.GetComponent<SpriteRenderer>().color = this.pd.color;

			this.pd.NewLevel();

			this.ball.ChangeBaseColor(this.pd.color);

			this.PlayerName.text = PhotonNetwork.player.NickName;

		}
        else
        {
			this.PlayerName.text = photonView.owner.NickName;
		}
		
		//this.photonView.RPC("RPC_SendColor", PhotonTargets.AllBuffered);
	}

	void Update()
	{
		//Vector3 color = new Vector3(this.pd.color.r, this.pd.color.g, this.pd.color.b);
		//this.photonView.RPC("RPC_SendColor", PhotonTargets.AllBuffered, color);

		if (photonView.isMine)
        {
			

			this.pd.InstantiatePlayerList();
			this.CheckInput();

            if (this.ball.waiting)
            {
				this.pd.FinishedLevel();
				//this.ball.col.enabled = false;
				//this.ball.DesactivateRb();
				this.TurnOff();
				if (this.animate == 0)
					this.animate = 1;
            }

            if (this.animate == 1 && cam.transform.position.y > -8)
            {
				cam.transform.Translate(new Vector3(0, -2, 0) * Time.deltaTime);
            }


		}

		
	}

	private void TurnOff()
    {

        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {
			Destroy(this.gameObject.transform.GetChild(i).gameObject);
		}

		Destroy(this.gameObject.GetComponent<SpriteRenderer>());
		Destroy(this.gameObject.GetComponent<SpriteRenderer>());
		Destroy(this.gameObject.GetComponent<Rigidbody2D>());

		this.transform.position = new Vector3(-1000, -1000, 0);
		
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

		this.pd.DidAShot();

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

	[PunRPC]
	private void RPC_SendColor()
	{
		this.gameObject.GetComponent<SpriteRenderer>().color = this.pd.color;
	}

}
