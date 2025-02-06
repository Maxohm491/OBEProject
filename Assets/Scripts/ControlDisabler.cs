using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Hands;

public class ControlDisabler : MonoBehaviour
{
    [SerializeField] private TrackedPoseDriver mainCameraMover;
    [SerializeField] private XRHandSkeletonDriver leftHandMover, rightHandMover;
    [SerializeField] private GameObject leftHandIKTarget, rightHandIKTarget;
    [SerializeField] private Transform leftRestPose, rightRestPose;

    public void Disable() {
        mainCameraMover.enabled = false;
        leftHandMover.enabled = false;
        rightHandMover.enabled = false;
        leftHandIKTarget.transform.position = leftRestPose.position;
        rightHandIKTarget.transform.position = rightRestPose.position;
    }

    public void Enable() {
        mainCameraMover.enabled = true;
        leftHandMover.enabled = true;
        rightHandMover.enabled = true;
    }
}
