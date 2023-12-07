using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(ASyncLoader))]
public class CustomSceneManager : MonoBehaviour
{
    PlayerPrefManager ppm;
    ASyncLoader loader;
    FadeScreen fadeScreen;

    private void Start() {
        ppm = GameObject.Find("PlayerPrefManager").GetComponent<PlayerPrefManager>();
        fadeScreen = GameObject.Find("FadeScreen").GetComponent<FadeScreen>();
        loader = GetComponent<ASyncLoader>();
    }
    public void LoadScene(int sceneIndex) {
        fadeScreen.FadeOut(sceneIndex);
    }
    public void LoadScene(int sceneIndex, string sceneName) {
        Debug.Log("Loading Scene: " + sceneIndex + " (" + sceneName + ")");
        ppm.ResetTimestamp();
        ppm.SetScene(sceneName);
        fadeScreen.FadeOut(sceneIndex);
    }
    public int GetActiveScene() {
        Scene currentScene = SceneManager.GetActiveScene();
        Debug.Log(currentScene.buildIndex);
        return currentScene.buildIndex;
    }
}
