using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using SFB;
using System;
using UnityEngine.Analytics;

public class RegistrationManager : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField emailIdInput;

    [SerializeField]
    private TMP_InputField passwordInput;

    [SerializeField]
    private TMP_InputField confirmPasswordInput;

    [SerializeField]
    private TMP_InputField firstNameInput;

    [SerializeField]
    private TMP_InputField lastNameInput;

    [SerializeField]
    private TMP_InputField userNameInput;

    [SerializeField]
    private TMP_InputField mobileNumberInput;

    [SerializeField]
    private TMP_InputField IDNumberInput;

    [SerializeField]
    private TMP_InputField OccupationInput;

    [SerializeField]
    private TMP_InputField referenceCodeInput;

    [SerializeField]
    private Button nextButton;

    [SerializeField]
    private RegistrationProgressManager registrationProgressManager;

    [SerializeField]
    private const string MatchEmailPattern =
        @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
 + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
 + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
 + @"([a-zA-Z0-9]+[\w-]+\.)+[a-zA-Z]{1}[a-zA-Z0-9-]{1,23})$";

    [SerializeField]
    private Regex PassRegex = new Regex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$");
    [SerializeField]
    private Regex PhoneRegex = new Regex("^(?=.*\\d)[\\d\\s-+]{7,15}$");
    [SerializeField]
    private Regex NameRegex = new Regex("^.{1,50}$");

    [SerializeField]
    private List<bool> validationChecker;

    [SerializeField]
    private List<string> genderId;

    [SerializeField]
    private TMP_InputField DateOfBirthInput;

    [SerializeField]
    private TMP_Dropdown genderDropDown;

    [SerializeField]
    private TMP_Dropdown countryDropDown;

    [SerializeField]
    private TMP_Dropdown stateDropDown;

    [SerializeField]
    private TMP_Dropdown cityDropDown;

    [SerializeField]
    private TMP_InputField adressInput;

    [SerializeField]
    private TMP_InputField schoolInput;

    [SerializeField]
    private Button profileButton_Browser;
    [SerializeField]
    private Button profileButton_Editor;

    [SerializeField]
    private Image profileImage;

    [SerializeField]
    private string profileImgConvertion;

    [SerializeField]
    private Toggle agreeToTermsToogle;

    [SerializeField]
    public GameObject loginDetailsObj;
    [SerializeField]
    public GameObject profileDataObj;

    [SerializeField]
    private TMP_Text messageText_1;
    [SerializeField]
    private TMP_Text messageText_2;


#if UNITY_WEBGL && !UNITY_EDITOR
    // WebGL
    [DllImport("__Internal")]
    private static extern void UploadFile(string gameObjectName, string methodName, string filter, bool multiple);
