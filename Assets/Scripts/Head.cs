using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    [SerializeField] private Transform rootObject, followObject, hips;
    [SerializeField] private Vector3 positionOffset, rotationOffset;

    [SerializeField] private Vector3 headBodyOffset;

    // Start is called before the first frame update
    void Start()
    {
        headBodyOffset = hips.position - transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        rootObject.position = transform.position + headBodyOffset;
        rootObject.forward = Vector3.ProjectOnPlane(followObject.forward, Vector3.up).normalized;

        transform.position = followObject.position; //.TransformPoint(positionoffset)
        transform.rotation = followObject.rotation * Quaternion.Euler(rotationOffset);

        
    }
}
