using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canvasRotation : MonoBehaviour
{
    private Quaternion OrgRotation;
    private Vector3 OrgPosition;
 
    void Start()
    {
        OrgRotation = transform.rotation;
        OrgPosition = transform.parent.transform.position - transform.position;
    }

    void LateUpdate()
    {
        transform.rotation = OrgRotation;
        transform.position = transform.parent.position - OrgPosition;
    }
}
