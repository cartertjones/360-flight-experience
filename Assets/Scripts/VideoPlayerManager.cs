using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;    //Must be using UnityEngine.Video
using UnityEngine;

public class VideoPlayerManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;     //Our video player component variable. Can be set in inspector or through code in Start()             
    public VideoClip[] clips;

    private int currTimestamp, prevTimestamp;

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
        }
        else if(ppm.GetScene() == "Conclusion")
        {
            videoPlayer.clip = clips[1];
        }
        Play();
    }

    private bool isPaused;


    private void Update()
    {
        //Check to see if the current clip has ended. Once it's over, prepare to transition to 360 video
        if ((videoPlayer.frame) > 0 && (videoPlayer.isPlaying == false))
        {
            sm.LoadScene(2, "360VideoTakeoff");
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
            Debug.Log(currTimestamp);
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

    //TODO call QuizController.UpdateTimestamp each second, passing video's timestamp as parameter
   
}



