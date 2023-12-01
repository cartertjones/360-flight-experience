using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private PlayerPrefManager ppm;
    private CustomSceneManager sm;

    public GameObject settingsMenu;
    public GameObject resumeGame;

    private void Start()
    {
        ppm = GameObject.Find("PlayerPrefManager").GetComponent<PlayerPrefManager>();
        sm = GameObject.Find("CustomSceneManager").GetComponent<CustomSceneManager>();
    }
    private void Update() {
        if(ppm.GetTimestamp() > 0 && (ppm.GetInitialMode() == 1 && ppm.isQuizMode())) {
            resumeGame.SetActive(true);
        }
        else {
            resumeGame.SetActive(false);
        }
    }
    public void NewGame()
    {
        ppm.ResetGame();
        ppm.SetInitialMode(ppm.isQuizMode() ? 1 : 0);
        sm.LoadScene(1, "Intro");
    }

    public void ResumeGame()
    {
        //scenechanger changescene to playerpref scene
        int sceneIndex = 0;
        string ppmScene = ppm.GetScene();
        switch(ppmScene) {
            case "Intro":
                sceneIndex = 1;
                break;
            case "360Video":
                sceneIndex = 2;
                break;
            case "Conclusion":
                sceneIndex = 1;
                break;
        }
        sm.LoadScene(sceneIndex);
    }

    public void Settings()
    {
        settingsMenu.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void Quit()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }
    
}
