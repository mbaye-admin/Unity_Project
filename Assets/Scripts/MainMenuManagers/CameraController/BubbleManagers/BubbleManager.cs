using System;
using System.Collections.Generic;
using UnityEngine;
public class BubbleManager : MonoBehaviour
{
    public List<HomePageURLContent> bubbleData;
    public GameObject bubblePrefabObj;

    [SerializeField]
    private SphereCollider thisCollider;

    [SerializeField]
    private Bounds bounds;

    private void Start()
    {
        thisCollider = GetComponent<SphereCollider>();

        bounds = thisCollider.bounds;
    }
    //public VideoPlayerManager _videoPlayManger;
    public void LoadBubblesData()
    {
        LoadBubblesWithInterval();

        //MainMenuCotroller.instance.CheckAppState(AppReadyState.IsBubbleReady);
    }

    void LoadBubblesWithInterval()
    {
        for (int i = 0; i < bubbleData.Count; i++)
        {
            LoadNextBubble(bubbleData[i]);
        }
    }

    public void LoadNextBubble(HomePageURLContent _HomePageURLContent)
    {
        GameObject BubbleObj = Instantiate(bubblePrefabObj);
        BubbleObj.transform.parent = transform;
        BubbleObj.transform.GetComponent<BubbleElements>().homePageURLContent = _HomePageURLContent;
        BubbleObj.transform.name = _HomePageURLContent.Name; /* + "_" + index.ToString();*/
        BubbleObj.transform.GetComponent<BubbleElements>()._bubbleManager = this;


        //float rangeOfX = (bounds.size.x / 3f);
        //float rangeOfY = (bounds.size.y / 3f);
        //float rangeOfZ = (bounds.size.z / 3f);

        //Vector3 newVec = transform.TransformPoint(new Vector3(UnityEngine.Random.Range(-rangeOfX, rangeOfX), UnityEngine.Random.Range(-rangeOfY, rangeOfY), UnityEngine.Random.Range(-rangeOfZ, rangeOfZ)));

        float rangeOfX = (bounds.size.x / 1500f);
        float rangeOfY = (bounds.size.y / 1750f);
        float rangeOfZ = (bounds.size.z / 1500f);

        //Debug.Log("rangeOfY : " + rangeOfY);

        Vector3 newVec = transform.TransformPoint(new Vector3(UnityEngine.Random.Range(-rangeOfX, rangeOfX), UnityEngine.Random.Range(-0.6f, -0.3f), UnityEngine.Random.Range(-rangeOfZ, rangeOfZ)));

        BubbleObj.transform.rotation = bubblePrefabObj.transform.rotation;
        BubbleObj.transform.position = newVec;
        BubbleObj.transform.localScale = new Vector3(.1f, .1f, .1f);

        BubbleObj.transform.GetComponent<MeshRenderer>().enabled = false;
        BubbleObj.transform.GetComponent<BubbleElements>().visualPlaneRenderer.GetComponent<MeshRenderer>().enabled = false;

        BubbleObj.gameObject.SetActive(true);
    }

    int addReadyCount = 0;
    public void CheckIfAddIsReady()
    {
        addReadyCount += 1;

        if (addReadyCount == transform.childCount)
        {
            Debug.Log("Adds Ready");
            MainMenuCotroller.instance.CheckAppState(AppReadyState.IsBubbleReady);
        }
    }

    public void DissableBubble()
    {
        foreach (Transform item in transform)
        {
            // item.gameObject.SetActive(false);
            item.GetComponent<SphereCollider>().enabled = false;

            item.transform.GetComponent<MeshRenderer>().enabled = false;
            item.transform.GetComponent<BubbleElements>().visualPlaneRenderer.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    public void EnableBubbles()
    {
        foreach (Transform item in transform)
        {
            //  item.gameObject.SetActive(true);
            item.GetComponent<SphereCollider>().enabled = true;

            item.transform.GetComponent<MeshRenderer>().enabled = true;
            item.transform.GetComponent<BubbleElements>().visualPlaneRenderer.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}

[System.Serializable]
public class BubbleDataContainer
{

}
