using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefManager : MonoBehaviour
{
    public void ResetGame()
    {
        ResetScore();
        ResetScene();
        ResetTimestamp();
    }
    public void ResetScore()
    {
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.Save();
    }
    public void IncreaseScore(int amount)
    {
        int currentScore = PlayerPrefs.GetInt("Score");
        int newScore = currentScore + amount;
        PlayerPrefs.SetInt("Score", newScore);
        PlayerPrefs.Save();

        Debug.Log("PlayerPref Score: " + PlayerPrefs.GetInt("Score"));
    }
    public int GetScore()
    {
        return PlayerPrefs.GetInt("Score");
    }
    public void ResetScene()
    {
        SetScene("Intro");
    }
    public void SetScene(string scene)
    {
        PlayerPrefs.SetString("Scene", scene);
        PlayerPrefs.Save();
    }
    public string GetScene()
    {
        return PlayerPrefs.GetString("Scene");
    }
    public void ResetTimestamp()
    {
        SetTimestamp(0);
    }
    public void SetTimestamp(int timestamp)
    {
        PlayerPrefs.SetInt("Timestamp", timestamp);
        PlayerPrefs.Save();
    }
    public int GetTimestamp()
    {
        return PlayerPrefs.GetInt("Timestamp");
    }
    public void SetQuizMode(bool quizMode)
    {
        PlayerPrefs.SetInt("quizMode", quizMode ? 1 : 0);
    }
    public void ToggleQuizMode()
    {
        SetQuizMode(!isQuizMode());
    }
    public bool isQuizMode()
    {
        return PlayerPrefs.GetInt("quizMode") == 1;
    }

    public void SetVolume(float value)
    {
        PlayerPrefs.SetFloat("Volume", value);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return PlayerPrefs.GetFloat("Volume");
    }

    public void SetInitialMode(int value) {
        PlayerPrefs.SetInt("InitialMode", value);
    }

    public int GetInitialMode() {
        return PlayerPrefs.GetInt("InitialMode");
    }
}
