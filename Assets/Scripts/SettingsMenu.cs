using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    private CustomSceneManager sm;

    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject pauseMenu;

    private PlayerPrefManager ppm;

    private Slider slider;
    private Toggle quizToggle;

    private void Start()
    {
        sm = GameObject.Find("CustomSceneManager").GetComponent<CustomSceneManager>();
        ppm = GameObject.Find("PlayerPrefManager").GetComponent<PlayerPrefManager>();

        quizToggle = GetComponentInChildren<Toggle>();
        if(quizToggle != null) {
            if(ppm.isQuizMode()) {
                quizToggle.isOn = true;
            }
            else {
                quizToggle.isOn = false;
            }

            if(sm.GetActiveScene() == 1) {
                quizToggle.interactable = false;
            }
        }
        
        slider = GetComponentInChildren<Slider>();
        slider.value = ppm.GetVolume();
        Debug.Log("PlayerPref Volume: " + ppm.GetVolume() + ", slider value: " + slider.value);
    }
    public void Back() 
    {
        if(sm.GetActiveScene() == 0) {
            if(mainMenu != null) {
                mainMenu.SetActive(true);
                this.gameObject.SetActive(false);
            }   
        }
        else {
            if(pauseMenu != null){
                pauseMenu.SetActive(true);
                this.gameObject.SetActive(false);
            }
        }
    }

    public void VolumeChanged()
    {
        ppm.SetVolume(slider.value);
        Debug.Log("Slider value: " + slider.value);
    }

    public void QuizModeToggled()
    {
        ppm.ToggleQuizMode();
    }
}
