using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static iTween;
using static System.TimeZoneInfo;

public class PlayerProfilePanelManager : MonoBehaviour
{
    [SerializeField]
    private EaseType _easeType;

    [SerializeField]
    [Range(0f, 5f)]
    private float transitionTime;

    private void OnEnable()
    {
        ValueTo(gameObject, iTween.Hash("from", 0, "to", 1f,
            "onupdatetarget", gameObject,"oncomplete", "OnUpdateCompleteEnable", "onupdate", "updateFromValue",
            "time", transitionTime, "easetype", iTween.EaseType.easeOutExpo));

        iTween.MoveTo(gameObject, iTween.Hash("x", 0, "time", transitionTime, "easetype", _easeType));
    }
    public void EnablePanel()
    {
        gameObject.transform.GetComponent<CanvasGroup>().interactable = false;

        ValueTo(gameObject, iTween.Hash("from", 0f, "to", 1f,
           "onupdatetarget", gameObject, "oncomplete", "OnUpdateCompleteEnable", "onupdate", "updateFromValue",
           "time", transitionTime, "easetype", _easeType));

    }
    public void UserProfilesAction()
    {
        gameObject.transform.GetComponent<CanvasGroup>().alpha = 1.0f;
        gameObject.transform.GetComponent<CanvasGroup>().interactable = false;

        ValueTo(gameObject, iTween.Hash("from", 1f, "to", 0f,
            "onupdatetarget", gameObject, "onupdate", "updateFromValue",
            "time", transitionTime, "easetype", _easeType));

        SlidePanelManager.instance._userProfilePanelManager.gameObject.SetActive(true);
    }
    public void CreateBlogsAction()
    {

    }
    public void FriendsAction()
    {
        gameObject.transform.GetComponent<CanvasGroup>().alpha = 1.0f;
        gameObject.transform.GetComponent<CanvasGroup>().interactable = false;

        ValueTo(gameObject, iTween.Hash("from", 1f, "to", 0f,
            "onupdatetarget", gameObject, "onupdate", "updateFromValue",
            "time", transitionTime, "easetype", _easeType));

        SlidePanelManager.instance._friendsPanelContainer.gameObject.SetActive(true);
    }
    public void MyBlogsAction()
    {

    }
    public void MyDesignsAction()
    {

    }
    public void MyGeneralBlogsAction()
    {

    }
    public void MyStoryOfCareAction()
    {

    }
    public void SetUpCareerAction()
    {

    }
    public void BackButton()
    {
      
        gameObject.transform.GetComponent<CanvasGroup>().interactable = false;

        ValueTo(gameObject, iTween.Hash("from", 1f, "to", 0f,
            "onupdatetarget", gameObject, "onupdate", "updateFromValue",
            "time", transitionTime, "oncomplete", "OnUpdateCompleteDissable", "easetype", _easeType));

        iTween.MoveTo(gameObject, iTween.Hash("x", -250f, "time", transitionTime, "easetype", _easeType));

        SlidePanelManager.instance._mainSlidePanelManager.EnablePanel();
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
