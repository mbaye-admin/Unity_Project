using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

public class BubbleElements : MonoBehaviour
{
    public BubbleManager _bubbleManager;
    public HomePageURLContent homePageURLContent;
    public MeshRenderer visualPlaneRenderer;

    [SerializeField]
    private Vector3 initialScale;

    [SerializeField]
    private Vector3 scaleSize;

    public float frequency = 1f; // Speed of sine movement
    public float magnitude = 0.2f; // Size of sine movement
    public float verticalSpeed = 1f; // Speed moving up
    public float horizontalSpeed = 0.2f; // Speed moving left and right

    public float baseScale = 1f; // The base scale at y = 0
    public float scaleRate = 0.2f; // How much the scale changes with height

    private Vector3 originalScale;
    [SerializeField]
    private MoveState moveState;

    private Vector3 pos;

    [SerializeField]
    private float lifeTime;

    [SerializeField]
    private SphereCollider thisCollider;

    [SerializeField]
    private float scaleMultiplier;


    private void OnEnable()
    {

    }

    private void Start()
    {
        verticalSpeed = UnityEngine.Random.Range(8f, 11f);
        lifeTime = UnityEngine.Random.Range(75f, 110f);

        moveState = MoveState.Bubble_Move_False;
        StartCoroutine(GetTexture(homePageURLContent.thubnailImgUrl));

        pos = transform.position;
        originalScale = transform.localScale;
        initialScale = transform.localScale;

        // Invoke(nameof(BeforeDestroy), lifeTime);
        Invoke("BeforeDestroy", lifeTime);
    }

    void BeforeDestroy()
    {
        _bubbleManager.LoadNextBubble(homePageURLContent);

        Destroy(gameObject); // Destroy the game object
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MainCamera")
        {
            CancelInvoke("BeforeDestroy");
            BeforeDestroy();
        }
    }

    public void OnHighLightTrigger()
    {
        //moveState = MoveState.Bubble_Move_False;
    }
    public void HighLightOff()
    {
        //moveState = MoveState.Bubble_Move_True;
    }
    public void OnBubbleClickAction()
    {
        Debug.Log("Bubble Click");
        MainMenuCotroller.instance._cameraController.gameObject.SetActive(false);
        MainMenuCotroller.instance._cameraController.ResetRotation();
        MainMenuCotroller.instance._cameraController.gameObject.SetActive(true);
        MainMenuCotroller.instance._videoPlayerManager.LoadVideoAndPlay(homePageURLContent.videoUrl);
    }
    public void Update()
    {
        if (moveState == MoveState.Bubble_Move_True)
        {
            pos += Vector3.up * verticalSpeed * Time.deltaTime;
            transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency / 2) * magnitude;

            scaleMultiplier = MathF.Abs(baseScale + (transform.localPosition.y * scaleRate));

            // Debug.Log("scaleMultiplier  : " + scaleMultiplier);

            transform.localScale = originalScale * Mathf.Clamp(scaleMultiplier, .3f, .4f);
        }
        transform.LookAt(Camera.main.transform);
    }
    IEnumerator GetTexture(string url)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(homePageURLContent);
            Debug.Log("Thumbnail : " + url);
            Debug.LogError("Thumbnail " + www.error);

            Destroy(gameObject);
        }
        else
        {

            Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            visualPlaneRenderer.material.mainTexture = myTexture;

            moveState = MoveState.Bubble_Move_True;

            if (MainMenuCotroller.instance._screenState == screenState.HomeScreen_Active)
            {
                transform.GetComponent<MeshRenderer>().enabled = true;
                visualPlaneRenderer.GetComponent<MeshRenderer>().enabled = true;
            }

            _bubbleManager.CheckIfAddIsReady();
        }
    }
}

public enum MoveState
{
    Bubble_Move_True,
    Bubble_Move_False
}