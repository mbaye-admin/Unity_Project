using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq.Expressions;
using System.Net;
using Unity.VisualScripting;


//using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static Cinemachine.CinemachineTriggerAction.ActionSettings;

public class WebSericesManager : MonoBehaviour
{
    public static WebSericesManager instance = null;

    public static WebSericesManager Instance
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
        DontDestroyOnLoad(this.gameObject);
    }

    private static string urlHeader = "https://dev.api.mbaye.com"; //Development URL

    public string authToken;

    [SerializeField]
    private string homePageDataURL = urlHeader + "/api/data/homepages";

    [SerializeField]
    private string getGenderURL = urlHeader + "/api/common/gender";

    [SerializeField]
    private string getCountryNameURL = urlHeader + "/api/common/countries";

    [SerializeField]
    private string getStateNamesURL = urlHeader + "/api/common/states";

    [SerializeField]
    private string getCityNameURL = urlHeader + "/api/common/cities";

    [SerializeField]
    private string checkUniqueURL = urlHeader + "/api/users/check-uniqueness";

    [SerializeField]
    private string registrationURL = urlHeader + "/api/users/register";

    [SerializeField]
    private string loginURL = urlHeader + "/api/users/login";

    [SerializeField]
    private string getUserDetailsByEmail = urlHeader + "/api/users";

    [SerializeField]
    private string getFriendsByFilter = urlHeader + "/api/users/search";

    [SerializeField]
    private string sendFriendRequest = urlHeader + "/api/friend-requests/send";

    [SerializeField]
    private string getFriendsList = urlHeader + "/api/friend-requests/friend-list";

    [SerializeField]
    private string getFriendsRequestList = urlHeader + "/api/friend-requests";

    [SerializeField]
    private string respondToFriendRequest = urlHeader + "/api/friend-requests/process";

    public IEnumerator GetHomePageData(Action<HomePageUrlAPI_Response> callBack)
    {
        HomePageUrlAPI_Response _getAllMessageResponse = new HomePageUrlAPI_Response();
        Debug.Log("homePageData  :  " + homePageDataURL);
        //UnityWebRequest apiRequest = UnityWebRequest.Post(homePageData,"");
        UnityWebRequest apiRequest = UnityWebRequest.PostWwwForm(homePageDataURL, "");
        //apiRequest.SetRequestHeader("Authorization", "Bearer " + loginAccessToken);

        yield return apiRequest.SendWebRequest();

        if (apiRequest.result != UnityWebRequest.Result.Success)
        {
            _getAllMessageResponse._apiResponseType = apiResponseType.SEVER_ERROR;
            _getAllMessageResponse.responseMessage = apiRequest.error;
            callBack(_getAllMessageResponse);
            Debug.Log(apiRequest.error);
        }
        else
        {
            Debug.Log(apiRequest.downloadHandler.text);

            List<object> homePageElements = new List<object>();
            homePageElements = MiniJSON.Json.Deserialize(apiRequest.downloadHandler.text) as List<object>;

            _getAllMessageResponse.homePageURLContents = new List<HomePageURLContent>();

            foreach (object element in homePageElements)
            {
                Dictionary<string, object> homePageUrls = new Dictionary<string, object>();
                homePageUrls = element as Dictionary<string, object>;

                HomePageURLContent _HomePageURLContent = new HomePageURLContent();

                _HomePageURLContent.Name = homePageUrls["Name"].ToString();
                _HomePageURLContent.videoUrl = homePageUrls["Video"].ToString();
                _HomePageURLContent.thubnailImgUrl = homePageUrls["Picture"].ToString();
                _HomePageURLContent.haloPictureUrl = homePageUrls["HaloPicture"].ToString();

                _getAllMessageResponse.homePageURLContents.Add(_HomePageURLContent);
            }

            _getAllMessageResponse._apiResponseType = apiResponseType.SUCCESS;
            _getAllMessageResponse.responseMessage = "";
            callBack(_getAllMessageResponse);
        }
    }
    public IEnumerator GetGenderDropDownData(Action<GenderList_Response> callBack)
    {
        GenderList_Response _getGenderDataResponse = new GenderList_Response();
        Debug.Log("Gender URL Data  :  " + getGenderURL);
        UnityWebRequest apiRequest = UnityWebRequest.Get(getGenderURL);
        //apiRequest.SetRequestHeader("Authorization", "Bearer " + loginAccessToken);

        yield return apiRequest.SendWebRequest();

        if (apiRequest.result != UnityWebRequest.Result.Success)
        {
            _getGenderDataResponse._apiResponseType = apiResponseType.SEVER_ERROR;
            _getGenderDataResponse.responseMessage = apiRequest.error;
            callBack(_getGenderDataResponse);
            Debug.Log(apiRequest.error);
        }
        else
        {
            Debug.Log("Gender Response : " + apiRequest.downloadHandler.text);

            List<object> genderResponseData = new List<object>();
            genderResponseData = MiniJSON.Json.Deserialize(apiRequest.downloadHandler.text) as List<object>;

            _getGenderDataResponse.genderList = new List<string>();
            _getGenderDataResponse.genderId = new List<string>();

            foreach (object element in genderResponseData)
            {
                Dictionary<string, object> genderNameList = new Dictionary<string, object>();
                genderNameList = element as Dictionary<string, object>;

                Debug.Log("_HomePageURLContent.Name   " + genderNameList["name"].ToString());

                _getGenderDataResponse.genderList.Add(genderNameList["name"].ToString());
                _getGenderDataResponse.genderId.Add(genderNameList["_id"].ToString());
            }

            _getGenderDataResponse._apiResponseType = apiResponseType.SUCCESS;
            _getGenderDataResponse.responseMessage = "";
            callBack(_getGenderDataResponse);
        }
    }

    public IEnumerator GetCountryDropDownData(Action<CountryList_Response> callBack)
    {
        CountryList_Response _getCountryDataResponse = new CountryList_Response();
        Debug.Log("Gender URL Data  :  " + getCountryNameURL);
        UnityWebRequest apiRequest = UnityWebRequest.Get(getCountryNameURL);
        //apiRequest.SetRequestHeader("Authorization", "Bearer " + loginAccessToken);

        yield return apiRequest.SendWebRequest();

        if (apiRequest.result != UnityWebRequest.Result.Success)
        {
            _getCountryDataResponse._apiResponseType = apiResponseType.SEVER_ERROR;
            _getCountryDataResponse.responseMessage = apiRequest.error;
            callBack(_getCountryDataResponse);
            Debug.Log(apiRequest.error);
        }
        else
        {
            Debug.Log("Gender Response : " + apiRequest.downloadHandler.text);

            Dictionary<string, object> countryResponseData = new Dictionary<string, object>();
            countryResponseData = MiniJSON.Json.Deserialize(apiRequest.downloadHandler.text) as Dictionary<string, object>;

            _getCountryDataResponse.countryList = new List<string>();

            List<object> countryList = countryResponseData["Countries"] as List<object>;

            foreach (object item in countryList)
            {
                Dictionary<string, object> countrySublistData = new Dictionary<string, object>();
                countrySublistData = item as Dictionary<string, object>;

                Debug.Log("Country Name " + countrySublistData["name"]);

                _getCountryDataResponse.countryList.Add(countrySublistData["name"].ToString());
            }

            _getCountryDataResponse._apiResponseType = apiResponseType.SUCCESS;
            _getCountryDataResponse.responseMessage = "";
            callBack(_getCountryDataResponse);
        }
    }

    public IEnumerator GetStateDropDownData(string countryName, Action<StateList_Response> callBack)
    {
        StateList_Response _getStateDataResponse = new StateList_Response();
        Debug.Log("Gender URL Data  :  " + getStateNamesURL + "/" + countryName);
        UnityWebRequest apiRequest = UnityWebRequest.Get(getStateNamesURL + "/" + countryName);
        //apiRequest.SetRequestHeader("Authorization", "Bearer " + loginAccessToken);

        yield return apiRequest.SendWebRequest();

        if (apiRequest.result != UnityWebRequest.Result.Success)
        {
            _getStateDataResponse._apiResponseType = apiResponseType.SEVER_ERROR;
            _getStateDataResponse.responseMessage = apiRequest.error;
            callBack(_getStateDataResponse);
            Debug.Log(apiRequest.error);
        }
        else
        {
            Debug.Log("Gender Response : " + apiRequest.downloadHandler.text);

            Dictionary<string, object> stateResponseData = new Dictionary<string, object>();
            stateResponseData = MiniJSON.Json.Deserialize(apiRequest.downloadHandler.text) as Dictionary<string, object>;

            _getStateDataResponse.stateList = new List<string>();

            List<object> stateList = stateResponseData["States"] as List<object>;

            foreach (object item in stateList)
            {
                Dictionary<string, object> stateSublistData = new Dictionary<string, object>();
                stateSublistData = item as Dictionary<string, object>;

                Debug.Log("Country Name " + stateSublistData["name"]);

                _getStateDataResponse.stateList.Add(stateSublistData["name"].ToString());
            }

            _getStateDataResponse._apiResponseType = apiResponseType.SUCCESS;
            _getStateDataResponse.responseMessage = "";
            callBack(_getStateDataResponse);
        }
    }

    public IEnumerator GetCityDropDownData(string cityName, Action<CityList_Response> callBack)
    {
        CityList_Response _getCityDataResponse = new CityList_Response();
        Debug.Log("City URL Data  :  " + getCityNameURL);
        UnityWebRequest apiRequest = UnityWebRequest.Get(getCityNameURL + "/" + cityName);
        //apiRequest.SetRequestHeader("Authorization", "Bearer " + loginAccessToken);

        yield return apiRequest.SendWebRequest();

        if (apiRequest.result != UnityWebRequest.Result.Success)
        {
            _getCityDataResponse._apiResponseType = apiResponseType.SEVER_ERROR;
            _getCityDataResponse.responseMessage = apiRequest.error;
            callBack(_getCityDataResponse);
            Debug.Log(apiRequest.error);
        }
        else
        {
            Debug.Log("City Response : " + apiRequest.downloadHandler.text);

            Dictionary<string, object> cityResponseData = new Dictionary<string, object>();
            cityResponseData = MiniJSON.Json.Deserialize(apiRequest.downloadHandler.text) as Dictionary<string, object>;

            _getCityDataResponse.cityList = new List<string>();

            List<object> stateList = cityResponseData["Cities"] as List<object>;

            foreach (object item in stateList)
            {
                Dictionary<string, object> stateSublistData = new Dictionary<string, object>();
                stateSublistData = item as Dictionary<string, object>;

                Debug.Log("City Name " + stateSublistData["name"]);

                _getCityDataResponse.cityList.Add(stateSublistData["name"].ToString());
            }

            _getCityDataResponse._apiResponseType = apiResponseType.SUCCESS;
            _getCityDataResponse.responseMessage = "";
            callBack(_getCityDataResponse);
        }
    }

    public IEnumerator CheckUniqueFields(string fieldName, string fieldValue, Action<Unique_Response> callBack)
    {
        Unique_Response _uniqueFieldCheck = new Unique_Response();
        Debug.Log("Unique URL Data  :  " + checkUniqueURL);

        Dictionary<string, string> urlHeaderKeys = new Dictionary<string, string>();
        urlHeaderKeys.Add("field", fieldName);
        urlHeaderKeys.Add("value", fieldValue);

        UnityWebRequest apiRequest = UnityWebRequest.Post(checkUniqueURL, urlHeaderKeys);
        //apiRequest.SetRequestHeader("Authorization", "Bearer " + loginAccessToken);

        yield return apiRequest.SendWebRequest();

        if (apiRequest.result != UnityWebRequest.Result.Success)
        {
            _uniqueFieldCheck._apiResponseType = apiResponseType.SEVER_ERROR;
            _uniqueFieldCheck.responseMessage = apiRequest.error;
            callBack(_uniqueFieldCheck);
            Debug.Log(apiRequest.error);
        }
        else
        {
            Debug.Log("City Response : " + apiRequest.downloadHandler.text);

            Dictionary<string, object> cityResponseData = new Dictionary<string, object>();
            cityResponseData = MiniJSON.Json.Deserialize(apiRequest.downloadHandler.text) as Dictionary<string, object>;

            _uniqueFieldCheck.uniqueStatus = (bool)cityResponseData["isUnique"];



            _uniqueFieldCheck._apiResponseType = apiResponseType.SUCCESS;
            _uniqueFieldCheck.responseMessage = "";
            callBack(_uniqueFieldCheck);
        }
    }

    public IEnumerator RegisterNewUser(string firstName, string lastName, string userName, string email,
        string password, string confirmPassword, string referenceCode, string idNumber,
        string occupation, string mobileNumber, string cityName, string countryName, string stateName, string genderId, string adress, string school,
        string dateOfBirth, string timeZone, Sprite profilepic, string profilePicture, Action<GetUserDataResponse> callBack)
    {
        GetUserDataResponse _registrationDataCheck = new GetUserDataResponse();
        Debug.Log("registration URL  :  " + registrationURL);

        WWWForm form = new WWWForm();
        form.headers["Content-Type"] = "application/json";
        form.headers["Accept"] = "*/*";
        form.AddField("firstName", firstName);
        form.AddField("lastName", lastName);
        form.AddField("timezone", "EST");
        form.AddField("userName", userName);
        form.AddField("email", email);
        form.AddField("password", password);
        form.AddField("confirmPassword", confirmPassword);

        form.AddField("referenceCode", referenceCode);
        form.AddField("idNumber", idNumber);
        form.AddField("occupation", occupation);
        form.AddField("mobileNumber", mobileNumber);

        form.AddField("cityName", cityName);
        form.AddField("countryName", countryName);
        form.AddField("stateName", stateName);
        form.AddField("genderId", genderId);

        form.AddField("address", adress);
        form.AddField("school", school);
        form.AddField("dateOfBirth", dateOfBirth);
        form.AddField("profilePicture", profilePicture);

        UnityWebRequest apiRequest = UnityWebRequest.Post(registrationURL, form);
        //apiRequest.SetRequestHeader("Content-Type", "application/json");
        //apiRequest.SetRequestHeader("Accept", "*/*");

        yield return apiRequest.SendWebRequest();

        if (apiRequest.result != UnityWebRequest.Result.Success)
        {
            _registrationDataCheck._apiResponseType = apiResponseType.SEVER_ERROR;
            _registrationDataCheck.responseMessage = apiRequest.error;
            callBack(_registrationDataCheck);
            Debug.Log(apiRequest.error);
        }
        else
        {
            Debug.Log("Download Response : " + apiRequest.downloadHandler.text);

            Dictionary<string, object> registrationResponseData = new Dictionary<string, object>();
            registrationResponseData = MiniJSON.Json.Deserialize(apiRequest.downloadHandler.text) as Dictionary<string, object>;

            if (registrationResponseData["success"].ToString() == "true")
            {
                UserDetails _UserDetails = new UserDetails();

                _UserDetails.firstName = firstName;
                _UserDetails.lastName = lastName;
                _UserDetails.userName = userName;
                _UserDetails.email = email;
                _UserDetails.password = password;
                _UserDetails.referenceCode = referenceCode;
                _UserDetails.cityName = cityName;
                _UserDetails.countryName = countryName;
                _UserDetails.stateName = stateName;
                _UserDetails.genderId = genderId;
                _UserDetails.idNumber = idNumber;
                _UserDetails.adress = adress;
                _UserDetails.school = school;
                _UserDetails.mobileNumber = mobileNumber;
                _UserDetails.dateOfBirth = dateOfBirth;
                _UserDetails.profilePicUrl = profilePicture;
                _UserDetails.occupation = occupation;
                _UserDetails.status = "";
                _UserDetails.is_login = "";
                _UserDetails.timezone = timeZone;
                _UserDetails.profleSprite = profilepic;

                MainMenuCotroller.instance._userDetails = _UserDetails;

            }
            //_registrationDataCheck.userDetails = new UserDetails();

            //List<object> RegistrationList = new List<object>();
            //RegistrationList = registrationResponseData["data"] as List<object>;

            //foreach (object item in RegistrationList)
            //{
            //    Dictionary<object, string> registrationSublistData = new Dictionary<object, string>();
            //    registrationSublistData = item as Dictionary<object, string>;

            //    UserDetails _UserDetails = new UserDetails();

            //    _UserDetails.firstName = registrationSublistData["firstName"].ToString();
            //    _UserDetails.lastName = registrationSublistData["lastName"].ToString();
            //    _UserDetails.userName = registrationSublistData["userName"].ToString();
            //    _UserDetails.email = registrationSublistData["email"].ToString();
            //    _UserDetails.password = registrationSublistData["password"].ToString();
            //    _UserDetails.referenceCode = registrationSublistData["referenceCode"].ToString();
            //    _UserDetails.cityName = registrationSublistData["cityName"].ToString();
            //    _UserDetails.countryName = registrationSublistData["countryName"].ToString();
            //    _UserDetails.stateName = registrationSublistData["stateName"].ToString();
            //    _UserDetails.genderId = registrationSublistData["genderId"].ToString();
            //    _UserDetails.idNumber = registrationSublistData["idNumber"].ToString();
            //    _UserDetails.adress = registrationSublistData["address"].ToString();
            //    _UserDetails.school = registrationSublistData["school"].ToString();
            //    _UserDetails.mobileNumber = registrationSublistData["mobileNumber"].ToString();
            //    _UserDetails.dateOfBirth = registrationSublistData["dateOfBirth"].ToString();
            //    _UserDetails.profilePicUrl = registrationSublistData["profilePicture"].ToString();
            //    _UserDetails.occupation = registrationSublistData["occupation"].ToString();
            //    _UserDetails.status = registrationSublistData["status"].ToString();
            //    _UserDetails.is_login = registrationSublistData["is_login"].ToString();
            //    _UserDetails.timezone = registrationSublistData["timezone"].ToString();

            //    _registrationDataCheck.userDetails = _UserDetails;
            //}


            _registrationDataCheck._apiResponseType = apiResponseType.SUCCESS;
            _registrationDataCheck.responseMessage = "";
            callBack(_registrationDataCheck);
        }
    }

    public IEnumerator LoginData(string emailId, string password, Action<LoginData> callBack)
    {
        LoginData _loginDataCheck = new LoginData();
        //Debug.Log("registration URL  :  " + loginURL);

        //Debug.Log("emailId  :  " + emailId);
        //Debug.Log("password  :  " + password);

        Dictionary<string, string> urlHeaderKeys = new Dictionary<string, string>();
        urlHeaderKeys.Add("email", emailId);
        urlHeaderKeys.Add("password", password);

        UnityWebRequest apiRequest = UnityWebRequest.Post(loginURL, urlHeaderKeys);
        //apiRequest.SetRequestHeader("Content-Type", "application/json");
        //apiRequest.SetRequestHeader("Accept", "*/*");

        yield return apiRequest.SendWebRequest();

        if (apiRequest.result != UnityWebRequest.Result.Success)
        {
            _loginDataCheck._apiResponseType = apiResponseType.SEVER_ERROR;
            _loginDataCheck.responseMessage = apiRequest.error;
            callBack(_loginDataCheck);
            Debug.Log(apiRequest.error);
        }
        else
        {
            Debug.Log("Download Response : " + apiRequest.downloadHandler.text);

            Dictionary<string, object> loginResponseData = new Dictionary<string, object>();
            loginResponseData = MiniJSON.Json.Deserialize(apiRequest.downloadHandler.text) as Dictionary<string, object>;

            Debug.Log("loginResponseData[\"success\"].ToString() : " + loginResponseData["success"].ToString());
            if (loginResponseData["success"].ToString() == "True")
            {
                Debug.Log("Success token : " + loginResponseData["token"].ToString());
                _loginDataCheck.userToken = loginResponseData["token"].ToString();

                _loginDataCheck._apiResponseType = apiResponseType.SUCCESS;
                _loginDataCheck.responseMessage = "";
            }
            else
            {
                _loginDataCheck._apiResponseType = apiResponseType.SEVER_ERROR;
                _loginDataCheck.responseMessage = "API Error";
            }
            callBack(_loginDataCheck);
        }
    }
    public IEnumerator GetUserDetailsByMail(string emailId, string authToken, Action<GetUserDataResponse> callBack)
    {
        GetUserDataResponse _getUserDetailsResponse = new GetUserDataResponse();
        Debug.Log("Unique URL Data  :  " + getUserDetailsByEmail);

        UnityWebRequest apiRequest = UnityWebRequest.Get(getUserDetailsByEmail + "/" + emailId);
        apiRequest.SetRequestHeader("authorization", authToken);

        yield return apiRequest.SendWebRequest();

        if (apiRequest.result != UnityWebRequest.Result.Success)
        {
            _getUserDetailsResponse._apiResponseType = apiResponseType.SEVER_ERROR;
            _getUserDetailsResponse.responseMessage = apiRequest.error;
            callBack(_getUserDetailsResponse);
            Debug.Log(apiRequest.error);
        }
        else
        {
            Debug.Log("GetUserDetailsByMail : " + apiRequest.downloadHandler.text);

            Dictionary<string, object> getUserDetailsResponse = new Dictionary<string, object>();
            getUserDetailsResponse = MiniJSON.Json.Deserialize(apiRequest.downloadHandler.text) as Dictionary<string, object>;

            UserDetails _UserDetails = new UserDetails();

            _UserDetails.apiId = getUserDetailsResponse["_id"].ToString();

            _UserDetails.firstName = getUserDetailsResponse["firstName"].ToString();
            _UserDetails.lastName = getUserDetailsResponse["lastName"].ToString();
            _UserDetails.userName = getUserDetailsResponse["userName"].ToString();
            _UserDetails.email = getUserDetailsResponse["email"].ToString();
            _UserDetails.password = "";
            _UserDetails.referenceCode = getUserDetailsResponse["referenceCode"].ToString();
            _UserDetails.cityName = getUserDetailsResponse["cityName"].ToString();
            _UserDetails.countryName = getUserDetailsResponse["countryName"].ToString();
            _UserDetails.stateName = getUserDetailsResponse["stateName"].ToString();

            _UserDetails.genderId = getUserDetailsResponse["genderId"].ToString();

            _UserDetails.idNumber = getUserDetailsResponse["idNumber"].ToString();
            _UserDetails.adress = getUserDetailsResponse["address"].ToString();
            _UserDetails.school = getUserDetailsResponse["school"].ToString();
            _UserDetails.mobileNumber = getUserDetailsResponse["mobileNumber"].ToString();
            _UserDetails.dateOfBirth = getUserDetailsResponse["dateOfBirth"].ToString();
            _UserDetails.profilePicUrl = getUserDetailsResponse["profilePicture"].ToString();
            _UserDetails.occupation = getUserDetailsResponse["occupation"].ToString();
            _UserDetails.status = getUserDetailsResponse["status"].ToString();
            _UserDetails.is_login = getUserDetailsResponse["is_login"].ToString();
            _UserDetails.timezone = getUserDetailsResponse["timezone"].ToString();

            MainMenuCotroller.instance._userDetails = _UserDetails;

            _getUserDetailsResponse._apiResponseType = apiResponseType.SUCCESS;
            _getUserDetailsResponse.responseMessage = "";
            callBack(_getUserDetailsResponse);
        }
    }

    public IEnumerator GetMembersByFilter(string userId, string searchInput, searchByFilter _searchByFilter,
        string authToken, int pageNumber, Action<GetMembersDataResponse> callBack)
    {
        GetMembersDataResponse _getMembersDetailsResponse = new GetMembersDataResponse();
        Debug.Log("getFriendsByFilter URL Data  :  " + getFriendsByFilter);

        Dictionary<string, string> urlHeaderKeys = new Dictionary<string, string>();
        urlHeaderKeys.Add("userId", userId);

        switch (_searchByFilter)
        {
            case searchByFilter.FirstName:

                urlHeaderKeys.Add("email", "");
                urlHeaderKeys.Add("firstName", searchInput);
                urlHeaderKeys.Add("userName", "");
                break;

            case searchByFilter.UserName:
                urlHeaderKeys.Add("email", "");
                urlHeaderKeys.Add("firstName", "");
                urlHeaderKeys.Add("userName", searchInput);
                break;

            case searchByFilter.email:
                urlHeaderKeys.Add("email", searchInput);
                urlHeaderKeys.Add("firstName", "");
                urlHeaderKeys.Add("userName", "");
                break;
        }

        urlHeaderKeys.Add("page", pageNumber.ToString());
        urlHeaderKeys.Add("limit", MainMenuCotroller.instance._FindFriendsManager.elementLimit.ToString());

        UnityWebRequest apiRequest = UnityWebRequest.Post(getFriendsByFilter, urlHeaderKeys);
        apiRequest.SetRequestHeader("authorization", authToken);

        yield return apiRequest.SendWebRequest();

        if (apiRequest.result != UnityWebRequest.Result.Success)
        {
            _getMembersDetailsResponse._apiResponseType = apiResponseType.SEVER_ERROR;
            _getMembersDetailsResponse.responseMessage = apiRequest.error;
            callBack(_getMembersDetailsResponse);
            Debug.Log(apiRequest.error);
        }
        else
        {
            //string path = "Assets/Resources/test.txt";

            //StreamReader inp_stm = new StreamReader(path);

            //while (!inp_stm.EndOfStream)
            //{
            //     inp_ln = inp_stm.ReadLine();
            //    // Do Something with the input. 
            //}

            //inp_stm.Close();

            Debug.Log("GetUserDetailsByMail : " + apiRequest.downloadHandler.text);

            Dictionary<string, object> getUserDetailsResponse = new Dictionary<string, object>();
            getUserDetailsResponse = MiniJSON.Json.Deserialize(apiRequest.downloadHandler.text) as Dictionary<string, object>;
            //getUserDetailsResponse = MiniJSON.Json.Deserialize(inp_ln) as Dictionary<string, object>;



            Debug.Log(getUserDetailsResponse["success"]);

            Dictionary<string, object> innerDict = new Dictionary<string, object>();
            innerDict = getUserDetailsResponse["searchResults"] as Dictionary<string, object>;

            Debug.Log("totalResults  " + innerDict["totalResults"]);

            float result;
            if (float.TryParse(innerDict["totalResults"].ToString(), out result))
            {
                MainMenuCotroller.instance._FindFriendsManager.pageCount = (result / MainMenuCotroller.instance._FindFriendsManager.elementLimit);
            }

            List<object> innerDict2 = new List<object>();
            innerDict2 = innerDict["results"] as List<object>;

            // MainMenuCotroller.instance._FindFriendsManager.friendsDetails = new List<friendsDetail>();
            _getMembersDetailsResponse.membersDetails = new List<friendsDetail>();

            foreach (object item in innerDict2)
            {
                friendsDetail friendsDetail = new friendsDetail();

                Dictionary<string, object> innerDict3 = new Dictionary<string, object>();
                innerDict3 = item as Dictionary<string, object>;

                Debug.Log(innerDict3["firstName"]);

                friendsDetail.friendId = innerDict3["_id"].ToString();
                friendsDetail.firstName = innerDict3["firstName"].ToString();
                friendsDetail.lastName = innerDict3["lastName"].ToString();
                friendsDetail.userName = innerDict3["userName"].ToString();
                friendsDetail.email = innerDict3["email"].ToString();
                friendsDetail.referenceCode = innerDict3["referenceCode"].ToString();
                friendsDetail.cityName = innerDict3["cityName"].ToString();

                friendsDetail.genderName = innerDict3["genderId"].ToString();

                //friendsDetail.address = innerDict3["address"].ToString();
                //friendsDetail.school = innerDict3["school"].ToString();
                //friendsDetail.mobileNumber = innerDict3["mobileNumber"].ToString();
                friendsDetail.dateOfBirth = innerDict3["dateOfBirth"].ToString();
                friendsDetail.profilePictureUrl = innerDict3["profilePicture"].ToString();
                //friendsDetail.occupation = innerDict3["occupation"].ToString();
                friendsDetail.friendStatus = innerDict3["status"].ToString();
                friendsDetail.numberId = innerDict3["numberId"].ToString();
                friendsDetail.isLoginStatus = innerDict3["is_login"].ToString();
                friendsDetail.timeZone = innerDict3["timezone"].ToString();

                _getMembersDetailsResponse.membersDetails.Add(friendsDetail);
            }



            _getMembersDetailsResponse._apiResponseType = apiResponseType.SUCCESS;
            _getMembersDetailsResponse.responseMessage = "";
            callBack(_getMembersDetailsResponse);
        }
    }
    public IEnumerator SendFriendRequest(string senderId, string reciverId, string authToken, Action<GetFriendRequestResponse> callBack)
    {
        GetFriendRequestResponse _getMembersDetailsResponse = new GetFriendRequestResponse();
        Debug.Log("respondToFriendRequest URL Data  :  " + sendFriendRequest);

        Dictionary<string, string> urlHeaderKeys = new Dictionary<string, string>();
        urlHeaderKeys.Add("senderId", senderId);
        urlHeaderKeys.Add("recipientId", reciverId);

        UnityWebRequest apiRequest = UnityWebRequest.Post(sendFriendRequest, urlHeaderKeys);
        apiRequest.SetRequestHeader("authorization", authToken);

        yield return apiRequest.SendWebRequest();

        if (apiRequest.result != UnityWebRequest.Result.Success)
        {
            _getMembersDetailsResponse._apiResponseType = apiResponseType.SEVER_ERROR;
            _getMembersDetailsResponse.responseMessage = apiRequest.error;
            callBack(_getMembersDetailsResponse);
            Debug.Log(apiRequest.error);
        }
        else
        {
            Debug.Log("SendFriendRequest : " + apiRequest.downloadHandler.text);

            Dictionary<string, object> getRequestDetailsResponse = new Dictionary<string, object>();
            getRequestDetailsResponse = MiniJSON.Json.Deserialize(apiRequest.downloadHandler.text) as Dictionary<string, object>;

            Debug.Log(getRequestDetailsResponse["sender"]);

            _getMembersDetailsResponse.senderId = getRequestDetailsResponse["sender"].ToString();
            _getMembersDetailsResponse.recieverId = getRequestDetailsResponse["recipient"].ToString();
            _getMembersDetailsResponse.statusStr = getRequestDetailsResponse["status"].ToString();
            _getMembersDetailsResponse.responseIdStr = getRequestDetailsResponse["_id"].ToString();

            _getMembersDetailsResponse._apiResponseType = apiResponseType.SUCCESS;
            _getMembersDetailsResponse.responseMessage = "";
            callBack(_getMembersDetailsResponse);
        }
    }

    public IEnumerator GetFriendList(string userId, string reciverId, string authToken, Action<GetFriendsDataResponse> callBack)
    {
        GetFriendsDataResponse _getMembersDetailsResponse = new GetFriendsDataResponse();
        Debug.Log("GetFriendList URL Data  :  " + getFriendsList);

        Dictionary<string, string> urlHeaderKeys = new Dictionary<string, string>();
        urlHeaderKeys.Add("userId", userId);
        urlHeaderKeys.Add("firstName", reciverId);
        urlHeaderKeys.Add("userName", "");
        //urlHeaderKeys.Add("userName", "reciverId");

        UnityWebRequest apiRequest = UnityWebRequest.Post(getFriendsList, urlHeaderKeys);
        apiRequest.SetRequestHeader("authorization", authToken);

        yield return apiRequest.SendWebRequest();

        if (apiRequest.result != UnityWebRequest.Result.Success)
        {
            _getMembersDetailsResponse._apiResponseType = apiResponseType.SEVER_ERROR;
            _getMembersDetailsResponse.responseMessage = apiRequest.error;
            callBack(_getMembersDetailsResponse);
            Debug.Log(apiRequest.error);
        }
        else
        {
            Debug.Log("GetFriendList : " + apiRequest.downloadHandler.text);

            Dictionary<string, object> getUserDetailsResponse = new Dictionary<string, object>();
            getUserDetailsResponse = MiniJSON.Json.Deserialize(apiRequest.downloadHandler.text) as Dictionary<string, object>;

            Debug.Log(getUserDetailsResponse["success"]);

            List<object> innerDict2 = new List<object>();
            innerDict2 = getUserDetailsResponse["Friends"] as List<object>;

            _getMembersDetailsResponse.friendsDetails = new List<friendsDetail>();

            foreach (object item in innerDict2)
            {
                friendsDetail friendsDetail = new friendsDetail();

                Dictionary<string, object> innerDict3 = new Dictionary<string, object>();
                innerDict3 = item as Dictionary<string, object>;

                Debug.Log(innerDict3["firstName"]);

                friendsDetail.friendId = innerDict3["_id"].ToString();
                friendsDetail.firstName = innerDict3["firstName"].ToString();
                friendsDetail.lastName = innerDict3["lastName"].ToString();
                friendsDetail.userName = innerDict3["userName"].ToString();
                friendsDetail.email = innerDict3["email"].ToString();
                friendsDetail.referenceCode = innerDict3["referenceCode"].ToString();
                friendsDetail.cityName = innerDict3["cityName"].ToString();

                friendsDetail.genderName = innerDict3["genderId"].ToString();

                //friendsDetail.address = innerDict3["address"].ToString();
                //friendsDetail.school = innerDict3["school"].ToString();
                //friendsDetail.mobileNumber = innerDict3["mobileNumber"].ToString();
                friendsDetail.dateOfBirth = innerDict3["dateOfBirth"].ToString();
                friendsDetail.profilePictureUrl = innerDict3["profilePicture"].ToString();
                //friendsDetail.occupation = innerDict3["occupation"].ToString();
               
                friendsDetail.numberId = innerDict3["numberId"].ToString();
                friendsDetail.isLoginStatus = innerDict3["is_login"].ToString();
                friendsDetail.timeZone = innerDict3["timezone"].ToString();

                friendsDetail.friendStatus = "Friend";
                //friendsDetail.friendStatus = innerDict3["status"].ToString();


                _getMembersDetailsResponse.friendsDetails.Add(friendsDetail);
            }


            _getMembersDetailsResponse._apiResponseType = apiResponseType.SUCCESS;
            _getMembersDetailsResponse.responseMessage = "";
            callBack(_getMembersDetailsResponse);
        }
    }

    public IEnumerator GetFriendRquestSentList(string userId, string firstName, string authToken, Action<GetFriendRequestDataResponse> callBack)
    {
        GetFriendRequestDataResponse _gerFriendRequestDetails = new GetFriendRequestDataResponse();
        Debug.Log("GetFriendList URL Data  :  " + getFriendsRequestList);

        Dictionary<string, string> urlHeaderKeys = new Dictionary<string, string>();
        urlHeaderKeys.Add("firstName", firstName);

        UnityWebRequest apiRequest = UnityWebRequest.Post(getFriendsRequestList + "/" + userId, urlHeaderKeys);
        apiRequest.SetRequestHeader("authorization", authToken);

        yield return apiRequest.SendWebRequest();

        if (apiRequest.result != UnityWebRequest.Result.Success)
        {
            _gerFriendRequestDetails._apiResponseType = apiResponseType.SEVER_ERROR;
            _gerFriendRequestDetails.responseMessage = apiRequest.error;
            callBack(_gerFriendRequestDetails);
            Debug.Log(apiRequest.error);
        }
        else
        {

            Debug.Log(apiRequest.downloadHandler.text);

            Dictionary<string, object> getUserDetailsResponse = new Dictionary<string, object>();
            getUserDetailsResponse = MiniJSON.Json.Deserialize(apiRequest.downloadHandler.text) as Dictionary<string, object>;

            Debug.Log(getUserDetailsResponse["success"]);

            List<object> innerDict2 = new List<object>();
            innerDict2 = getUserDetailsResponse["Data"] as List<object>;

            _gerFriendRequestDetails.membersDetails = new List<friendsDetail>();

            foreach (object item in innerDict2)
            {
                friendsDetail _friendsDetail = new friendsDetail();

                Dictionary<string, object> Dict_2 = new Dictionary<string, object>();
                Dict_2 = item as Dictionary<string, object>;

                Dictionary<string, object> Dict_3 = new Dictionary<string, object>();
                Dict_3 = Dict_2["sender"] as Dictionary<string, object>;

                Debug.Log(Dict_3["firstName"]);

                _friendsDetail.friendId = Dict_3["_id"].ToString();
                _friendsDetail.firstName = Dict_3["firstName"].ToString();
                _friendsDetail.lastName = Dict_3["lastName"].ToString();
                _friendsDetail.userName = Dict_3["userName"].ToString();
                _friendsDetail.email = Dict_3["email"].ToString();
                _friendsDetail.referenceCode = Dict_3["referenceCode"].ToString();
                _friendsDetail.cityName = Dict_3["cityName"].ToString();
                _friendsDetail.countryName = Dict_3["countryName"].ToString();
                _friendsDetail.stateName = Dict_3["stateName"].ToString();
                _friendsDetail.genderId = Dict_3["genderId"].ToString();
                _friendsDetail.idNumber = Dict_3["idNumber"].ToString();
                _friendsDetail.address = Dict_3["address"].ToString();
                _friendsDetail.school = Dict_3["school"].ToString();
                _friendsDetail.mobileNumber = Dict_3["mobileNumber"].ToString();
                _friendsDetail.dateOfBirth = Dict_3["dateOfBirth"].ToString();
                _friendsDetail.profilePictureUrl = Dict_3["profilePicture"].ToString();
                _friendsDetail.occupation = Dict_3["occupation"].ToString();
                _friendsDetail.isLoginStatus = Dict_3["is_login"].ToString();
                _friendsDetail.timeZone = Dict_3["timezone"].ToString();
                _friendsDetail.numberId = Dict_3["numberId"].ToString();

                _friendsDetail.requestId= Dict_2["_id"].ToString();
                _friendsDetail.friendStatus = Dict_2["status"].ToString();

                _gerFriendRequestDetails.membersDetails.Add(_friendsDetail);
            }

            _gerFriendRequestDetails._apiResponseType = apiResponseType.SUCCESS;
            _gerFriendRequestDetails.responseMessage = getUserDetailsResponse["message"].ToString();
            callBack(_gerFriendRequestDetails);
        }
    }
    public IEnumerator RespondToFriendRequest(string requestId, string action, string authToken, Action<GetFriendRequestResponse> callBack)
    {
        GetFriendRequestResponse _getMembersDetailsResponse = new GetFriendRequestResponse();
        Debug.Log("respondToFriendRequest URL Data  :  " + respondToFriendRequest);
        Debug.Log("requestId  :  " + requestId);

        Dictionary<string, string> urlHeaderKeys = new Dictionary<string, string>();
        urlHeaderKeys.Add("requestId", requestId);
        urlHeaderKeys.Add("action", action);

        UnityWebRequest apiRequest = UnityWebRequest.Post(respondToFriendRequest, urlHeaderKeys);
        apiRequest.SetRequestHeader("authorization", authToken);

        yield return apiRequest.SendWebRequest();

        if (apiRequest.result != UnityWebRequest.Result.Success)
        {
            _getMembersDetailsResponse._apiResponseType = apiResponseType.SEVER_ERROR;
            _getMembersDetailsResponse.responseMessage = apiRequest.error;
            callBack(_getMembersDetailsResponse);
            Debug.Log(apiRequest.error);
        }
        else
        {
            Debug.Log("SendFriendRequest : " + apiRequest.downloadHandler.text);

            Dictionary<string, object> getRequestDetailsResponse = new Dictionary<string, object>();
            getRequestDetailsResponse = MiniJSON.Json.Deserialize(apiRequest.downloadHandler.text) as Dictionary<string, object>;

            Debug.Log(getRequestDetailsResponse["sender"]);

            _getMembersDetailsResponse.senderId = getRequestDetailsResponse["sender"].ToString();
            _getMembersDetailsResponse.recieverId = getRequestDetailsResponse["recipient"].ToString();
            _getMembersDetailsResponse.statusStr = getRequestDetailsResponse["status"].ToString();
            _getMembersDetailsResponse.responseIdStr = getRequestDetailsResponse["_id"].ToString();

            _getMembersDetailsResponse._apiResponseType = apiResponseType.SUCCESS;
            _getMembersDetailsResponse.responseMessage = "";
            callBack(_getMembersDetailsResponse);
        }
    }
}
public enum apiResponseType
{
    SUCCESS,
    FAIL,
    SEVER_ERROR
}
public class HomePageUrlAPI_Response
{
    public apiResponseType _apiResponseType;
    public string responseMessage;
    public string dataContent;

