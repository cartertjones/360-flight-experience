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

}
