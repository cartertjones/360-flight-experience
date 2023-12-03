using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;    //Must be using UnityEngine.Video
using UnityEngine;

public class VideoPlayerManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;     //Our video player component variable. Can be set in inspector or through code in Start()             
    public VideoClip[] clips;

    private double currTimestamp, prevTimestamp;
    private bool videoStarted;

    // [SerializeField]
    // private int videoLength;

    [SerializeField]
    private QuizController qc;

    [SerializeField]
    private PlayerPrefManager ppm;

    [SerializeField]
    private CustomSceneManager sm;
    
    private bool isPaused;

    public bool videoEnded;

    private void Start() {
        videoPlayer.clip = clips[0];
        videoEnded = false;
    }

    private void Update()
    {
        if(videoPlayer.isPrepared && videoPlayer.time >= videoPlayer.length - 0.5)
        {
            if(!videoEnded)
            {
                videoEnded = true;
            }
            
            if(videoEnded && sm.GetActiveScene() == 2)
            {
                Debug.Log("Current Scene is 360Video, loading Conclusion.");
                sm.LoadScene(3, "Conclusion");
            }
        }
        //Update timestamp
        currTimestamp = videoPlayer.time;
        if(currTimestamp != prevTimestamp)
        {
            if(qc != null)
            {
                qc.UpdateTimestamp(currTimestamp);
            }
            ppm.SetTimestamp((int)(currTimestamp));
        }
        prevTimestamp = currTimestamp;

        //update volume
        videoPlayer.SetDirectAudioVolume(0, ppm.GetVolume());
    }

    public void Pause() {
        videoPlayer.Pause();
    }
    public void Play() {
        videoPlayer.time = ppm.GetTimestamp();
        videoPlayer.Play();
        videoStarted = true;
    }

    public double GetLength() {
        return videoPlayer.length;
    }

    public double GetTimestamp() {
        return (double)ppm.GetTimestamp();
    }

    public void SetTimestamp(double timestamp) {
        videoPlayer.time = timestamp;
    }
}



