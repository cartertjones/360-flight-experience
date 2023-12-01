using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;    //Must be using UnityEngine.Video
using UnityEngine;

public class VideoPlayerManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;     //Our video player component variable. Can be set in inspector or through code in Start()             
    public VideoClip[] clips;

    private int currTimestamp, prevTimestamp;

    // [SerializeField]
    // private int videoLength;

    [SerializeField]
    private QuizController qc;

    [SerializeField]
    private PlayerPrefManager ppm;

    [SerializeField]
    private CustomSceneManager sm;

    private void Start() {
        //select clip to be played
        if(ppm.GetScene() == "Intro")
        {
            videoPlayer.clip = clips[0];
            Play();
        }
        if(ppm.GetScene() == "360Video")
        {
            videoPlayer.clip = clips[0]; //stupid
            Play();
        }
        else if(ppm.GetScene() == "Conclusion")
        {
            videoPlayer.clip = clips[1];
            Play();
        }
        
    }

    private bool isPaused;


    private void Update()
    {
        // if(videoLength != null || videoLength != 0) {
        //     if(ppm.GetTimestamp() == videoLength) {
        //         if(ppm.GetScene() == "360Video") {
        //             sm.LoadScene(1, "Conclusion");
        //         }
        //         else if(ppm.GetScene() == "Conclusion") {
        //             //show end ui?
        //         }
        //     }
        // }

        if(ppm.GetScene() != "Intro" && ppm.GetTimestamp() >= videoPlayer.length) {
            switch(ppm.GetScene()) {
                case "360Video":
                    sm.LoadScene(1, "Conclusion");
                    break;
                case "Conclusion":
                    sm.LoadScene(3, "EndScreen");
                    break;
            }
        }

        //Update timestamp
        currTimestamp = (int)(videoPlayer.time);
        if(currTimestamp != prevTimestamp)
        {
            if(qc != null)
            {
                qc.UpdateTimestamp(currTimestamp);
            }
            ppm.SetTimestamp(currTimestamp);
            Debug.Log(ppm.GetTimestamp());

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
    }
    public void TogglePlay() {

    }

    public int GetLength() {
        return (int)videoPlayer.length;
    }

    //TODO call QuizController.UpdateTimestamp each second, passing video's timestamp as parameter
   
}



