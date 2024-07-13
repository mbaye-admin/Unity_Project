using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashPanelManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text loadingText;

    [SerializeField]
    private TMP_Text loadingDescriptionText;

    [SerializeField]
    private float speed;

    //private void OnEnable()
    //{
    //    iTween.ValueTo(gameObject, iTween.Hash("from", 0, "to", 100, "onupdatetarget", gameObject, "onupdate", "updateFromValue", "time", speed, "easetype", iTween.EaseType.easeOutExpo));
    //}
    //private void updateFromValue(int newValue)
    //{
    //    loadingText.text = "Loading : " + newValue.ToString() + " %";
    //    Debug.Log("My Value that is tweening: " + newValue);

    //    if (newValue >= 100)
    //    {
    //        LoadMainScene();
    //    }
    //}

    private void LoadMainScene()
    {
        SceneManager.LoadScene("BasicSampleScene"); 
        //SceneManager.LoadScene("MainApplication");
    }

    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return null;

        //Begin to load the Scene you specify
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("MainApplication");

       // AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("BasicSampleScene");

        //Don't let the Scene activate until you allow it to
        asyncOperation.allowSceneActivation = false;
        Debug.Log("Pro :" + asyncOperation.progress);
        //When the load is still in progress, output the Text and progress bar
        while (!asyncOperation.isDone)
        {
            //Output the current progress
            loadingText.text = "Loading progress: " + (asyncOperation.progress * 100) + "%";

            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f)
            {
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }

    }
}
