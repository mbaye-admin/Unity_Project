using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlanetsManager : MonoBehaviour
{
    [SerializeField]
    private Vector3 initialScale;

    [SerializeField]
    private Vector3 scaleSize;

    [SerializeField]
    private GameObject planetNavigateImg;

    [SerializeField]
    private GameObject planetNavigateText;

    [SerializeField]
    private GameObject planetLoggedInNavigateImg;

    [SerializeField]
    private GameObject planetNameImg;

    [SerializeField]
    private GameObject planetMeshObj;

    [SerializeField]
    private float planetRotationDirection;

    [SerializeField]
    private float planetRotationSpeed;


    private void OnEnable()
    {
        initialScale = transform.localScale;

        iTween.RotateBy(planetMeshObj, iTween.Hash("z", planetRotationDirection, "speed", planetRotationSpeed, "looptype", iTween.LoopType.loop));
    }

   public void SetPlanetState(UserLoginState _UserLoginState)
    {
        planetNavigateImg.gameObject.SetActive(false);
        planetNavigateText.gameObject.SetActive(false);
        planetNameImg.gameObject.SetActive(false);
        planetLoggedInNavigateImg.SetActive(false);

        switch (_UserLoginState)
        {
            case UserLoginState.USER_LOGGED_IN:
                planetNavigateImg.SetActive(false);
                planetLoggedInNavigateImg.SetActive(true);
                break;

            case UserLoginState.USER_LOGGED_OUT:
                planetNavigateImg.SetActive(true);
                planetLoggedInNavigateImg.SetActive(false);
                break;
        }      

    }


    public void OnHighLightTrigger()
    {
        if (transform.localScale != scaleSize)
        {
            Debug.Log("Planet Highlight");
            transform.localScale = scaleSize;
            planetNameImg.gameObject.SetActive(true);
        }
    }
    public void HighLightOff()
    {
        if (transform.localScale != initialScale)
        {
            Debug.Log("Planet Highlight Off");
            transform.localScale = initialScale;
            planetNameImg.gameObject.SetActive(false);
        }
    }

    public void OnPlanetClickAction()
    {
        Debug.Log("Planet Click");

    }
}
