using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterDescriptionManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text headingTxt;
    [SerializeField]
    private TMP_Text characterNameTxt;
    [SerializeField]
    private TMP_Text DescriptionTxt;
    [SerializeField]
    private List<characterSet> character;


    private void OnEnable()
    {
        foreach (characterSet item in character)
        {
            item.character.SetActive(false);
        }
    }

    public void OnCharacterDescriptionClose()
    {
        MainMenuCotroller.instance.mainCanvasObj.SetActive(true);
        MainMenuCotroller.instance._cameraController.transform.rotation = Quaternion.identity;
        MainMenuCotroller.instance._cameraController.transform.position = new Vector3(0, 1f, -10f);
        MainMenuCotroller.instance._cameraController.enabled = true;
        MainMenuCotroller.instance._playerInputController.gameObject.SetActive(true);
        MainMenuCotroller.instance._bubbleManager.gameObject.SetActive(true);
        MainMenuCotroller.instance.planetsControlManager.gameObject.SetActive(true);
        MainMenuCotroller.instance.haloManager.gameObject.SetActive(true);
        MainMenuCotroller.instance.charaterControlManager.gameObject.SetActive(true);

        MainMenuCotroller.instance._videoPlayerManager.videoPlayer.Play();

        
        switch (MainMenuCotroller.instance._UserLoginState)
        {
            case UserLoginState.USER_LOGGED_IN:
                MainMenuCotroller.instance._AstronautManager.gameObject.SetActive(true);
                break;

            case UserLoginState.USER_LOGGED_OUT:
                MainMenuCotroller.instance._AstronautManager.gameObject.SetActive(false);
                break;
        }


        MainMenuCotroller.instance._CharacterDescriptionManager.gameObject.SetActive(false);
        MainMenuCotroller.instance.charaterControlManager.ResetCharacters();
    }

    public void LoadDescriptionData(characterName _characterName)
    {
        foreach (characterSet characterSet in character)
        {
            if (characterSet._characterName == _characterName)
            {
                characterNameTxt.text = characterSet.characterName;
                DescriptionTxt.text = characterSet.description;
                characterSet.character.SetActive(true);
            }
        }
    }

}
[System.Serializable]
public class characterSet
{
    public string characterName;
    [TextArea(minLines: 1, maxLines: 100000)]
    public string description;
    public characterName _characterName;
    public GameObject character;
}