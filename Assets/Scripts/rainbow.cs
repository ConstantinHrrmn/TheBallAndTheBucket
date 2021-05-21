using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rainbow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.HSVToRGB(.34f, .84f, .67f);
    }

    // Update is called once per frame
    void Update()
    {
        // Assign HSV values to float h, s & v. (Since material.color is stored in RGB)
        float h, s, v;
        Color.RGBToHSV(this.gameObject.GetComponent<SpriteRenderer>().color, out h, out s, out v);

        // Use HSV values to increase H in HSVToRGB. It looks like putting a value greater than 1 will round % 1 it
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.HSVToRGB(h + Time.deltaTime * .25f, s, v);
    }


}
