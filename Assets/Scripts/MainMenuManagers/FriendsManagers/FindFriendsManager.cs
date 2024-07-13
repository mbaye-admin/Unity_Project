using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class FindFriendsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject findMebersHeaderObj;
    [SerializeField]
    private GameObject findFriendsHeaderObj;
    [SerializeField]
    private GameObject friendRequestHeaderObj;

    [SerializeField]
    private TMP_InputField searchInput_Friends;
    [SerializeField]
    private TMP_InputField searchInput_Members;
    [SerializeField]
    private TMP_InputField searchInput_FriendRequest;

    [SerializeField]
    private Button searchByButton_members;
    [SerializeField]
    private Button searchByButton_friends;
    [SerializeField]
    private Button searchByButton_friendRequest;


    public List<friendsDetail> getMembersDetails;
    public List<friendsDetail> getFriendsDetails;
    public List<friendsDetail> getFriendsRequestDetails;


    [SerializeField]
    private GameObject friendsDetailElementObj;
    [SerializeField]
    private GameObject friendsRequestElementObj;
    [SerializeField]
    private GameObject friendsDetailParentObj;


    [SerializeField]
    private GameObject scrollViewComponetObj;


    [SerializeField]
    private searchByFilter _searchByFilter;

    [SerializeField]
    private GameObject filterViewComponetObj_Members;
    [SerializeField]
    private GameObject filterViewComponetObj_Friends;
    [SerializeField]
    private GameObject filterViewComponetObj_FriendRequest;

    public float pageCount;
    [Range(5, 50)]
    public float elementLimit;
    public int currentPageIndex;

    [SerializeField]
    private Button loadNextButton;

    [SerializeField]
    private GridLayoutGroup _gridLayoutGroup;

    [SerializeField]
    private GameObject[] headingImage;

    private void OnEnable()
    {
        SetPageEnvironment();

        // LoadFriendRequestListAction();
    }
    void SetPageEnvironment()
    {
        findMebersHeaderObj.SetActive(false);
        findFriendsHeaderObj.SetActive(false);
        friendRequestHeaderObj.SetActive(false);

        filterViewComponetObj_Members.SetActive(false);
        filterViewComponetObj_Friends.SetActive(false);
        filterViewComponetObj_FriendRequest.SetActive(false);

        searchInput_Friends.gameObject.SetActive(false);
        searchInput_Members.gameObject.SetActive(false);
        searchInput_Members.gameObject.SetActive(false);

        searchInput_Friends.text = "";
        searchInput_Members.text = "";
        searchInput_FriendRequest.text = "";

        currentPageIndex = 1;
        loadNextButton.gameObject.SetActive(false);

        scrollViewComponetObj.SetActive(false);

        switch (MainMenuCotroller.instance._friendsListType)
        {
            case friendsListType.GetFriendsDetails:
                searchByButton_members.gameObject.SetActive(false);
                searchByButton_friends.gameObject.SetActive(true);
                searchByButton_friendRequest.gameObject.SetActive(false);
                findFriendsHeaderObj.SetActive(true);
                findMebersHeaderObj.SetActive(false);
                friendRequestHeaderObj.SetActive(false);

                foreach (GameObject item in headingImage)
                {
                    item.SetActive(false);
                }

                LoadFriendsData();

                SearchByFilterAction("firstName");

                break;

            case friendsListType.GetMembersList:
                searchByButton_members.gameObject.SetActive(true);
                searchByButton_friends.gameObject.SetActive(false);
                searchByButton_friendRequest.gameObject.SetActive(false);
                findFriendsHeaderObj.SetActive(false);
                findMebersHeaderObj.SetActive(true);
                friendRequestHeaderObj.SetActive(false);

                SearchByFilterAction("firstName");

                break;
            case friendsListType.GetFriendRequestDetails:
                searchByButton_members.gameObject.SetActive(false);
                searchByButton_friends.gameObject.SetActive(false);
                searchByButton_friendRequest.gameObject.SetActive(false);
                findFriendsHeaderObj.SetActive(false);
                findMebersHeaderObj.SetActive(false);
                friendRequestHeaderObj.SetActive(true);
                LoadFriendRequestListAction();

                foreach (GameObject item in headingImage)
                {
                    item.SetActive(false);
                }

                SearchByFilterAction("firstName");

                break;
        }

    }
    public void SearchFilterAction()
    {
        switch (MainMenuCotroller.instance._friendsListType)
        {
            case friendsListType.GetFriendsDetails:

                filterViewComponetObj_Members.SetActive(false);
                filterViewComponetObj_Friends.SetActive(true);
                filterViewComponetObj_FriendRequest.SetActive(false);
                LoadFriendsData();

                break;

            case friendsListType.GetMembersList:

                filterViewComponetObj_Members.SetActive(true);
                filterViewComponetObj_Friends.SetActive(false);
                filterViewComponetObj_FriendRequest.SetActive(false);

                break;
            case friendsListType.GetFriendRequestDetails:

                filterViewComponetObj_Members.SetActive(false);
                filterViewComponetObj_Friends.SetActive(false);
                filterViewComponetObj_FriendRequest.SetActive(true);
                LoadFriendRequestListAction();
                break;
        }
    }
    public void SearchByFilterAction(string searchByFilterStr)
    {
        switch (searchByFilterStr)
        {
            case "firstName":
                searchInput_Friends.placeholder.GetComponent<TMP_Text>().text = "Search By First Name";
                searchInput_Members.placeholder.GetComponent<TMP_Text>().text = "Search By First Name";
                searchInput_FriendRequest.placeholder.GetComponent<TMP_Text>().text = "Search By First Name";
                _searchByFilter = searchByFilter.FirstName;
                break;
            case "userName":
                searchInput_Friends.placeholder.GetComponent<TMP_Text>().text = "Search By User Name";
                searchInput_Members.placeholder.GetComponent<TMP_Text>().text = "Search By User Name";
                searchInput_FriendRequest.placeholder.GetComponent<TMP_Text>().text = "Search By User Name";
                _searchByFilter = searchByFilter.UserName;
                break;
            case "email":
                searchInput_Friends.placeholder.GetComponent<TMP_Text>().text = "Search By User email";
                searchInput_Members.placeholder.GetComponent<TMP_Text>().text = "Search By User email";
                searchInput_FriendRequest.placeholder.GetComponent<TMP_Text>().text = "Search By User email";
                _searchByFilter = searchByFilter.email;
                break;

        }
        filterViewComponetObj_Members.SetActive(false);
        filterViewComponetObj_Friends.SetActive(false);
        filterViewComponetObj_FriendRequest.SetActive(false);

        searchByButton_members.gameObject.SetActive(false);
        searchByButton_friends.gameObject.SetActive(false);
        searchByButton_friendRequest.gameObject.SetActive(false);

        searchInput_Members.gameObject.SetActive(true);
        searchInput_Friends.gameObject.SetActive(true);
        searchInput_FriendRequest.gameObject.SetActive(true);
    }
    public void LoadMemberData()
    {
        if (searchInput_Members.text.Length > 0)
        {
            for (int i = 0; i < friendsDetailParentObj.transform.childCount; i++)
            {
                Destroy(friendsDetailParentObj.transform.GetChild(i).gameObject);
            }

            currentPageIndex = 1;

            StartCoroutine(WebSericesManager.instance.GetMembersByFilter(MainMenuCotroller.instance._userDetails.apiId,
                searchInput_Members.text, _searchByFilter, WebSericesManager.instance.authToken, currentPageIndex, (GetMembersResponseData) =>
            {
                switch (GetMembersResponseData._apiResponseType)
                {
                    case apiResponseType.SUCCESS:
                        getMembersDetails = GetMembersResponseData.membersDetails;

                        GenerateUserDetails(getMembersDetails);

                        if (pageCount > 1)
                        {
                            loadNextButton.gameObject.SetActive(true);
                        }

                        Debug.Log("Success");

                        break;

                    case apiResponseType.FAIL:
                        Debug.LogWarning(GetMembersResponseData.responseMessage);

                        break;

                    case apiResponseType.SEVER_ERROR:
                        Debug.LogWarning(GetMembersResponseData.responseMessage);

                        break;
                }
            }));
        }
    }
    void GenerateUserDetails(List<friendsDetail> friendsDetails)
    {
        if (friendsDetails.Count > 0)
        {
            scrollViewComponetObj.SetActive(true);
        }

        switch (MainMenuCotroller.instance._friendsListType)
        {
            case friendsListType.GetFriendRequestDetails:
                _gridLayoutGroup.cellSize = new Vector2(800, 400);
                foreach (friendsDetail friendsDetail in friendsDetails)
                {
                    GameObject friendElementOb = Instantiate(friendsRequestElementObj);
                    friendElementOb.transform.parent = friendsDetailParentObj.transform;
                    friendElementOb.transform.localScale = Vector3.one;

                    friendElementOb.GetComponent<FriendsElementsManager>().friendsDetail = friendsDetail;

                    friendElementOb.SetActive(true);
                }
                break;

            default:
                _gridLayoutGroup.cellSize = new Vector2(300, 400);
                foreach (friendsDetail friendsDetail in friendsDetails)
                {
                    GameObject friendElementOb = Instantiate(friendsDetailElementObj);
                    friendElementOb.transform.parent = friendsDetailParentObj.transform;
                    friendElementOb.transform.localScale = Vector3.one;

                    friendElementOb.GetComponent<FriendsElementsManager>().friendsDetail = friendsDetail;

                    friendElementOb.SetActive(true);
                }
                break;
        }



    }
    public void BackButton()
    {
        MainMenuCotroller.instance._screenState = screenState.HomeScreen_Active;
        MainMenuCotroller.instance._bubbleManager.EnableBubbles();

        MainMenuCotroller.instance._slidePanelManager._mainSlidePanelManager.SetSlidePanel(MainMenuCotroller.instance._UserLoginState);
        MainMenuCotroller.instance.planetsControlManager.SetPlanetState(MainMenuCotroller.instance._UserLoginState);
        MainMenuCotroller.instance.SetScreenPopulated();


        MainMenuCotroller.instance._FindFriendsManager.gameObject.SetActive(false);
        
    }
    public void LoadFriendsData()
    {
        for (int i = 0; i < friendsDetailParentObj.transform.childCount; i++)
        {
            Destroy(friendsDetailParentObj.transform.GetChild(i).gameObject);
        }

        currentPageIndex = 1;

        StartCoroutine(WebSericesManager.instance.GetFriendList(MainMenuCotroller.instance._userDetails.apiId,
            searchInput_Friends.text, WebSericesManager.instance.authToken, (GetFriendsDetails) =>
               {
                   switch (GetFriendsDetails._apiResponseType)
                   {
                       case apiResponseType.SUCCESS:
                           getFriendsDetails = GetFriendsDetails.friendsDetails;

                           GenerateUserDetails(getFriendsDetails);

                           if (pageCount > 1)
                           {
                               loadNextButton.gameObject.SetActive(true);
                           }
                           else
                           {
                               loadNextButton.gameObject.SetActive(false);
                           }

                           Debug.Log("Success");

                           break;

                       case apiResponseType.FAIL:
                           Debug.LogWarning(GetFriendsDetails.responseMessage);

                           break;

                       case apiResponseType.SEVER_ERROR:
                           Debug.LogWarning(GetFriendsDetails.responseMessage);

                           break;
                   }
               }));
    }
    public void LoadFriendRequestListAction()
    {
        for (int i = 0; i < friendsDetailParentObj.transform.childCount; i++)
        {
            Destroy(friendsDetailParentObj.transform.GetChild(i).gameObject);
        }

        currentPageIndex = 1;

        StartCoroutine(WebSericesManager.instance.GetFriendRquestSentList(MainMenuCotroller.instance._userDetails.apiId,
            searchInput_FriendRequest.text, WebSericesManager.instance.authToken, (GetFriendsDetails) =>
            {
                switch (GetFriendsDetails._apiResponseType)
                {
                    case apiResponseType.SUCCESS:
                        getFriendsRequestDetails = GetFriendsDetails.membersDetails;

                        GenerateUserDetails(getFriendsRequestDetails);

                        if (pageCount > 1)
                        {
                            loadNextButton.gameObject.SetActive(true);
                        }
                        else
                        {
                            loadNextButton.gameObject.SetActive(false);
                        }

                        Debug.Log("Success");

                        break;

                    case apiResponseType.FAIL:
                        Debug.LogWarning(GetFriendsDetails.responseMessage);

                        break;

                    case apiResponseType.SEVER_ERROR:
                        Debug.LogWarning(GetFriendsDetails.responseMessage);
                        break;
                }
            }));
    }
    public void LoadNextInList()
    {
        if (pageCount > 1 && currentPageIndex < (pageCount - 1))
        {
            currentPageIndex += 1;
            switch (MainMenuCotroller.instance._friendsListType)
            {
                case friendsListType.GetFriendsDetails:

                    StartCoroutine(WebSericesManager.instance.GetFriendList(MainMenuCotroller.instance._userDetails.apiId,
            searchInput_Friends.text, WebSericesManager.instance.authToken, (GetFriendsDetails) =>
            {
                switch (GetFriendsDetails._apiResponseType)
                {
                    case apiResponseType.SUCCESS:
                        getFriendsDetails = GetFriendsDetails.friendsDetails;

                        GenerateUserDetails(getFriendsDetails);

                        if (pageCount > 1)
                        {
                            loadNextButton.gameObject.SetActive(true);
                        }
                        else
                        {
                            loadNextButton.gameObject.SetActive(false);
                        }

                        Debug.Log("Success");

                        break;

                    case apiResponseType.FAIL:
                        Debug.LogWarning(GetFriendsDetails.responseMessage);

                        break;

                    case apiResponseType.SEVER_ERROR:
                        Debug.LogWarning(GetFriendsDetails.responseMessage);

                        break;
                }
            }));

                    break;

                case friendsListType.GetMembersList:

                    StartCoroutine(WebSericesManager.instance.GetMembersByFilter(MainMenuCotroller.instance._userDetails.apiId,
               searchInput_Members.text, _searchByFilter, WebSericesManager.instance.authToken, currentPageIndex, (GetMembersResponseData) =>
               {
                   switch (GetMembersResponseData._apiResponseType)
                   {
                       case apiResponseType.SUCCESS:
                           getMembersDetails = GetMembersResponseData.membersDetails;

                           GenerateUserDetails(getMembersDetails);

                           if (pageCount > 1)
                           {
                               loadNextButton.gameObject.SetActive(true);
                           }

                           Debug.Log("Success");

                           break;

                       case apiResponseType.FAIL:
                           Debug.LogWarning(GetMembersResponseData.responseMessage);

                           break;

                       case apiResponseType.SEVER_ERROR:
                           Debug.LogWarning(GetMembersResponseData.responseMessage);

                           break;
                   }
               }));

                    break;

                case friendsListType.GetFriendRequestDetails:

                    break;
            }
        }
        else
        {
            loadNextButton.gameObject.SetActive(false);
        }
    }
}
public enum FriendStatus
{
    Not_A_Friend,
    Request_Accepted,
    Request_Rejected,
    Request_Sent,
    videoCall
}
[System.Serializable]
public class friendsDetail
{
    public string requestId;
    public string friendId;
    public string firstName;
    public string lastName;
    public string userName;
    public string email;
    public string referenceCode;
    public string cityName;
    public string countryName;
    public string stateName;
    public string genderId;
    public string genderName;
    public string idNumber;
    public string address;
    public string school;
    public string mobileNumber;
    public string dateOfBirth;
    public string profilePictureUrl;
    public string occupation;
    public string friendStatus;
    public string numberId;
    public string isLoginStatus;
    public string timeZone;
}

public enum friendsListType
{
    GetMembersList,
    GetFriendsDetails,
    GetFriendRequestDetails
}
public enum searchByFilter
{
    FirstName,
    UserName,
    email
}