using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashSceneManager : MonoBehaviour
{
    public static SplashSceneManager instance = null;

    public static SplashSceneManager Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        //DontDestroyOnLoad(this.gameObject);
    }
    [SerializeField]
    private SplashPanelManager splashPanelManager;
}
