using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

//using System.Windows.Forms.VisualStyles;
using UnityEngine;

public class MainMenuCotroller : MonoBehaviour
{
    public static MainMenuCotroller instance = null;
    public static MainMenuCotroller Instance
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

    public List<HomePageURLContent> homePageURLContents;

    public HaloManager haloManager;

    public BubbleManager _bubbleManager;

    [SerializeField]
    private LoadingScreenManager _loadingScreenManager;

    public PlanetsControlManager planetsControlManager;

    public CharacterControlManager charaterControlManager;

    public CharacterDescriptionManager _CharacterDescriptionManager;

    public PlayerInputController _playerInputController;

    public CameraController _cameraController;

    public VideoPlayerManager _videoPlayerManager;

    public RegistrationManager registrationManager;

    public LoginManager LoginManager;

    public GameObject mainCanvasObj;

    public SlidePanelManager _slidePanelManager;

    public AstronautManager _AstronautManager;

    public FindFriendsManager _FindFriendsManager;

    public UIVideoManager _UIVideoManager;

    public List<AppReadyState> AppReadyState;

    public UserLoginState _UserLoginState;
    public screenState _screenState;
    public UserDetails _userDetails;

    public friendsListType _friendsListType;



    private void Start()
    {
        AppReadyState = new List<AppReadyState>();
        _loadingScreenManager.gameObject.SetActive(true);
        LoadHomePageUrlData();
    }
    void LoadHomePageUrlData()
    {
        _UserLoginState = UserLoginState.USER_LOGGED_OUT;
        _screenState = screenState.HomeScreen_Active;

        _slidePanelManager._mainSlidePanelManager.SetSlidePanel(_UserLoginState);
        planetsControlManager.SetPlanetState(_UserLoginState);

        StartCoroutine(WebSericesManager.instance.GetHomePageData((LeaderBoardAPIResponse) =>
         {
             switch (LeaderBoardAPIResponse._apiResponseType)
             {
                 case apiResponseType.SUCCESS:
                     Debug.Log("Success");
                     homePageURLContents = LeaderBoardAPIResponse.homePageURLContents;

                     InitateHaloVideo(videoTag.Lioness);
                     InitateBubbles();

                     break;

                 case apiResponseType.FAIL:
                     Debug.LogWarning(LeaderBoardAPIResponse.responseMessage);

                     break;

                 case apiResponseType.SEVER_ERROR:
                     Debug.LogWarning(LeaderBoardAPIResponse.responseMessage);

                     break;
             }
         }));
    }
    void InitateHaloVideo(videoTag _videoTag)
    {
        foreach (HomePageURLContent item in homePageURLContents)
        {
            // Debug.Log("_videoTag " + _videoTag.ToString() + "item   " + item.Name);
            if (_videoTag.ToString() == item.Name)
            {
                Debug.Log("Condition " + item.Name);
                haloManager.LoadHaloData(item);
            }
        }
    }
    void InitateBubbles()
    {
        foreach (HomePageURLContent item in homePageURLContents)
        {
            //Debug.Log("Condition " + item.Name);
            _bubbleManager.bubbleData.Add(item);
        }
        _bubbleManager.LoadBubblesData();
        _bubbleManager.LoadBubblesData();
    }
    public void CheckAppState(AppReadyState _AppReadyState)
    {
        if (!AppReadyState.Contains(_AppReadyState))
        {
            AppReadyState.Add(_AppReadyState);
        }
        if (AppReadyState.Count >= 2)
        {
            MainMenuCotroller.instance._videoPlayerManager.videoPlayer.Play();

            //Invoke("HideLoadingScreen", .5f);
            StartCoroutine(HideLoadingScreen());
        }
    }

    IEnumerator HideLoadingScreen()
    {
        yield return new WaitUntil(() => MainMenuCotroller.instance._videoPlayerManager.videoPlayer.isPlaying);
        MainMenuCotroller.instance._UIVideoManager.InitVideoStream();
        _loadingScreenManager.gameObject.SetActive(false);

    }
    public void SetScreenClean()
    {
        MainMenuCotroller.instance._cameraController.gameObject.SetActive(false);
        MainMenuCotroller.instance._cameraController.ResetRotation();
        MainMenuCotroller.instance._cameraController.gameObject.SetActive(true);

        MainMenuCotroller.instance.haloManager.gameObject.SetActive(false);
        MainMenuCotroller.instance.planetsControlManager.gameObject.SetActive(false);
        MainMenuCotroller.instance.charaterControlManager.gameObject.SetActive(false);

        MainMenuCotroller.instance._cameraController.gameObject.GetComponent<CameraController>().enabled = false;

        MainMenuCotroller.instance._screenState = screenState.HomeScreen_Inactive;
        MainMenuCotroller.instance._bubbleManager.DissableBubble();
    }
    public void SetScreenPopulated()
    {
        MainMenuCotroller.instance._cameraController.gameObject.SetActive(false);
        MainMenuCotroller.instance._cameraController.ResetRotation();
        MainMenuCotroller.instance._cameraController.gameObject.SetActive(true);

        MainMenuCotroller.instance.haloManager.gameObject.SetActive(true);
        MainMenuCotroller.instance.planetsControlManager.gameObject.SetActive(true);
        MainMenuCotroller.instance.charaterControlManager.gameObject.SetActive(true);

        MainMenuCotroller.instance._cameraController.gameObject.GetComponent<CameraController>().enabled = true;

        MainMenuCotroller.instance._videoPlayerManager.videoPlayer.Play();

        MainMenuCotroller.instance._bubbleManager.EnableBubbles();
        MainMenuCotroller.instance._screenState = screenState.HomeScreen_Active;
    }

    public void SetScreenClearExceptBubble()
    {
        MainMenuCotroller.instance._cameraController.gameObject.SetActive(false);
        MainMenuCotroller.instance._cameraController.ResetRotation();
        MainMenuCotroller.instance._cameraController.gameObject.SetActive(true);

        MainMenuCotroller.instance.haloManager.gameObject.SetActive(false);
        MainMenuCotroller.instance.planetsControlManager.gameObject.SetActive(false);
        MainMenuCotroller.instance.charaterControlManager.gameObject.SetActive(false);

        MainMenuCotroller.instance._cameraController.gameObject.GetComponent<CameraController>().enabled = false;

        MainMenuCotroller.instance._bubbleManager.EnableBubbles();
        MainMenuCotroller.instance._screenState = screenState.HomeScreen_Active;
    }
}
[System.Serializable]
public class HomePageURLContent
{
    public string Name;
    public string videoUrl;
    public string thubnailImgUrl;
    public string haloPictureUrl;
}
public enum videoTag
{
    Lioness
}
public enum AppReadyState
{
    APP_NOT_READY,
    IsVideoReady,
    IsBubbleReady
}
[System.Serializable]
public class UserDetails
{
    public string apiId;
    public string firstName;
    public string lastName;
    public string userName;
    public string email;
    public string password;
    public string referenceCode;
    public string cityName;
    public string stateName;
    public string countryName;
    public string genderId;
    public string idNumber;
    public string adress;
    public string school;
    public string mobileNumber;
    public string dateOfBirth;
    public string profilePicUrl;
    public Sprite profleSprite;
    public string occupation;
    public string status;
    public string is_login;
    public string timezone;
}
public enum UserLoginState
{
    USER_LOGGED_OUT,
    USER_LOGGED_IN
}
public enum screenState
{
    HomeScreen_Active,
    HomeScreen_Inactive
}