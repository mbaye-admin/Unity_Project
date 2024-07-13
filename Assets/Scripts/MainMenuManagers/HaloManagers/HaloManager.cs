using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaloManager : MonoBehaviour
{

    public videoTag _videoTag;
    public VideoPlayerManager _videoPlayManger;
    private string _videoUrl;
    public void LoadHaloData(HomePageURLContent _homePageURLContent)
    {
        _videoUrl = _homePageURLContent.videoUrl;

        LoadHaloVideo();
    }

    public void LoadHaloVideo()
    {
        _videoPlayManger.LoadVideoAndPlay(_videoUrl);
    }
}

