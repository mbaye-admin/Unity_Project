using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class UIButtonsManager : MonoBehaviour
{
    [SerializeField]
    private Volume globalVolume;

    [SerializeField]
    private VolumeProfile volumeProfileDark, volumeProfileLight;

    bool appLightMode;
    public void ChangeThemeAction()
    {
        Debug.Log("appLightMode  " + appLightMode);

        switch (appLightMode)
        {
            case false:
                globalVolume.profile = volumeProfileLight;
                appLightMode = true;
                break;
            case true:
                globalVolume.profile = volumeProfileDark;
                appLightMode = false;
                break;
        }
    }

    public void ResetCameraAction()
    {
        MainMenuCotroller.instance._cameraController.gameObject.SetActive(false);
        MainMenuCotroller.instance._cameraController.ResetRotation();
        MainMenuCotroller.instance._cameraController.gameObject.SetActive(true);

        MainMenuCotroller.instance.planetsControlManager.ResetPlanet();
        MainMenuCotroller.instance.charaterControlManager.ResetCharacters();
    }
}
