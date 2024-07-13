using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class FriendsElementsManager : MonoBehaviour
{
    [SerializeField]
    private Image profilePic;

    [SerializeField]
    private Sprite defaultPic;

    public TMP_Text userNameTxt;

    public FriendStatus friendStatus;

    public Button[] buttonCollections;

    public string profileImgConvertion;

    public friendsDetail friendsDetail;

    public GameObject responseButtonGroup;

    public GameObject ButtonGroup;

    private void Start()
    {
        SetProfileElement();
    }

    void SetProfileElement()
    {
        foreach (Button item in buttonCollections)
        {
            item.gameObject.SetActive(false);
        }

        responseButtonGroup.SetActive(false);

        StartCoroutine(GetTexture(friendsDetail.profilePictureUrl));

        userNameTxt.text = friendsDetail.firstName + " " + friendsDetail.lastName;
        //userNameTxt.text = friendsDetail.userName;

        foreach (Button item in buttonCollections)
        {
            item.gameObject.SetActive(false);
        }
        Debug.Log("friendsDetail.friendStatus  " + friendsDetail.friendStatus);
        switch (friendsDetail.friendStatus)
        {
            case "Not a friend":
                buttonCollections[(int)FriendStatus.Not_A_Friend].gameObject.SetActive(true);
                break;
            case "Accepted":
                buttonCollections[(int)FriendStatus.Request_Accepted].gameObject.SetActive(true);
                break;
            case "Rejected":
                buttonCollections[(int)FriendStatus.Request_Rejected].gameObject.SetActive(true);
                break;
            case "Request_Sent":
                Debug.Log("MainMenuCotroller.instance._friendsListType  " + MainMenuCotroller.instance._friendsListType);
                switch (MainMenuCotroller.instance._friendsListType)
                {
                    case friendsListType.GetFriendsDetails:
                        buttonCollections[(int)FriendStatus.Request_Sent].gameObject.SetActive(true);
                        break;
                    case friendsListType.GetFriendRequestDetails:
                        ResponseAction();
                        break;
                    case friendsListType.GetMembersList:
                        buttonCollections[(int)FriendStatus.Request_Sent].gameObject.SetActive(true);
                        break;
                }
                break;
            case "Friend":
                buttonCollections[((int)FriendStatus.videoCall) + 1].gameObject.SetActive(true);
                break;
        }
    }
    IEnumerator GetTexture(string url)
    {
        if (url.Length > 1)
        {
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Thumbnail : " + url);
                Debug.LogError("Thumbnail " + www.error);
                profilePic.sprite = defaultPic;
                //Destroy(gameObject);
            }
            else
            {
                Texture2D myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                Sprite mySprite = Sprite.Create(myTexture, new Rect(0.0f, 0.0f, myTexture.width, myTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
                profilePic.sprite = mySprite;
            }
        }
    }

    public void AddFriendAction()
    {
        foreach (Button item in buttonCollections)
        {
            item.gameObject.SetActive(false);
        }

        StartCoroutine(WebSericesManager.instance.SendFriendRequest(MainMenuCotroller.instance._userDetails.apiId, friendsDetail.friendId,
            WebSericesManager.instance.authToken, (SendRequestResponseData) =>
                {
                    switch (SendRequestResponseData._apiResponseType)
                    {
                        case apiResponseType.SUCCESS:

                            Debug.Log("Success");

                            switch (SendRequestResponseData.statusStr)
                            {
                                case "Not a friend":
                                    buttonCollections[(int)FriendStatus.Not_A_Friend].gameObject.SetActive(true);
                                    break;
                                case "Accepted":
                                    buttonCollections[(int)FriendStatus.Request_Accepted].gameObject.SetActive(true);
                                    break;
                                case "Rejected":
                                    buttonCollections[(int)FriendStatus.Request_Rejected].gameObject.SetActive(true);
                                    break;
                                case "Request_Sent":
                                    switch (MainMenuCotroller.instance._friendsListType)
                                    {
                                        case friendsListType.GetFriendsDetails:
                                            buttonCollections[(int)FriendStatus.Request_Sent].gameObject.SetActive(true);
                                            break;
                                        case friendsListType.GetFriendRequestDetails:
                                            buttonCollections[(int)FriendStatus.Request_Sent].gameObject.SetActive(true);
                                            break;
                                        case friendsListType.GetMembersList:
                                            buttonCollections[(int)FriendStatus.Request_Sent].gameObject.SetActive(true);
                                            break;
                                    }
                                    break;
                                case "Pending":
                                    buttonCollections[(int)FriendStatus.Request_Sent].gameObject.SetActive(true);
                                    break;
                            }

                            break;

                        case apiResponseType.FAIL:
                            Debug.LogWarning(SendRequestResponseData.responseMessage);

                            break;

                        case apiResponseType.SEVER_ERROR:
                            Debug.LogWarning(SendRequestResponseData.responseMessage);

                            break;
                    }
                }));
    }
    bool buttonStatus = true;
    public void ResponseAction()
    {
        responseButtonGroup.SetActive(true);
    }

    public void ResponseToRequest(string responseStr)
    {
        switch (responseStr)
        {
            case "Accept":
                StartCoroutine(WebSericesManager.instance.RespondToFriendRequest(friendsDetail.requestId, "Accept",
            WebSericesManager.instance.authToken, (SendRequestResponseData) =>
            {
                switch (SendRequestResponseData._apiResponseType)
                {
                    case apiResponseType.SUCCESS:

                        Debug.Log("Success");
                        foreach (Button item in buttonCollections)
                        {
                            item.gameObject.SetActive(false);
                        }
                        switch (SendRequestResponseData.statusStr)
                        {
                            case "Not a friend":
                                buttonCollections[1].gameObject.SetActive(true);
                                break;
                            case "Accepted":
                                buttonCollections[0].gameObject.SetActive(true);
                                break;
                            case "Rejected":
                                buttonCollections[1].gameObject.SetActive(true);
                                break;
                            case "Request_Sent":
                                buttonCollections[0].gameObject.SetActive(true);
                                break;
                            case "Pending":
                                buttonCollections[1].gameObject.SetActive(true);
                                break;
                            default:
                                buttonCollections[0].gameObject.SetActive(true);
                                break;
                        }
                        responseButtonGroup.SetActive(false);
                        ButtonGroup.SetActive(true);
                        break;

                    case apiResponseType.FAIL:
                        Debug.LogWarning(SendRequestResponseData.responseMessage);

                        break;

                    case apiResponseType.SEVER_ERROR:
                        Debug.LogWarning(SendRequestResponseData.responseMessage);

                        break;
                }
            }));
                break;

            case "Rejet":

                StartCoroutine(WebSericesManager.instance.RespondToFriendRequest(friendsDetail.requestId, "Reject",
           WebSericesManager.instance.authToken, (SendRequestResponseData) =>
           {
               switch (SendRequestResponseData._apiResponseType)
               {
                   case apiResponseType.SUCCESS:
                       Debug.Log("Success");
                       foreach (Button item in buttonCollections)
                       {
                           item.gameObject.SetActive(false);
                       }
                       switch (SendRequestResponseData.statusStr)
                       {
                           case "Not a friend":
                               buttonCollections[1].gameObject.SetActive(true);
                               break;
                           case "Accepted":
                               buttonCollections[0].gameObject.SetActive(true);
                               break;
                           case "Rejected":
                               buttonCollections[1].gameObject.SetActive(true);
                               break;
                           case "Request_Sent":
                               buttonCollections[0].gameObject.SetActive(true);
                               break;
                           case "Pending":
                               buttonCollections[1].gameObject.SetActive(true);
                               break;
                           default:
                               buttonCollections[0].gameObject.SetActive(true);
                               break;
                       }
                       responseButtonGroup.SetActive(false);
                       ButtonGroup.SetActive(true);
                       break;

                   case apiResponseType.FAIL:
                       Debug.LogWarning(SendRequestResponseData.responseMessage);
                       break;

                   case apiResponseType.SEVER_ERROR:
                       Debug.LogWarning(SendRequestResponseData.responseMessage);
                       break;
               }
           }));

                break;
        }
    }
}
