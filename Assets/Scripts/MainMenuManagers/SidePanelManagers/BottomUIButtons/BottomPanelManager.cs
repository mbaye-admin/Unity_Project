using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomPanelManager : MonoBehaviour
{
    public void VideoCallButton()
    {
        MainMenuCotroller.instance.SetScreenClean();
        switch (MainMenuCotroller.instance._UserLoginState)
        {
            case UserLoginState.USER_LOGGED_OUT:
                MainMenuCotroller.instance.LoginManager.gameObject.SetActive(true);
                break;

            case UserLoginState.USER_LOGGED_IN:
                break;
        }
    }
    public void VoiceCallButton()
    {
        MainMenuCotroller.instance.SetScreenClean();
        switch (MainMenuCotroller.instance._UserLoginState)
        {
            case UserLoginState.USER_LOGGED_OUT:
                MainMenuCotroller.instance.LoginManager.gameObject.SetActive(true);
                break;

            case UserLoginState.USER_LOGGED_IN:
                break;
        }
    }

    public void chatButton()
    {
        MainMenuCotroller.instance.SetScreenClean();
        switch (MainMenuCotroller.instance._UserLoginState)
        {
            case UserLoginState.USER_LOGGED_OUT:
                MainMenuCotroller.instance.LoginManager.gameObject.SetActive(true);
                break;

            case UserLoginState.USER_LOGGED_IN:
                break;
        }
    }

    public void GroupChatButton()
    {
        MainMenuCotroller.instance.SetScreenClean();
        switch (MainMenuCotroller.instance._UserLoginState)
        {
            case UserLoginState.USER_LOGGED_OUT:
                MainMenuCotroller.instance.LoginManager.gameObject.SetActive(true);
                break;

            case UserLoginState.USER_LOGGED_IN:
                break;
        }
    }

    public void InviteButton()
    {
        MainMenuCotroller.instance.SetScreenClean();
        switch (MainMenuCotroller.instance._UserLoginState)
        {
            case UserLoginState.USER_LOGGED_OUT:
                MainMenuCotroller.instance.LoginManager.gameObject.SetActive(true);
                break;

            case UserLoginState.USER_LOGGED_IN:
                break;
        }
    }
}
