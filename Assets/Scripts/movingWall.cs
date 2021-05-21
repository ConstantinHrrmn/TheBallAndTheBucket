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

    public bool MoveX;

    // Start is called before the first frame update
    void Start()
    {
        this.min_x = this.transform.position.x;
        this.max_x = this.transform.position.x + maxDistance;

        this.min_y = this.transform.position.y;
        this.max_y = this.transform.position.y + maxDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.MoveX)
        {
            transform.position = new Vector3(Mathf.PingPong(Time.time * this.speed, max_x - min_x) + min_x, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time * this.speed, max_y - min_y) + min_y, transform.position.z);
        }
    }

    void InvertDirection()
    {
        this.maxDistance *= -1;
    }
}
