using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoPlayerManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string _videoUrl;

    public Image mask;
    public GameObject fullScreenObj;

    [SerializeField]
    private bool _isFullScreen = false;

    [SerializeField]
    private bool _isBigScreen = false;

    [SerializeField]
    private Button backButton;
    [SerializeField]
    private Button fullScreenButton;

    private void OnEnable()
    {
        _isBigScreen = true;
        _isFullScreen=true;
        backButton.gameObject.SetActive(false);
        fullScreenButton.gameObject.SetActive(false);

        videoPlayer = GetComponent<VideoPlayer>();
    }

    void VideoPlayerInit()
    {

    }

    public void PlayInBigScreen()
    {
        Debug.Log("PlayInBigScreen");

        switch (_isBigScreen)
        {
            case true:
                backButton.gameObject.SetActive(true);
                fullScreenButton.gameObject.SetActive(true);
                mask.enabled = false;
                _isBigScreen = false;
                break;
            case false:
                backButton.gameObject.SetActive(false);
                fullScreenButton.gameObject.SetActive(false);
                mask.enabled = true;
                _isBigScreen = true;
                break;
        }
    }
    public void PlayInFullScreen()
    {
        Debug.Log("PlayInFullScreen");
       

        switch (_isFullScreen)
        {
            case true:
                MainMenuCotroller.instance._cameraController.enabled = true;
                fullScreenObj.gameObject.SetActive(false);
                _isFullScreen = false;
                break;
            case false:
                MainMenuCotroller.instance._cameraController.enabled = false;
                fullScreenObj.gameObject.SetActive(true);
                _isFullScreen = true;
                break;
        }

    }
    public void LoadVideoAndPlay(string videoUrl)
    {
        //videoPlayer.Stop();
        _videoUrl = videoUrl;

        videoPlayer.url = _videoUrl;
       // videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
       // videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.Prepare();

        MainMenuCotroller.instance.CheckAppState(AppReadyState.IsVideoReady);



        //videoPlayer.Play();
    }
}
