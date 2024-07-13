using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonHooverManager : MonoBehaviour
{
    public TMP_Text hooverText;
    public string hooverNameStr;

    private void OnEnable()
    {
        hooverText.text = hooverNameStr;
        hooverText.gameObject.SetActive(false);
    }

    public void OnMouseOverAction()
    {
        hooverText.gameObject.SetActive(true);
    }

    public void OnMouseExitAction()
    {
        hooverText.gameObject.SetActive(false);
    }
}
