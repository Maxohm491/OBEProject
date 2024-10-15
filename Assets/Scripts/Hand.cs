using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    // Physics Movement
    [SerializeField] private GameObject followObject;
    [SerializeField] private Vector3 positionOffset;
    [SerializeField] private Vector3 rotationOffset;

    // Update is called once per frame
    void Update()
    {
        transform.position = followObject.transform.position; //.TransformPoint(positionoffset)
        transform.rotation = followObject.transform.rotation * Quaternion.Euler(rotationOffset);
    }
}
