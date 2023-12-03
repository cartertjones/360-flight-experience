using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Developer
{
    [MenuItem("Developer/Clear PlayerPrefs")]
    public static void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("All saved preferences have been cleared.");
    }

    //-----------------------------------------------------------

    [MenuItem("Developer/SetScene/MainMenu")]
    public static void SetSceneToMainMenu()
    {
        CustomSceneManager sm = GameObject.Find("CustomSceneManager").GetComponent<CustomSceneManager>();
        sm.LoadScene(0, "MainMenu");
    }

    [MenuItem("Developer/SetScene/Intro")]
    public static void SetSceneToIntro()
    {
        CustomSceneManager sm = GameObject.Find("CustomSceneManager").GetComponent<CustomSceneManager>();
        sm.LoadScene(1, "Intro");
    }

    [MenuItem("Developer/SetScene/360Video")]
    public static void SetSceneTo360Video()
    {
        CustomSceneManager sm = GameObject.Find("CustomSceneManager").GetComponent<CustomSceneManager>();
        sm.LoadScene(2, "360Video");
    }

    [MenuItem("Developer/SetScene/Conclusion")]
    public static void SetSceneToConclusion()
    {
        CustomSceneManager sm = GameObject.Find("CustomSceneManager").GetComponent<CustomSceneManager>();
        sm.LoadScene(3, "Conclusion");
    }

    [MenuItem("Developer/SetScene/EndScreen")]
    public static void SetSceneToEndScreen()
    {
        CustomSceneManager sm = GameObject.Find("CustomSceneManager").GetComponent<CustomSceneManager>();
        sm.LoadScene(4, "EndScreen");
    }

    //-----------------------------------------------------------

    [MenuItem("Developer/SetTimestamp/Beginning")]
    public static void SetTimestampBeginning()
    {
        VideoPlayerManager vpm = GameObject.Find("VideoManager").GetComponent<VideoPlayerManager>();
        vpm.SetTimestamp(0);
    }

    [MenuItem("Developer/SetTimestamp/Half")]
    public static void SetTimestampHalf()
    {
        VideoPlayerManager vpm = GameObject.Find("VideoManager").GetComponent<VideoPlayerManager>();
        vpm.SetTimestamp(vpm.GetLength() / 2);
    }

    [MenuItem("Developer/SetTimestamp/End")]
    public static void SetTimestampEnd()
    {
        VideoPlayerManager vpm = GameObject.Find("VideoManager").GetComponent<VideoPlayerManager>();
        vpm.SetTimestamp(vpm.GetLength() - 1);
    }
}
