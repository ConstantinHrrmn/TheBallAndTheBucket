using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletableWallManager : MonoBehaviour
{
    private bool activated;
    private float timer;
    public float limit;

    private Color baseColor;
    private Color ActivatedColor;

    // Start is called before the first frame update
    void Start()
    {
        this.Reset();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.activated)
        {
            this.timer += Time.deltaTime;
            if (this.timer > this.limit)
            {
                this.Reset();
            }
            else
            {
                this.SetChildsCollider(false);
                this.SetChildsColor(true);
            }
        }
    }

    public void SetChildsCollider(bool status)
    {
        int count = transform.childCount;
        for (int i = 0; i < count; i++)
        {
            Transform child = transform.GetChild(i);
            child.gameObject.GetComponent<BoxCollider2D>().enabled = status;
        }
    }

    public void SetChildsColor(bool random)
    {
        int count = transform.childCount;
        for (int i = 0; i < count; i++)
        {
            Transform child = transform.GetChild(i);
            Color c = child.gameObject.GetComponent<SpriteRenderer>().color;
            if (random)
            {
                c = this.ActivatedColor;
            }
            else
            {
                c = this.baseColor;

            }
            child.gameObject.GetComponent<SpriteRenderer>().color = c;

        }
    }





    public void Activate()
    {
        this.activated = true;
        Debug.LogWarning("ACTIVATED");
    }

    public void Reset()
    {
        this.activated = false;
        this.timer = 0f;
        this.baseColor = new Color(222, 223, 225, 1);
        this.ActivatedColor = new Color(255, 255, 47, 0.3f);

        this.SetChildsCollider(true);
        this.SetChildsColor(false);
    }
}
