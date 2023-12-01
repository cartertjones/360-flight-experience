using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseMenuWithTimer : PauseMenu
{
    public Slider videoSlider;
    public VideoPlayerManager vpm;

    public TextMeshProUGUI currentTime;
    public TextMeshProUGUI videoTime;

    public override void Start() {
        base.Start();
    }
    public void Update() {
        videoSlider.value = (float)(vpm.GetTimestamp()/vpm.GetLength());
        currentTime.text = FormatTime((int)vpm.GetTimestamp());
        videoTime.text = FormatTime((int)vpm.GetLength());
    }

    public void SkipBack(int amount) {
        vpm.SetTimestamp(vpm.GetTimestamp() - amount);
    }

    public void SkipForward(int amount) {
        vpm.SetTimestamp(vpm.GetTimestamp() + amount);
    }

    string FormatTime(int totalSeconds)
    {
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;

        string formattedTime = $"{minutes:D2}:{seconds:D2}";

        return formattedTime;
    }
}
