using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PauseMenu : MonoBehaviour
{
    public GameObject settingsMenu;
    private CustomSceneManager sm;
    [SerializeField]
    private PlayerPauseController ppc;
    
    public virtual void Start()
    {
        sm = GameObject.Find("CustomSceneManager").GetComponent<CustomSceneManager>();
    }

    public virtual void Resume()
    {
        ppc.ToggleUIPosition();
    }
    public virtual void Settings()
    {
        if(settingsMenu != null) {
            settingsMenu.SetActive(true);
            this.gameObject.SetActive(false);
            Debug.Log("activated settings menu");
        }  
    }
    public virtual void SkipSection() {
        sm.LoadScene(sm.GetActiveScene() + 1, "Scene");
    }
    public virtual void RestartSection() {
        sm.LoadScene(sm.GetActiveScene(), "Scene");
    }
    public virtual void MainMenu()
    {
        sm.LoadScene(0);
    }
}