#endif
    public void OnClickBrowser()
    {
        validationChecker[(int)ValidationData.profilePic] = false;
#if UNITY_WEBGL && !UNITY_EDITOR
        UploadFile(gameObject.name, "OnFileUpload", ".png, .jpg", false);
#endif
    }

    // Called from browser
    public void OnFileUpload(string url)
    {
        StartCoroutine(OutputRoutine(url));
    }

    // Standalone platforms & editor
    public void OnClick()
    {
        validationChecker[(int)ValidationData.profilePic] = true;
        var paths = StandaloneFileBrowser.OpenFilePanel("Title", "", "png,jpg", false);
        if (paths.Length > 0)
        {
            StartCoroutine(OutputRoutine(new System.Uri(paths[0]).AbsoluteUri));
        }
    }


    private IEnumerator OutputRoutine(string url)
    {
        var loader = new WWW(url);
        yield return loader;

        Sprite photoSprite = Sprite.Create(loader.texture, new Rect(0.0f, 0.0f, loader.texture.width, loader.texture.height), new Vector2(0.5f, 0.5f), 100.0f);

#if UNITY_WEBGL && !UNITY_EDITOR

        profileImage.sprite = photoSprite;
#else
        profileImage.sprite = photoSprite;
#endif

        validationChecker[(int)ValidationData.profilePic] = true;

        byte[] imgByte = photoSprite.texture.EncodeToJPG();
        profileImgConvertion = "data:image/png;base64," + Convert.ToBase64String(imgByte);
    }

    private void OnEnable()
    {
        ResetInputData();
    }
    void ResetInputData()
    {
        loginDetailsObj.gameObject.SetActive(true);
        profileDataObj.gameObject.SetActive(false);

#if UNITY_WEBGL && !UNITY_EDITOR
        profileButton_Editor.gameObject.SetActive(false);
        profileButton_Browser.gameObject.SetActive(true);

#else
        profileButton_Editor.gameObject.SetActive(true);
        profileButton_Browser.gameObject.SetActive(false);
#endif

        validationChecker = new List<bool>();

        for (int i = 0; i < Enum.GetValues(typeof(ValidationData)).Length; i++)
        {
            validationChecker.Add(false);
        }

        for (int i = 6; i < Enum.GetValues(typeof(ValidationData)).Length; i++)
        {
            validationChecker[i] = false;
        }

        emailIdInput.text = "";
        emailIdInput.transform.GetChild(0).gameObject.SetActive(false);

        passwordInput.text = "";
        passwordInput.transform.GetChild(0).gameObject.SetActive(false);

        confirmPasswordInput.text = "";
        confirmPasswordInput.transform.GetChild(0).gameObject.SetActive(false);

        firstNameInput.text = "";
        firstNameInput.transform.GetChild(0).gameObject.SetActive(false);

        lastNameInput.text = "";
        lastNameInput.transform.GetChild(0).gameObject.SetActive(false);

        userNameInput.text = "";
        userNameInput.transform.GetChild(0).gameObject.SetActive(false);

        mobileNumberInput.text = "";
        mobileNumberInput.transform.GetChild(0).gameObject.SetActive(false);

        IDNumberInput.text = "";
        IDNumberInput.transform.GetChild(0).gameObject.SetActive(false);

        OccupationInput.text = "";
        OccupationInput.transform.GetChild(0).gameObject.SetActive(false);

        referenceCodeInput.text = "";
        referenceCodeInput.transform.GetChild(0).gameObject.SetActive(false);

        genderDropDown.ClearOptions();
        countryDropDown.ClearOptions();
        stateDropDown.ClearOptions();
        cityDropDown.ClearOptions();

        adressInput.text = "";
        adressInput.transform.GetChild(0).gameObject.SetActive(false);

        schoolInput.text = "";
        schoolInput.transform.GetChild(0).gameObject.SetActive(false);

        DateOfBirthInput.transform.GetChild(0).gameObject.SetActive(false);
        genderDropDown.transform.GetChild(0).gameObject.SetActive(false);
        countryDropDown.transform.GetChild(0).gameObject.SetActive(false);
        stateDropDown.transform.GetChild(0).gameObject.SetActive(false);
        cityDropDown.transform.GetChild(0).gameObject.SetActive(false);

        LoadCountryDetails();
        LoadGenderDetails();

        messageText_1.text = "";
        messageText_2.text = "";

        registrationProgressManager.ResetRegistrationProgress();
    }
    private bool IsEmailFormatted(string email)
    {
        if (email != null)
        {
            return Regex.IsMatch(email, MatchEmailPattern);
        }
        else return false;
    }
    private bool IsPasswordFormatted(string password)
    {
        if (password != null)
        {
            return PassRegex.IsMatch(password);
        }
        else return false;
    }
    private bool IsPhoneFormatted(string phoneNumber)
    {
        if (phoneNumber != null)
        {
            return PhoneRegex.IsMatch(phoneNumber);
        }
        else return false;
    }
    private bool IsNameFormatted(string name)
    {
        if (name != null)
        {
            return NameRegex.IsMatch(name);
        }
        else return false;
    }
    public void ValidateEmail()
    {
        if (IsEmailFormatted(emailIdInput.text))
        {
            validationChecker[(int)ValidationData.emailId] = true;
            emailIdInput.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            messageText_1.text = "Enter a valid email id.";
            validationChecker[(int)ValidationData.emailId] = false;
            emailIdInput.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    public void ValidatePassword()
    {
        // 
        if (IsPasswordFormatted(passwordInput.text))
        {
            validationChecker[(int)ValidationData.password] = true;
            passwordInput.transform.GetChild(0).gameObject.SetActive(false);

        }
        else
        {
            messageText_1.text = "Password must contain at least eight characters, at least one number " +
                "and both lower and uppercase letters and special characters";
            validationChecker[(int)ValidationData.password] = false;
            passwordInput.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    public void ValidateConfirmPassword()
    {
        if (confirmPasswordInput.text == passwordInput.text && confirmPasswordInput.text.Length > 0)
        {
            validationChecker[(int)ValidationData.confirm_password] = true;
            confirmPasswordInput.transform.GetChild(0).gameObject.SetActive(false);

        }
        else
        {
            messageText_1.text = "Password do not match.";
            validationChecker[(int)ValidationData.confirm_password] = false;
            confirmPasswordInput.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    public void ValidateFirstName()
    {
        if (IsNameFormatted(firstNameInput.text))
        {
            validationChecker[(int)ValidationData.firstName] = true;
            firstNameInput.transform.GetChild(0).gameObject.SetActive(false);


        }
        else
        {
            messageText_1.text = "Enter a valid first name.";
            validationChecker[(int)ValidationData.firstName] = false;
            firstNameInput.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    public void ValidateLastName()
    {
        if (IsNameFormatted(lastNameInput.text))
        {
            validationChecker[(int)ValidationData.lastName] = true;
            lastNameInput.transform.GetChild(0).gameObject.SetActive(false);


        }
        else
        {
            messageText_1.text = "Enter a valid last name.";
            validationChecker[(int)ValidationData.lastName] = false;
            lastNameInput.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    public void ValidateUserName()
    {
        if (userNameInput.text.Length > 3)
        {
            validationChecker[(int)ValidationData.userName] = true;
            userNameInput.transform.GetChild(0).gameObject.SetActive(false);


        }
        else
        {
            messageText_1.text = "Enter a valid username.";
            validationChecker[(int)ValidationData.userName] = false;
            userNameInput.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    public void ValidatePhoneNumber()
    {
        if (IsPhoneFormatted(mobileNumberInput.text))
        {
            validationChecker[(int)ValidationData.mobileNuber] = true;
            mobileNumberInput.transform.GetChild(0).gameObject.SetActive(false);


        }
        else
        {
            messageText_1.text = "Enter a valid mobile number.";
            validationChecker[(int)ValidationData.mobileNuber] = false;
            mobileNumberInput.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    public void ValidateIdNumber()
    {
        if (IDNumberInput.text.Length == 0 || IDNumberInput.text.Length > 3)
        {
            validationChecker[(int)ValidationData.idNumber] = true;
            IDNumberInput.transform.GetChild(0).gameObject.SetActive(false);


        }
        else
        {
            messageText_1.text = "Enter a valid ID number.";
            validationChecker[(int)ValidationData.idNumber] = false;
            IDNumberInput.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    public void ValidateOccupation()
    {
        if (OccupationInput.text.Length == 0 || OccupationInput.text.Length > 2)
        {
            validationChecker[(int)ValidationData.occupation] = true;
            OccupationInput.transform.GetChild(0).gameObject.SetActive(false);



        }
        else
        {
            messageText_1.text = "Enter a valid Occupation detail.";
            validationChecker[(int)ValidationData.occupation] = false;
            OccupationInput.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    public void ValidateReferenceCode()
    {
        if (referenceCodeInput.text.Length == 0 || referenceCodeInput.text.Length > 3)
        {
            validationChecker[(int)ValidationData.referenceCode] = true;
            referenceCodeInput.transform.GetChild(0).gameObject.SetActive(false);


        }
        else
        {
            messageText_1.text = "Enter a valid reference code.";
            validationChecker[(int)ValidationData.referenceCode] = false;
            referenceCodeInput.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    public void NextButtonAction()
    {
        ValidateEmail();
        ValidatePassword();
        ValidateConfirmPassword();
        ValidateFirstName();
        ValidateLastName();
        ValidateUserName();
        ValidatePhoneNumber();
        ValidateIdNumber();
        ValidateOccupation();
        ValidateReferenceCode();

        for (int i = 0; i < 6; i++)
        {
            if (validationChecker[i] == false)
            {
                Debug.Log("Validation false");
                messageText_1.text = "Fill in the credentials.";
                return;
            }
        }

        messageText_1.text = "";

        loginDetailsObj.gameObject.SetActive(false);
        profileDataObj.gameObject.SetActive(true);

        Debug.Log("Validation Correct");
    }

    public void ValidateDob()
    {
        DateTime strng;

        if (DateTime.TryParse(DateOfBirthInput.text, out strng))
        {

            validationChecker[(int)ValidationData.dob] = true;
            DateOfBirthInput.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            messageText_2.text = "Select a valid date of birth.";
            validationChecker[(int)ValidationData.dob] = false;
            DateOfBirthInput.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void ValidateGender()
    {
        if (genderDropDown.value != 0)
        {

            validationChecker[(int)ValidationData.gender] = true;
            genderDropDown.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            messageText_2.text = "Select a valid option";
            validationChecker[(int)ValidationData.gender] = false;
            genderDropDown.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    public void ValidateCountry()
    {
        if (countryDropDown.value != 0)
        {

            validationChecker[(int)ValidationData.country] = true;
            countryDropDown.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            messageText_2.text = "Select a valid option";
            validationChecker[(int)ValidationData.country] = false;
            countryDropDown.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    public void ValidateState()
    {
        if (stateDropDown.value != 0)
        {

            validationChecker[(int)ValidationData.state] = true;
            stateDropDown.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            messageText_2.text = "Select a valid option";
            validationChecker[(int)ValidationData.state] = false;
            stateDropDown.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    public void ValidateCity()
    {
        if (cityDropDown.value != 0)
        {

            validationChecker[(int)ValidationData.city] = true;
            cityDropDown.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            messageText_2.text = "Select a valid option";
            validationChecker[(int)ValidationData.city] = false;
            cityDropDown.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    public void ValidateAddress()
    {
        if (adressInput.text.Length == 0 || adressInput.text.Length > 2)
        {

            validationChecker[(int)ValidationData.adress] = true;
            adressInput.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            messageText_2.text = "Enter a valid address";
            validationChecker[(int)ValidationData.adress] = false;
            adressInput.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    public void ValidateSchool()
    {
        if (schoolInput.text.Length == 0 || schoolInput.text.Length > 2)
        {

            validationChecker[(int)ValidationData.school] = true;
            schoolInput.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            messageText_2.text = "Enter a valid school name";
            validationChecker[(int)ValidationData.school] = false;
            schoolInput.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    public void ValidateAgreeToTerms()
    {
        if (agreeToTermsToogle.isOn)
        {
            validationChecker[(int)ValidationData.termsAndCondition] = true;
        }
        else
        {
            messageText_2.text = "Please accept terms and conditions.";
            validationChecker[(int)ValidationData.termsAndCondition] = false;
        }
    }

    public void PreviousAction()
    {
        loginDetailsObj.gameObject.SetActive(true);
        profileDataObj.gameObject.SetActive(false);
    }

    public void RegsterAction()
    {
        ValidateDob();
        ValidateGender();
        ValidateCountry();
        ValidateState();
        ValidateCity();
        ValidateAddress();
        ValidateSchool();

        foreach (bool item in validationChecker)
        {
            if (!item)
            {
                Debug.Log("Validation false");

                return;
            }
        }

        string date = DateOfBirthInput.text.Replace("-", "/");
        Debug.Log("date  " + date);

        StartCoroutine(WebSericesManager.instance.RegisterNewUser(firstNameInput.text, lastNameInput.text, userNameInput.text,
       emailIdInput.text, passwordInput.text, confirmPasswordInput.text, referenceCodeInput.text, IDNumberInput.text, OccupationInput.text,
       mobileNumberInput.text, cityDropDown.captionText.text, countryDropDown.captionText.text, stateDropDown.captionText.text,
      genderId[genderDropDown.value - 1], adressInput.text, schoolInput.text, date, "timeZone", profileImage.sprite, profileImgConvertion, (RegistrationUserDetails) =>
                {
                    switch (RegistrationUserDetails._apiResponseType)
                    {
                        case apiResponseType.SUCCESS:
                            Debug.Log("Success");
                            //RegistrationUserDetails.userDetails.
                            MainMenuCotroller.instance._screenState = screenState.HomeScreen_Active;
                            MainMenuCotroller.instance._bubbleManager.EnableBubbles();
                            MainMenuCotroller.instance._UserLoginState = UserLoginState.USER_LOGGED_IN;
                            MainMenuCotroller.instance.registrationManager.gameObject.SetActive(false);

                            MainMenuCotroller.instance._slidePanelManager._mainSlidePanelManager.SetSlidePanel(MainMenuCotroller.instance._UserLoginState);
                            MainMenuCotroller.instance.planetsControlManager.SetPlanetState(MainMenuCotroller.instance._UserLoginState);
                            MainMenuCotroller.instance.SetScreenPopulated();

                            MainMenuCotroller.instance._AstronautManager.gameObject.SetActive(true);
                            MainMenuCotroller.instance._AstronautManager.LoadProfilePicFromSprite(MainMenuCotroller.instance._userDetails.profleSprite);
                            
                            break;

                        case apiResponseType.FAIL:
                            Debug.LogWarning(RegistrationUserDetails.responseMessage);

                            break;

                        case apiResponseType.SEVER_ERROR:
                            Debug.LogWarning(RegistrationUserDetails.responseMessage);

                            break;
                    }
                }));
    }
    public void LoadGenderDetails()
    {
        StartCoroutine(WebSericesManager.instance.GetGenderDropDownData((GenderDetailsData) =>
        {
            switch (GenderDetailsData._apiResponseType)
            {
                case apiResponseType.SUCCESS:
                    Debug.Log("Success");

                    genderId = new List<string>();


                    foreach (string item in GenderDetailsData.genderId)
                    {
                        genderId.Add(item);
                    }

                    genderDropDown.AddOptions(GenderDetailsData.genderList);

                    genderDropDown.options.Insert(0, new TMP_Dropdown.OptionData("Select Gender"));

                    //foreach (string item in GenderDetailsData.genderList)
                    //{
                    //    Debug.Log("Call back success item  " + item);
                    //}
                    break;

                case apiResponseType.FAIL:
                    Debug.LogWarning(GenderDetailsData.responseMessage);

                    break;

                case apiResponseType.SEVER_ERROR:
                    Debug.LogWarning(GenderDetailsData.responseMessage);

                    break;
            }
        }));
    }
    public void LoadCountryDetails()
    {
        StartCoroutine(WebSericesManager.instance.GetCountryDropDownData((CountryDeatails) =>
        {
            switch (CountryDeatails._apiResponseType)
            {
                case apiResponseType.SUCCESS:
                    Debug.Log("Success");

                    countryDropDown.AddOptions(CountryDeatails.countryList);

                    countryDropDown.options.Insert(0, new TMP_Dropdown.OptionData("Select Country"));
                    //foreach (string item in CountryDeatails.countryList)
                    //{                       
                    //    Debug.Log("Call back success item  " + item);
                    //}
                    break;

                case apiResponseType.FAIL:
                    Debug.LogWarning(CountryDeatails.responseMessage);

                    break;

                case apiResponseType.SEVER_ERROR:
                    Debug.LogWarning(CountryDeatails.responseMessage);

                    break;
            }
        }));
    }
    public void LoadStateList()
    {
        StartCoroutine(WebSericesManager.instance.GetStateDropDownData(countryDropDown.captionText.text, (LeaderBoardAPIResponse) =>
        {
            switch (LeaderBoardAPIResponse._apiResponseType)
            {
                case apiResponseType.SUCCESS:
                    Debug.Log("Success");
                    stateDropDown.options.Insert(0, new TMP_Dropdown.OptionData("Select State/Country/Region"));
                    stateDropDown.AddOptions(LeaderBoardAPIResponse.stateList);

                   
                    //foreach (string item in LeaderBoardAPIResponse.stateList)
                    //{
                    //    Debug.Log("Call back success item  " + item);
                    //}
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
    public void LoadCityList()
    {
        StartCoroutine(WebSericesManager.instance.GetCityDropDownData(stateDropDown.captionText.text, (CityListAPIResponse) =>
        {
            switch (CityListAPIResponse._apiResponseType)
            {
                case apiResponseType.SUCCESS:
                    Debug.Log("Success");
                    cityDropDown.options.Insert(0, new TMP_Dropdown.OptionData("Select City/Town"));
                    cityDropDown.AddOptions(CityListAPIResponse.cityList);
                    
                    //foreach (string item in CityListAPIResponse.cityList)
                    //{
                    //    Debug.Log("Call back success item  " + item);
                    //}
                    break;

                case apiResponseType.FAIL:
                    Debug.LogWarning(CityListAPIResponse.responseMessage);

                    break;

                case apiResponseType.SEVER_ERROR:
                    Debug.LogWarning(CityListAPIResponse.responseMessage);

                    break;
            }
        }));
    }
    public void CheckUiqueField(string fieldName, string fieldValue)
    {
        StartCoroutine(WebSericesManager.instance.CheckUniqueFields(fieldName, fieldValue, (uniqueFieldResponse) =>
        {
            switch (uniqueFieldResponse._apiResponseType)
            {
                case apiResponseType.SUCCESS:
                    Debug.Log("Success");

                    Debug.Log("Call back success item  " + uniqueFieldResponse.uniqueStatus);

                    break;

                case apiResponseType.FAIL:
                    Debug.LogWarning(uniqueFieldResponse.responseMessage);

                    break;

                case apiResponseType.SEVER_ERROR:
                    Debug.LogWarning(uniqueFieldResponse.responseMessage);

                    break;
            }
        }));
    }

    public void BackButtonAction()
    {
        MainMenuCotroller.instance._UserLoginState = UserLoginState.USER_LOGGED_OUT;
        MainMenuCotroller.instance._screenState = screenState.HomeScreen_Active;
        MainMenuCotroller.instance._bubbleManager.EnableBubbles();

        MainMenuCotroller.instance._slidePanelManager._mainSlidePanelManager.SetSlidePanel(MainMenuCotroller.instance._UserLoginState);
        MainMenuCotroller.instance.planetsControlManager.SetPlanetState(MainMenuCotroller.instance._UserLoginState);
        MainMenuCotroller.instance.SetScreenPopulated();

        MainMenuCotroller.instance.registrationManager.gameObject.SetActive(false);
    }
}

public enum ValidationData
{
    emailId,
    password,
    confirm_password,
    firstName,
    lastName,
    userName,
    mobileNuber,
    idNumber,
    occupation,
    referenceCode,

    dob,
    gender,
    country,
    state,
    city,
    adress,
    school,
    profilePic,
    termsAndCondition
}

public enum UniqueFields
{
    userName,
    email,
    mobileNumber
}