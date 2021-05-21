using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingWall : MonoBehaviour
{

    public float maxDistance;
    public float speed;

    private float min_x;
    private float max_x;

    private float min_y;
    private float max_y;

    private float timeout = 1f;

    public bool MoveX;

    private float x;
    private float y;

    // Start is called before the first frame update
    void Start()
    {
        this.min_x = this.transform.position.x;
        this.max_x = this.transform.position.x + maxDistance;

        this.min_y = this.transform.position.y;
        this.max_y = this.transform.position.y + maxDistance;

        this.x = 0;
        this.y = 0;
    }

    // Update is called once per frame
    void Update()
    {
            if (this.MoveX)
            {
                this.x += Time.deltaTime * this.speed;
                transform.position = new Vector3(Mathf.PingPong(this.x, max_x - min_x) + min_x, transform.position.y, transform.position.z);
            }
            else
            {
                this.y += Time.deltaTime * this.speed;
                transform.position = new Vector3(transform.position.x, Mathf.PingPong(this.y, max_y - min_y) + min_y, transform.position.z);
            }
    }

    void InvertDirection()
    {
        this.maxDistance *= -1;
    }
}