    public List<HomePageURLContent> homePageURLContents;
}
public class GenderList_Response
{
    public apiResponseType _apiResponseType;
    public string responseMessage;
    public string dataContent;

    public List<string> genderList;
    public List<string> genderId;
}
public class CountryList_Response
{
    public apiResponseType _apiResponseType;
    public string responseMessage;
    public string dataContent;

    public List<string> countryList;
}
public class StateList_Response
{
    public apiResponseType _apiResponseType;
    public string responseMessage;
    public string dataContent;

    public List<string> stateList;
}
public class CityList_Response
{
    public apiResponseType _apiResponseType;
    public string responseMessage;
    public string dataContent;

    public List<string> cityList;
}
public class Unique_Response
{
    public apiResponseType _apiResponseType;
    public string responseMessage;
    public string dataContent;

    public bool uniqueStatus;
}
public class GetUserDataResponse
{
    public apiResponseType _apiResponseType;
    public string responseMessage;
    public string dataContent;

    public UserDetails userDetails;
}
public class LoginData
{
    public apiResponseType _apiResponseType;
    public string responseMessage;
    public string dataContent;

    public string userToken;
}
public class GetFriendsDataResponse
{
    public apiResponseType _apiResponseType;
    public string responseMessage;
    public string dataContent;

    public List<friendsDetail> friendsDetails;
}
public class GetFriendRequestResponse
{
    public apiResponseType _apiResponseType;
    public string responseMessage;
    public string dataContent;

    public string senderId;
    public string recieverId;
    public string statusStr;
    public string responseIdStr;
}
public class GetMembersDataResponse
{
    public apiResponseType _apiResponseType;
    public string responseMessage;
    public string dataContent;

    public List<friendsDetail> membersDetails;
}
public class GetFriendRequestDataResponse
{
    public apiResponseType _apiResponseType;
    public string responseMessage;
    public string dataContent;

    public List<friendsDetail> membersDetails;
}