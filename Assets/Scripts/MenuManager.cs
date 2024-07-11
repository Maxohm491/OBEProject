using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private bool isMale = true;
    private bool avatarControl = false;
    private float timeUntilOBE = 0;

    [SerializeField] private Button maleButton, femaleButton, startButton, plusButton, minusButton;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private Toggle avatarControlToggle;


    // Start is called before the first frame update
    void Start()
    {
        femaleButton.interactable = false;

        maleButton.onClick.AddListener(ToggleGender);
        femaleButton.onClick.AddListener(ToggleGender);

        plusButton.onClick.AddListener(PlusButtonClicked);
        minusButton.onClick.AddListener(MinusButtonClicked);

        startButton.onClick.AddListener(StartButtonClicked);

        avatarControlToggle.onValueChanged.AddListener(AvatarControlChecked);
    }

    void AvatarControlChecked(bool newValue) {
        avatarControl = newValue;
    }

    void PlusButtonClicked() {
        timeUntilOBE += 30;
        timeText.text = ((int)timeUntilOBE / 60).ToString() + ":" + (timeUntilOBE % 60).ToString(); 
    }

    void MinusButtonClicked() {
        if(timeUntilOBE == 0) {
            return;
        }

        timeUntilOBE -= 30;
        timeText.text = ((int)timeUntilOBE / 60).ToString() + ":" + (timeUntilOBE % 60).ToString(); 
    }

    void StartButtonClicked() {
        Debug.Log("Starting");
    }

    void ToggleGender() {
        if (isMale) {
            isMale = false;
            maleButton.interactable = true;
            femaleButton.interactable = false;
        }
        else {
            isMale = true;
            maleButton.interactable = false;
            femaleButton.interactable = true;
        }
    }
}
