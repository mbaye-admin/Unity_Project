using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static iTween;

public class BlogsPanelManager : MonoBehaviour
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
    public void CareStoriesAction()
    {

    }
    public void CareersAction()
    {

    }
    public void DesignsAction()
    {

    }
    public void FamilyAndFriendsAction()
    {

    }
    public void FlimsAction()
    {

    }
    public void GeneralAction()
    {

    }
    public void MusicAction()
    {

    }
    public void MountainsAndSeasAction()
    {

    }
    public void PoliticsAction()
    {

    }
    public void SportsAction()
    {

    }
    public void TravelAction()
    {

    }
    public void BackButton()
    {
        gameObject.transform.GetComponent<CanvasGroup>().interactable = false;

        ValueTo(gameObject, iTween.Hash("from", 1f, "to", 0f,
            "onupdatetarget", gameObject, "onupdate", "updateFromValue",
            "time", transitionTime, "oncomplete", "OnUpdateCompleteDissable", "easetype",_easeType));

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
