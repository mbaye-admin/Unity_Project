using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Vector2 roationAngle;

    [SerializeField]
    private Vector2 videoCameraPostion;

    [SerializeField]
    private Vector2 videoCameraRotation;

    public float scrollSpeed = 15f;
    public float distance = 40f;
    public float scroll;

    private void Start()
    {
        distance = 100f;
        // Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (MainMenuCotroller.instance._screenState == screenState.HomeScreen_Active)
        {
            if (MainMenuCotroller.instance._playerInputController._objSelectType == ObjSelectType.SELECTION_FALSE)
            {
                if (Input.GetMouseButton(0))
                {
                    roationAngle.x += Input.GetAxis("Mouse X");
                    roationAngle.y += Input.GetAxis("Mouse Y");
                }

                transform.localRotation = Quaternion.Euler(-roationAngle.y, roationAngle.x, 0);

                scroll = Input.GetAxis("Mouse ScrollWheel");
                transform.position += transform.forward * scroll * (scrollSpeed * 75f) * Time.deltaTime;

                // Clamp the camera movement to a certain distance if needed
                transform.position = Vector3.ClampMagnitude(transform.position, distance);
            }
        }
    }
    public void ResetRotation()
    {
        roationAngle = Vector2.zero;
        transform.localRotation = Quaternion.Euler(roationAngle);
        transform.position = new Vector3(0, 1f, -10f);
    }
}
