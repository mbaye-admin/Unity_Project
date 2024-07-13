using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetsControlManager : MonoBehaviour
{
    [SerializeField]
    private List<PlanetsManager> planetsCollection;

    public void SetPlanetState(UserLoginState _UserLoginState)
    {
        foreach (PlanetsManager item in planetsCollection)
        {
            item.SetPlanetState(_UserLoginState);
        }
    }


    public void ResetPlanet()
    {
        foreach (PlanetsManager item in planetsCollection)
        {
            item.HighLightOff();
        }
    }
}
