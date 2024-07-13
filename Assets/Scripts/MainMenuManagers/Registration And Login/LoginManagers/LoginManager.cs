using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField emailIdInput;

    [SerializeField]
    private TMP_InputField passwordInput;

    [SerializeField]
    private Regex PassRegex = new Regex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$");

    [SerializeField]
    private const string MatchEmailPattern =
        @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
 + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
 + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
 + @"([a-zA-Z0-9]+[\w-]+\.)+[a-zA-Z]{1}[a-zA-Z0-9-]{1,23})$";

    [SerializeField]
    private TMP_Text messageText;

    [SerializeField]
    private List<bool> validationChecker;

    private Toggle rememberToggle;

    private void OnEnable()
    {
        ResetInputData();
    }
    void ResetInputData()
    {

        emailIdInput.text = "";
        emailIdInput.transform.GetChild(0).gameObject.SetActive(false);

        passwordInput.text = "";
        passwordInput.transform.GetChild(0).gameObject.SetActive(false);

        messageText.text = "";


        validationChecker = new List<bool>();

        for (int i = 0; i < Enum.GetValues(typeof(LoginValidationData)).Length; i++)
        {
            validationChecker.Add(false);
        }

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
    public void ValidateEmail()
    {
        if (IsEmailFormatted(emailIdInput.text))
        {
            validationChecker[(int)ValidationData.emailId] = true;
            emailIdInput.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            validationChecker[(int)ValidationData.emailId] = false;
            messageText.text = "Enter a valid email id.";
            emailIdInput.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    public void ValidatePassword()
    {
        if (IsPasswordFormatted(passwordInput.text))
        {
            validationChecker[(int)ValidationData.password] = true;
            passwordInput.transform.GetChild(0).gameObject.SetActive(false);

        }
        else
        {
            validationChecker[(int)ValidationData.password] = false;
            messageText.text = "Password must contain at least eight characters, at least one number " +
                "and both lower and uppercase letters and special characters";

            passwordInput.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void LoginAction()
    {
        ValidateEmail();
        ValidatePassword();

        for (int i = 0; i < Enum.GetValues(typeof(LoginValidationData)).Length; i++)
        {
            if (validationChecker[i] == false)
            {
                Debug.Log("Validation false");
                messageText.text = "Fill in the credentials.";
                return;
            }
        }


        StartCoroutine(WebSericesManager.instance.LoginData(emailIdInput.text, passwordInput.text, (LoginDetails) =>
        {
            switch (LoginDetails._apiResponseType)
            {
                case apiResponseType.SUCCESS:
                    Debug.Log("Success");
                    WebSericesManager.instance.authToken = LoginDetails.userToken;
                    LoadUserDetails(emailIdInput.text, WebSericesManager.instance.authToken);

                    break;

                case apiResponseType.FAIL:
                    messageText.text = "Invalid Credentials.";
                    Debug.LogWarning(LoginDetails.responseMessage);

                    break;

                case apiResponseType.SEVER_ERROR:
                    messageText.text = "Invalid Credentials.";
                    Debug.LogWarning(LoginDetails.responseMessage);

                    break;
            }
        }));
    }

    public void LoadUserDetails(string emailId, string authToken)
    {
        StartCoroutine(WebSericesManager.instance.GetUserDetailsByMail(emailId, authToken, (LoginDetails) =>
        {
            switch (LoginDetails._apiResponseType)
            {
                case apiResponseType.SUCCESS:
                    Debug.Log("Success");
                    MainMenuCotroller.instance._screenState = screenState.HomeScreen_Inactive;
                    MainMenuCotroller.instance._bubbleManager.EnableBubbles();
                    MainMenuCotroller.instance._UserLoginState = UserLoginState.USER_LOGGED_IN;
                    MainMenuCotroller.instance.LoginManager.gameObject.SetActive(false);
                    MainMenuCotroller.instance._slidePanelManager._mainSlidePanelManager.SetSlidePanel(MainMenuCotroller.instance._UserLoginState);
                    MainMenuCotroller.instance.planetsControlManager.SetPlanetState(MainMenuCotroller.instance._UserLoginState);
                    MainMenuCotroller.instance.SetScreenPopulated();

                    MainMenuCotroller.instance._AstronautManager.gameObject.SetActive(true);
                    MainMenuCotroller.instance._AstronautManager.LoadProfilePic(MainMenuCotroller.instance._userDetails.profilePicUrl); ;


                    break;

                case apiResponseType.FAIL:
                    Debug.LogWarning(LoginDetails.responseMessage);

                    break;

                case apiResponseType.SEVER_ERROR:
                    Debug.LogWarning(LoginDetails.responseMessage);

                    break;
            }
        }));
    }

    public void RememberMeAction()
    {
        if (rememberToggle.isOn)
        {
            PlayerPrefs.SetString("login_email", emailIdInput.text);
            PlayerPrefs.SetString("login_password", passwordInput.text);
        }
    }
    public void SignUpAction()
    {
        MainMenuCotroller.instance.LoginManager.gameObject.SetActive(false);
        MainMenuCotroller.instance.registrationManager.gameObject.SetActive(true);
    }

    public void ForgotPasswordAction()
    {

    }
    public void BackButtonAction()
    {
        MainMenuCotroller.instance._UserLoginState = UserLoginState.USER_LOGGED_OUT;
        MainMenuCotroller.instance._screenState = screenState.HomeScreen_Active;
        MainMenuCotroller.instance._bubbleManager.EnableBubbles();

        MainMenuCotroller.instance._slidePanelManager._mainSlidePanelManager.SetSlidePanel(MainMenuCotroller.instance._UserLoginState);
        MainMenuCotroller.instance.planetsControlManager.SetPlanetState(MainMenuCotroller.instance._UserLoginState);
        MainMenuCotroller.instance.SetScreenPopulated();

        MainMenuCotroller.instance.LoginManager.gameObject.SetActive(false);
    }
}

public enum LoginValidationData
{
    emailId,
    passwprd
}