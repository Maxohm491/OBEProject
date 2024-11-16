using UnityEngine;
using System.Collections.Generic;


public class RoomCollapse : MonoBehaviour
{
    [SerializeField] private List<FadeController> objectsToFade = new List<FadeController>();
    [SerializeField] private List<GameObject> objectsToDeactivate = new List<GameObject>();
    [SerializeField] private float fadeDuration = 2f;
    [SerializeField] private List<GameObject> objectsToMove;
    [SerializeField] private float targetY = -100f;

    private bool isFading = false;
    private bool hasFadedOut = false;
    private bool isMoving = false;
    private bool hasDeactivated = false;
    private float timer = 0f;
    private Dictionary<GameObject, float> startYPositions = new Dictionary<GameObject, float>();

    private void Start()
    {
        foreach (var obj in objectsToMove)
        {
            if (obj != null)
            {
                startYPositions[obj] = obj.transform.position.y;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !isFading && !hasFadedOut && !hasDeactivated)
        {
            StartFade();
            StartMovingObjects();
            DeactivateObjects();
        }

        if (isFading)
        {
            timer += Time.deltaTime;
            float progress = Mathf.Clamp01(timer / fadeDuration);
            float opacity = 1f - progress;

            foreach (FadeController fader in objectsToFade)
            {
                if (fader != null)
                {
                    fader.SetMaterialsToTransparent();
                    fader.SetOpacity(opacity);
                }
            }

            if (timer >= fadeDuration)
            {
                isFading = false;
                hasFadedOut = true;
            }
        }

        if (isMoving)
        {
            foreach (var obj in objectsToMove)
            {
                if (obj != null)
                {
                    float currentY = obj.transform.position.y;
                    float newY = Mathf.Lerp(currentY, targetY, Time.deltaTime * 0.1f);
                    obj.transform.position = new Vector3(obj.transform.position.x, newY, obj.transform.position.z);

                    if (Mathf.Abs(obj.transform.position.y - targetY) < 0.1f)
                    {
                        obj.transform.position = new Vector3(obj.transform.position.x, targetY, obj.transform.position.z);
                    }
                }
            }

            bool allObjectsAtTarget = true;
            foreach (var obj in objectsToMove)
            {
                if (obj != null && Mathf.Abs(obj.transform.position.y - targetY) > 0.1f)
                {
                    allObjectsAtTarget = false;
                    break;
                }
            }

            if (allObjectsAtTarget)
            {
                isMoving = false;
            }
        }
    }

    public void StartFade()
    {
        timer = 0f;
        isFading = true;
    }

    public void StartMovingObjects()
    {
        isMoving = true;
    }
    private void DeactivateObjects()
    {
        foreach (var obj in objectsToDeactivate)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }

        hasDeactivated = true;
    }
}
