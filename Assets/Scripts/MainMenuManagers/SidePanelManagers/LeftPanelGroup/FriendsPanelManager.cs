using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static iTween;
using static System.TimeZoneInfo;

public class FriendsPanelManager : MonoBehaviour
{
    [SerializeField]
    private EaseType _easeType;

    [SerializeField]
    [Range(0f, 5f)]
    private float transitionTime;
    private void OnEnable()
    {
        ValueTo(gameObject, iTween.Hash("from", 0, "to", 1f,
            "onupdatetarget", gameObject, "oncomplete", "OnUpdateCompleteEnable", "onupdate", "updateFromValue",
            "time", transitionTime, "easetype", iTween.EaseType.easeOutExpo));

        iTween.MoveTo(gameObject, iTween.Hash("x", 0, "time", transitionTime, "easetype", _easeType));
    }
    public void FindMembersAction()
    {
        switch (MainMenuCotroller.instance._UserLoginState)
        {
            case UserLoginState.USER_LOGGED_OUT:
                MainMenuCotroller.instance.SetScreenClean();
                MainMenuCotroller.instance.LoginManager.gameObject.SetActive(true);
                break;

            case UserLoginState.USER_LOGGED_IN:
                MainMenuCotroller.instance.SetScreenClean();
                MainMenuCotroller.instance._friendsListType = friendsListType.GetMembersList;
                MainMenuCotroller.instance._FindFriendsManager.gameObject.SetActive(true);
                break;
        }
    }
    public void FriendsActivitiesAction()
    {

    }
    public void FriendsListAction()
    {
        switch (MainMenuCotroller.instance._UserLoginState)
        {
            case UserLoginState.USER_LOGGED_OUT:
                MainMenuCotroller.instance.SetScreenClean();
                MainMenuCotroller.instance.LoginManager.gameObject.SetActive(true);
                break;

            case UserLoginState.USER_LOGGED_IN:

                MainMenuCotroller.instance.SetScreenClearExceptBubble();

               MainMenuCotroller.instance._friendsListType = friendsListType.GetFriendsDetails;
                MainMenuCotroller.instance._FindFriendsManager.gameObject.SetActive(true);
                break;
        }
    }
    public void FriendsRequestAction()
    {
        switch (MainMenuCotroller.instance._UserLoginState)
        {
            case UserLoginState.USER_LOGGED_OUT:
                MainMenuCotroller.instance.SetScreenClean();
                MainMenuCotroller.instance.LoginManager.gameObject.SetActive(true);
                break;

            case UserLoginState.USER_LOGGED_IN:

                MainMenuCotroller.instance.SetScreenClean();

                MainMenuCotroller.instance._friendsListType = friendsListType.GetFriendRequestDetails;
                MainMenuCotroller.instance._FindFriendsManager.gameObject.SetActive(true);
                break;
        }
    }

    public void BackButton()
    {
        gameObject.transform.GetComponent<CanvasGroup>().interactable = false;

        ValueTo(gameObject, iTween.Hash("from", 1f, "to", 0f,
            "onupdatetarget", gameObject, "onupdate", "updateFromValue",
            "time", transitionTime, "oncomplete", "OnUpdateCompleteDissable", "easetype", _easeType));

        iTween.MoveTo(gameObject, iTween.Hash("x", -250f, "time", transitionTime, "easetype", _easeType));

        SlidePanelManager.instance._playerProfilePanelManager.EnablePanel();
    }

    public void updateFromValue(float newValue)
    {
        gameObject.transform.GetComponent<CanvasGroup>().alpha = newValue;

    }

    public void OnUpdateCompleteEnable()
    {
        gameObject.transform.GetComponent<CanvasGroup>().interactable = true;
    }

    public void OnUpdateCompleteDissable()
    {
        gameObject.transform.GetComponent<CanvasGroup>().interactable = false;
        gameObject.SetActive(false);
    }
}
