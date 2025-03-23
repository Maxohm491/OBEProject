using System;
using System.Collections;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.XR.Interaction.Toolkit;


public class OBEManager : MonoBehaviour
{
    [SerializeField] private Camera headSetCamera;
    [SerializeField] private GameObject OBECameraObject;
    [SerializeField] private Renderer fadeRenderer;
    [SerializeField] private Transform finalOBEPosition;
    [SerializeField] private float moveSeconds = 1f;
    [SerializeField] private float headRadius = 0.25f;
    [SerializeField] private float tunnelVisionSoftness = 0.5f;
    [SerializeField] private ControlDisabler disabler;
    [SerializeField] private RoomCollapse fader;
    [SerializeField] private TextMeshProUGUI countdownText;

    private OBEState state;
    private Camera OBECamera; 
    private float timeToOBE = 60f;
    private float timeOBEtoFade = 60f;
    private bool controlInOBE = false;
    private Vector3 initialPosition;

    public enum OBEState {
        Before,
        Moving,
        DoneMoving,
        Fading
    }

    void Start()
    {
        // volume.profile.TryGetSettings(out vignette);

        OBECamera = OBECameraObject.GetComponent<Camera>();

        headSetCamera.enabled = true;
        OBECamera.enabled = false;

        state = OBEState.Before;

        initialPosition = headSetCamera.gameObject.transform.position;

        try {
            timeToOBE = SettingsManager.Instance.timeUntilOBE;
            timeOBEtoFade = SettingsManager.Instance.timeOBEtoFade;
            controlInOBE = SettingsManager.Instance.controlInOBE;
        }
        catch {
            Debug.LogWarning("No settings found; initialized to default values");
        }
    }

    void Update() {
        if (timeToOBE > 0 && state == OBEState.Before) {
            timeToOBE -= Time.deltaTime;

            int seconds = (int) (timeToOBE % 60);
            countdownText.text = ((int)timeToOBE / 60).ToString() + ":" + (seconds < 10 ? "0" + seconds.ToString() : seconds.ToString());
        }
        else if (timeToOBE <= 0 && state == OBEState.Before) {
            countdownText.text = "";

            OnPrimaryButtonEvent(true);
        }
        else if (timeOBEtoFade > 0 && state == OBEState.DoneMoving) {
            timeOBEtoFade -= Time.deltaTime;
        }
        else if (timeOBEtoFade <= 0 && state == OBEState.DoneMoving) {
            state = OBEState.Fading;
            fader.StartFade();
            Debug.Log("fading");
        }
    }


    public void OnPrimaryButtonEvent(bool pressed)
    {
        if(state == OBEState.Before && pressed) {
            state = OBEState.Moving;

            StartCoroutine(DoOBE());
        }
    }

    IEnumerator DoOBE() {
        // Offset OBE camera outside of skull
        Vector3 backwardsUnit = Vector3.Normalize(new Vector3(-headSetCamera.transform.forward.x, 0, -headSetCamera.transform.forward.z));

        OBECamera.gameObject.transform.position = headSetCamera.transform.position + (backwardsUnit * headRadius);

        headSetCamera.enabled = false;
        OBECamera.enabled = true;

        if (!controlInOBE) {
            disabler.Disable();
        }   

        fadeRenderer.material.SetFloat("_FeatheringEffect", tunnelVisionSoftness);

        yield return StartCoroutine(MoveCamera());

        state = OBEState.DoneMoving;
    }

    IEnumerator MoveCamera() {
        float timeSinceStarted = 0f;
        initialPosition = OBECamera.gameObject.transform.position;
        
        while (timeSinceStarted < moveSeconds)
        {
            timeSinceStarted += Time.deltaTime;
            fadeRenderer.material.SetFloat("_ApertureSize", Math.Abs(1.0f - (timeSinceStarted/moveSeconds)));
            OBECameraObject.transform.position = Vector3.Lerp(initialPosition, finalOBEPosition.position, Mathf.SmoothStep(0f, 1f, timeSinceStarted / moveSeconds));

            yield return null;
        }
    }
}
