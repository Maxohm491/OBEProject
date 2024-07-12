using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class OBEManager : MonoBehaviour
{
    [SerializeField] private InputCoordinator coordinator;
    [SerializeField] private Camera headSetCamera;
    [SerializeField] private GameObject OBECameraObject;
    [SerializeField] private Transform finalOBEPosition;
    [SerializeField] private float moveSeconds = 1f;
    [SerializeField] private ControlDisabler disabler;
    [SerializeField] private TextMeshProUGUI countdownText;

    private OBEState state;
    private Camera OBECamera; 
    private float timeToOBE = 30f;
    private bool controlInOBE = true;
    private Vector3 initialPosition;

    public enum OBEState {
        Before,
        Moving,
        DoneMoving
    }

    void Start()
    {
        OBECamera = OBECameraObject.GetComponent<Camera>();

        headSetCamera.enabled = true;
        OBECamera.enabled = false;
        coordinator.primaryButtonPress.AddListener(OnPrimaryButtonEvent);

        state = OBEState.Before;

        initialPosition = headSetCamera.gameObject.transform.position;

        try {
            timeToOBE = SettingsManager.Instance.timeUntilOBE;
            controlInOBE = SettingsManager.Instance.controlInOBE;
        }
        catch {
            Debug.LogWarning("No settings found; initialized to default value");
        }
    }

    void Update() {
        if (timeToOBE > 0 && state == OBEState.Before) {
            timeToOBE -= Time.deltaTime;

            int seconds = (int) (timeToOBE % 60);
            countdownText.text = ((int)timeToOBE / 60).ToString() + ":" + (seconds < 10 ? "0" + seconds.ToString() : seconds.ToString());
        }
        else {
            OnPrimaryButtonEvent(true);
        }
    }


    public void OnPrimaryButtonEvent(bool pressed)
    {
        if(state == OBEState.Before && pressed) {
            state = OBEState.Moving;

            headSetCamera.enabled = false;
            OBECamera.enabled = true;

            StartCoroutine(DoOBE());
        }
    }

    IEnumerator DoOBE() {
        if (!controlInOBE) {
            disabler.Disable();
        }

        yield return StartCoroutine(MoveCamera());

        Debug.Log("Done moving");
        state = OBEState.DoneMoving;
    }

    IEnumerator MoveCamera() {
        float timeSinceStarted = 0f;
        initialPosition = headSetCamera.gameObject.transform.position;

        while (timeSinceStarted < moveSeconds)
        {
            timeSinceStarted += Time.deltaTime;
            OBECameraObject.transform.position = Vector3.Lerp(initialPosition, finalOBEPosition.position, Mathf.SmoothStep(0f, 1f, timeSinceStarted / moveSeconds));

            yield return null;
        }
    }
}
