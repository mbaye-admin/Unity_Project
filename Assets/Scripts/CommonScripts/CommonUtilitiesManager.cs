using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonUtilitiesManager : MonoBehaviour
{
    public static CommonUtilitiesManager instance = null;
    public static CommonUtilitiesManager Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
