using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private bool avatarControl = false;
    private float timeUntilOBE = 0;
    private float timeUntilFade = 0;

    [SerializeField] private Button startButton, plusOBEButton, minusOBEButton, plusFadeButton, minusFadeButton;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI fadeTimeText;
    [SerializeField] private Toggle avatarControlToggle;


    // Start is called before the first frame update
    void Start()
    {
        plusOBEButton.onClick.AddListener(PlusButtonClicked);
        minusOBEButton.onClick.AddListener(MinusButtonClicked);
        PlusButtonClicked(); // Start at 30 sec

        plusFadeButton.onClick.AddListener(PlusFadeButtonClicked);
        minusFadeButton.onClick.AddListener(MinusFadeButtonClicked);
        PlusFadeButtonClicked();

        startButton.onClick.AddListener(StartButtonClicked);

        avatarControlToggle.onValueChanged.AddListener(AvatarControlChecked);
    }

    void AvatarControlChecked(bool newValue) {
        avatarControl = newValue;
    }

    void PlusButtonClicked() {
        timeUntilOBE += 30;
        timeText.text = ((int)timeUntilOBE / 60).ToString() + ":" + (timeUntilOBE % 60 == 0 ? "00" : "30"); 
    }

    void MinusButtonClicked() {
        if(timeUntilOBE == 0) {
            return;
        }

        timeUntilOBE -= 30;
        timeText.text = ((int)timeUntilOBE / 60).ToString() + ":" + (timeUntilOBE % 60 == 0 ? "00" : "30"); 
    }

    void PlusFadeButtonClicked() {
        timeUntilFade += 30;
        fadeTimeText.text = ((int)timeUntilFade / 60).ToString() + ":" + (timeUntilFade % 60 == 0 ? "00" : "30"); 
    }

    void MinusFadeButtonClicked() {
        if(timeUntilFade == 0) {
            return;
        }

        timeUntilFade -= 30;
        fadeTimeText.text = ((int)timeUntilFade / 60).ToString() + ":" + (timeUntilFade % 60 == 0 ? "00" : "30"); 
    }

    void StartButtonClicked() {
        Debug.Log("Starting");
        SettingsManager.Instance.controlInOBE = avatarControl;
        SettingsManager.Instance.timeUntilOBE = timeUntilOBE;
        SettingsManager.Instance.timeOBEtoFade = timeUntilFade;

        SceneManager.LoadScene("MainScene");
    }
}
