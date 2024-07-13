using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Rendering.PostProcessing;

public class AstronautManager : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer profileFaceRenderer;

    public void LoadProfilePic(string profilePicUrl)
    {
        StartCoroutine(GetTexture(profilePicUrl));
    }

    public void LoadProfilePicFromSprite(Sprite sprite)
    {
        var croppedTexture = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
        var pixels = sprite.texture.GetPixels((int)sprite.textureRect.x,
        (int)sprite.textureRect.y,
                                                (int)sprite.textureRect.width,
                                                (int)sprite.textureRect.height);
        croppedTexture.SetPixels(pixels);
        croppedTexture.Apply();

        profileFaceRenderer.material.mainTexture = croppedTexture;
    }

    IEnumerator GetTexture(string url)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Thumbnail : " + url);
            Debug.LogError("Thumbnail " + www.error);

            Destroy(gameObject);
        }
        else
        {

            Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            profileFaceRenderer.material.mainTexture = myTexture;

            //transform.GetComponent<MeshRenderer>().enabled = true;
            //profileFaceRenderer.GetComponent<MeshRenderer>().enabled = true;

        }
    }
}
