using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Hands;

public class ControlDisabler : MonoBehaviour
{
    [SerializeField] private TrackedPoseDriver mainCameraMover;
    [SerializeField] private XRHandSkeletonDriver leftHandMover, rightHandMover;

    public void Disable() {
        Debug.Log("Disable");
        mainCameraMover.enabled = false;
        leftHandMover.enabled = false;
        rightHandMover.enabled = false;
    }

    public void Enable() {
        Debug.Log("Enabled");
        mainCameraMover.enabled = true;
        leftHandMover.enabled = true;
        rightHandMover.enabled = true;
    }
}
