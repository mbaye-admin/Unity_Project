using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterDataManager : MonoBehaviour
{

    public CharacterController characterMeshObj;

    public string description;

    [SerializeField]
    private Transform[] pathTransforms;

    [SerializeField]
    private float pathValue;

    [SerializeField]
    private float speed;

    [SerializeField]
    private iTween.EaseType easeType;

    private void Start()
    {
        CharacterInitialisation();
        SetPathForTrevor();
    }

    public void CharacterInitialisation()
    {
        characterMeshObj.parentToScale = this.gameObject;

        characterMeshObj.initialParentScale = transform.localScale;
        characterMeshObj.maximumScaleSize = transform.localScale * 1.8f;
    }

    void SetPathForTrevor()
    {
        if (pathTransforms.Length > 0)
        {
            iTween.ValueTo(gameObject, iTween.Hash("from", 0f, "to", 1f, "onupdatetarget", gameObject, "onupdate", "updateFromValue", "time", speed, "oncomplete", "SetPathForTrevor", "easetype", easeType));
        }
    }

    private void updateFromValue(float newValue)
    {
        if (pathTransforms.Length > 0)
        {
            //Debug.Log("My Value that is tweening: " + newValue);
            //Debug.Log("iTween.PathLength(pathTransforms) : " + iTween.PathLength(pathTransforms));

            pathValue = newValue;
            Vector3 currentPosition = iTween.PointOnPath(pathTransforms, pathValue);
            // Debug.Log("currentPosition  " + currentPosition);
            characterMeshObj.transform.LookAt(new Vector3(currentPosition.x, currentPosition.y, currentPosition.z + 180));
            iTween.PutOnPath(characterMeshObj.gameObject, pathTransforms, pathValue);
        }
    }

    private void OnDrawGizmos()
    {
        iTween.DrawPath(pathTransforms, Color.red);
    }
}
