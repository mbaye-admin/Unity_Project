using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Vector3 initialParentScale;

    public Vector3 maximumScaleSize;

    public Vector3 initialMeshSize;

    [SerializeField]
    private TMP_Text characterNameTxt;

    [SerializeField]
    private characterName _characterName;

    public GameObject parentToScale;

    private void OnEnable()
    {
        // parentToScale.transform.localScale = transform.localScale;       
    }

    private void Start()
    {
        initialMeshSize = transform.localScale;
        characterNameTxt.gameObject.SetActive(false);
        //initialScale = parentToScale.transform.localScale;
        //scaleSize = initialScale * 1.5f;
    }

    public void OnHighLightTrigger()
    {
        if (parentToScale.transform.localScale != maximumScaleSize)
        {
            //characterNameTxt.gameObject.SetActive(true);
            Debug.Log("Character Highlight");
            parentToScale.transform.localScale = maximumScaleSize;
        }
    }
    public void HighLightOff()
    {
        if (parentToScale.transform.localScale != initialParentScale)
        {
            Debug.Log("Missing " + gameObject.name);
            //characterNameTxt.gameObject.SetActive(false);
            Debug.Log("Character Highlight Off");
            parentToScale.transform.localScale = initialParentScale;


        }
        if (transform.localScale != initialMeshSize)
        {
            transform.localScale = initialMeshSize;
        }
    }

    public void OnCharacterClickAction()
    {
        Debug.Log("Character Click");

        MainMenuCotroller.instance.mainCanvasObj.SetActive(false);
        MainMenuCotroller.instance._cameraController.transform.rotation = Quaternion.identity;
        MainMenuCotroller.instance._cameraController.transform.position = new Vector3(0, 1f, -14f);
        MainMenuCotroller.instance._cameraController.enabled = false;
        MainMenuCotroller.instance._playerInputController.gameObject.SetActive(false);
        MainMenuCotroller.instance._bubbleManager.gameObject.SetActive(false);
        MainMenuCotroller.instance.planetsControlManager.gameObject.SetActive(false);
        MainMenuCotroller.instance.haloManager.gameObject.SetActive(false);

        MainMenuCotroller.instance._CharacterDescriptionManager.gameObject.SetActive(true);
        MainMenuCotroller.instance._CharacterDescriptionManager.LoadDescriptionData(_characterName);

        transform.localScale = initialParentScale;

        MainMenuCotroller.instance._AstronautManager.gameObject.SetActive(false);

        MainMenuCotroller.instance.charaterControlManager.gameObject.SetActive(false);
    }

    private void Update()
    {
        //characterNameTxt.transform.LookAt(new Vector3(Camera.main.transform.rotation.x, Camera.main.transform.rotation.y, Camera.main.transform.rotation.z));
    }
}
