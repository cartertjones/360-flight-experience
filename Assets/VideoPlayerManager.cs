using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;    //Must be using UnityEngine.Video
using UnityEngine;

public class VideoPlayerManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;     //Our video player component variable. Can be set in inspector or through code in Start()             
    public VideoClip[] clips;           //Array for video clips


    private void Update()
    {
        //Check to see if the current clip has ended. Once it's over, call to PlayClip()
        if ((videoPlayer.frame) > 0 && (videoPlayer.isPlaying == false))
        {
            PlayClip();
        }
    }

    public void PlayClip()
    {
        if (videoPlayer.clip == clips[0])            //Checks to see if the clip that just played was the first video
        {
            videoPlayer.clip = clips[1];     //Sets the videoPlayer clip to the next chosen video
        }
        else
        {
            videoPlayer.clip = clips[0];     //Sets the videoPlayer clip to the next chosen video
        }
        
    }

   
}



