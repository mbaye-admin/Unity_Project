using System;
using System.Collections;
using System.Collections.Generic;
//using System.Windows.Forms;
using UnityEngine;
using UnityEngine.UI;
using static iTween;
using static System.TimeZoneInfo;
using Button = UnityEngine.UI.Button;

public class MainSlidePanelManager : MonoBehaviour
{
    [SerializeField]
    private EaseType _easeType;

    [SerializeField]
    [Range(0f, 5f)]
    private float transitionTime;

    [SerializeField]
    private Button registerButton;

    [SerializeField]
    private Button loginButton;

    [SerializeField]
    private Button logoutButton;

    public void SetSlidePanel(UserLoginState _UserLoginState)
    {
        registerButton.gameObject.SetActive(false);
        loginButton.gameObject.SetActive(false);
        logoutButton.gameObject.SetActive(false);

        switch (_UserLoginState)
        {
            case UserLoginState.USER_LOGGED_IN:
                logoutButton.gameObject.SetActive(true);
                break;

            case UserLoginState.USER_LOGGED_OUT:
                registerButton.gameObject.SetActive(true);
                loginButton.gameObject.SetActive(true);
                break;
        }
    }

    public void EnablePanel()
    {
        gameObject.transform.GetComponent<CanvasGroup>().interactable = false;

        ValueTo(gameObject, iTween.Hash("from", 0f, "to", 1f,
           "onupdatetarget", gameObject, "oncomplete", "updateComplete", "onupdate", "updateFromValue",
           "time", transitionTime, "easetype", _easeType));

    }

    public void HomeAction()
    {
        MainMenuCotroller.instance._cameraController.gameObject.SetActive(false);
        MainMenuCotroller.instance._cameraController.ResetRotation();
        MainMenuCotroller.instance._cameraController.gameObject.SetActive(true);
        MainMenuCotroller.instance.haloManager.LoadHaloVideo();
    }
    public void MyProfileAction()
    {
        gameObject.transform.GetComponent<CanvasGroup>().alpha = 1.0f;
        gameObject.transform.GetComponent<CanvasGroup>().interactable = false;

        ValueTo(gameObject, iTween.Hash("from", 1f, "to", 0f,
            "onupdatetarget", gameObject, "onupdate", "updateFromValue",
            "time", transitionTime, "easetype", _easeType));

        SlidePanelManager.instance._playerProfilePanelManager.gameObject.SetActive(true);

    }
    public void ParticipateAction()
    {
        gameObject.transform.GetComponent<CanvasGroup>().alpha = 1.0f;
        gameObject.transform.GetComponent<CanvasGroup>().interactable = false;

        ValueTo(gameObject, iTween.Hash("from", 1f, "to", 0f,
            "onupdatetarget", gameObject, "onupdate", "updateFromValue",
            "time", transitionTime, "easetype", _easeType));

        SlidePanelManager.instance._participatePanelManager.gameObject.SetActive(true);
    }
    public void BlogsAction()
    {
        gameObject.transform.GetComponent<CanvasGroup>().alpha = 1.0f;
        gameObject.transform.GetComponent<CanvasGroup>().interactable = false;

        ValueTo(gameObject, iTween.Hash("from", 1f, "to", 0f,
            "onupdatetarget", gameObject, "onupdate", "updateFromValue",
            "time", transitionTime, "easetype", _easeType));

        SlidePanelManager.instance._blogsPanelManager.gameObject.SetActive(true);
    }
    public void JobProfilesAction()
    {
        gameObject.transform.GetComponent<CanvasGroup>().alpha = 1.0f;
        gameObject.transform.GetComponent<CanvasGroup>().interactable = false;

        ValueTo(gameObject, iTween.Hash("from", 1f, "to", 0f,
            "onupdatetarget", gameObject, "onupdate", "updateFromValue",
            "time", transitionTime, "easetype", _easeType));

        SlidePanelManager.instance._jobProfilePanelManager.gameObject.SetActive(true);
    }
    public void GuideAction()
    {

    }
    public void CareersAction()
    {

    }
    public void AboutUsAction()
    {

    }
    public void TermsAction()
    {

    }
    public void RegisterAction()
    {
        MainMenuCotroller.instance.SetScreenClean();
        MainMenuCotroller.instance.registrationManager.gameObject.SetActive(true);
    }
    public void LoginAction()
    {
        MainMenuCotroller.instance.SetScreenClean(); 
        MainMenuCotroller.instance.LoginManager.gameObject.SetActive(true);
    }
    public void LogOutAction()
    {
        MainMenuCotroller.instance._screenState = screenState.HomeScreen_Active;
        MainMenuCotroller.instance._bubbleManager.EnableBubbles();
        MainMenuCotroller.instance._UserLoginState = UserLoginState.USER_LOGGED_OUT;

        MainMenuCotroller.instance.registrationManager.gameObject.SetActive(false);
        MainMenuCotroller.instance.LoginManager.gameObject.SetActive(false);

        MainMenuCotroller.instance._slidePanelManager._mainSlidePanelManager.SetSlidePanel(MainMenuCotroller.instance._UserLoginState);
        MainMenuCotroller.instance.planetsControlManager.SetPlanetState(MainMenuCotroller.instance._UserLoginState);
        MainMenuCotroller.instance.SetScreenPopulated();

        MainMenuCotroller.instance._AstronautManager.gameObject.SetActive(false);
    }


    public void updateFromValue(float newValue)
    {
        gameObject.transform.GetComponent<CanvasGroup>().alpha = newValue;
    }

    public void updateComplete()
    {
        gameObject.transform.GetComponent<CanvasGroup>().interactable = true;
    }
}
