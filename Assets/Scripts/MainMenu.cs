using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private PlayerPrefManager ppm;

    private void Start()
    {
        ppm = GameObject.Find("PlayerPrefManager").GetComponent<PlayerPrefManager>();
    }
    public void NewGame()
    {
        ppm.ResetGame();
    }

    public void ResumeGame()
    {
        //scenechanger changescene to playerpref scene
        //then pass saved timestamp to videoplayer
    }
    
}
