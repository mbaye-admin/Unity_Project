using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.Video;
public class UIVideoManager : MonoBehaviour
{
    [SerializeField]
    [Range(0f, 1f)]
    private float volume;
    [SerializeField]
    private Button playButton;
    [SerializeField]
    private Button pauseButton;

    private void Start()
    {
        //InitVideoStream();

        playButton.interactable = false;
        pauseButton.interactable = false;
    }

    public void InitVideoStream()
    {
        volume = 1f;
        playButton.interactable = true;
        pauseButton.interactable = true;
        playButton.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(true);
    }

    public void Play_PauseVideo(string playSts)
    {
        switch (playSts)
        {
            case "play":

                //Debug.Log();

                if (!MainMenuCotroller.instance._videoPlayerManager.videoPlayer.isPlaying)
                {
                    playButton.gameObject.SetActive(false);
                    pauseButton.gameObject.SetActive(true);
                    MainMenuCotroller.instance._videoPlayerManager.videoPlayer.Play();
                }
                break;

            case "pause":
                if (MainMenuCotroller.instance._videoPlayerManager.videoPlayer.isPlaying)
                {
                    playButton.gameObject.SetActive(true);
                    pauseButton.gameObject.SetActive(false);
                    MainMenuCotroller.instance._videoPlayerManager.videoPlayer.Pause();
                }
                break;
        }
    }
    public void VoulumeUp()
    {
        if (volume < 0.9f)
        {
            volume += 0.1f;

        }
        else
        {
            volume = 1f;
        }
        //Debug.Log("volume + : " + volume);

        MainMenuCotroller.instance._videoPlayerManager.videoPlayer.SetDirectAudioVolume(0, Mathf.Clamp(volume, 0, 1f));
    }
    public void VolumeDown()
    {
        if (volume > 0.2f)
        {
            volume -= 0.1f;

        }
        else
        {
            volume = 0f;
        }
        //Debug.Log("volume - : " + volume);

        MainMenuCotroller.instance._videoPlayerManager.videoPlayer.SetDirectAudioVolume(0, Mathf.Clamp(volume, 0, 1f));
    }
}
