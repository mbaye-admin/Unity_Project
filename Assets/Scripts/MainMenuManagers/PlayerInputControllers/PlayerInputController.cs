using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField]
    private LayerMask _layerToHit;

    [SerializeField]
    private float maxDistanceVal = 100f;

    [SerializeField]
    private float scrollValue;

    [SerializeField]
    private float characterSize;

    public ObjSelectType _objSelectType;

    [SerializeField]
    Vector3 targetScale;

    private void OnEnable()
    {
        _objSelectType = ObjSelectType.SELECTION_FALSE;
    }

    private void Update()
    {
        DetectRayFromMouse();
    }

    void DetectRayFromMouse()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (/*!EventSystem.current.IsPointerOverGameObject() && */Physics.Raycast(cameraRay, out RaycastHit hitObj, Mathf.Infinity, _layerToHit))
        {
            if (hitObj.transform.GetComponent<PlanetsManager>())
            {
                _objSelectType = ObjSelectType.SELECTION_TRUE;

                hitObj.transform.GetComponent<PlanetsManager>().OnHighLightTrigger();

                if (Input.GetMouseButtonDown(0))
                {
                    // Debug.Log("Hit Obj Name : " + hitObj.collider.gameObject.name);
                    hitObj.transform.GetComponent<PlanetsManager>().OnPlanetClickAction();
                }
            }

            if (hitObj.transform.GetComponent<CharacterController>())
            {
                _objSelectType = ObjSelectType.SELECTION_TRUE;

                hitObj.transform.GetComponent<CharacterController>().OnHighLightTrigger();



                scrollValue = Input.GetAxis("Mouse ScrollWheel");

                if (scrollValue > 0)
                {
                    Debug.Log("scrollValue  " + scrollValue);

                    targetScale = hitObj.transform.GetComponent<CharacterController>().transform.localScale;
                    targetScale += scrollValue * 10 * targetScale * Time.deltaTime;

                    hitObj.transform.GetComponent<CharacterController>().transform.localScale = targetScale;

                }


                if (Input.GetMouseButtonDown(0))
                {
                    // Debug.Log("Hit Obj Name : " + hitObj.collider.gameObject.name);
                    hitObj.transform.GetComponent<CharacterController>().OnCharacterClickAction();
                }
            }

            if (hitObj.transform.GetComponent<BubbleElements>())
            {
                _objSelectType = ObjSelectType.SELECTION_TRUE;

                hitObj.transform.GetComponent<BubbleElements>().OnHighLightTrigger();
                if (Input.GetMouseButtonDown(0))
                {
                    // Debug.Log("Hit Obj Name : " + hitObj.collider.gameObject.name);
                    hitObj.transform.GetComponent<BubbleElements>().OnBubbleClickAction();
                }
            }
        }
        else
        {
            if (MainMenuCotroller.instance != null)
            {
                _objSelectType = ObjSelectType.SELECTION_FALSE;

                MainMenuCotroller.instance.planetsControlManager.ResetPlanet();
                MainMenuCotroller.instance.charaterControlManager.ResetCharacters();
            }
        }
    }
}

public enum ObjSelectType
{
    SELECTION_TRUE,
    SELECTION_FALSE,

}