using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomSceneManager : MonoBehaviour
{
    PlayerPrefManager ppm;

    private void Start() {
        ppm = GameObject.Find("PlayerPrefManager").GetComponent<PlayerPrefManager>();
    }
    public void LoadScene(int sceneIndex) {
        SceneManager.LoadScene(sceneIndex);
    }
    public void LoadScene(int sceneIndex, string sceneName) {
        Debug.Log("Loading Scene: " + sceneIndex + " (" + sceneName + ")");
        ppm.ResetTimestamp();
        ppm.SetScene(sceneName);
        SceneManager.LoadScene(sceneIndex);
    }
    public int GetActiveScene() {
        Scene currentScene = SceneManager.GetActiveScene();
        Debug.Log(currentScene.buildIndex);
        return currentScene.buildIndex;
    }
}
