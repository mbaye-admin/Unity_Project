using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidePanelManager : MonoBehaviour
{
    public static SlidePanelManager instance = null;
    public static SlidePanelManager Instance
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


    public MainSlidePanelManager _mainSlidePanelManager;
    public ParticipatePanelManager _participatePanelManager;
    public BlogsPanelManager _blogsPanelManager;
    public JobProfilePanelManager _jobProfilePanelManager;
    public ListProfilePanelManager _listProfilePanelManager;
    public PlayerProfilePanelManager _playerProfilePanelManager;
    public UserProfilePanelManager _userProfilePanelManager;
    public FriendsPanelManager _friendsPanelContainer;
    public CareerPanelManager _careerPanelContainer;



    [SerializeField]
    private iTween.EaseType _easeType;

    [SerializeField]
    [Range(0f, 5f)]
    private float transitionTime;

    [SerializeField]
    private Vector3 initPosition;

    private void OnEnable()
    {
        initPosition = transform.position;
        _mainSlidePanelManager.gameObject.GetComponent<CanvasGroup>().alpha = 1f;
        _mainSlidePanelManager.gameObject.SetActive(true);
    }


    bool buttonStatus = false;
    public void ExpandPanel()
    {
        switch (buttonStatus)
        {
            case false:
                buttonStatus = true;
                _mainSlidePanelManager.gameObject.SetActive(true);
                iTween.MoveTo(gameObject, iTween.Hash("position", new Vector3(0f, transform.position.y, transform.position.z), "time", transitionTime, "easetype", _easeType));
                break;

            case true:
                buttonStatus = false;
                iTween.MoveTo(gameObject, iTween.Hash("position", initPosition, "time", transitionTime, "easetype", _easeType));
                break;
        }
    }
}

