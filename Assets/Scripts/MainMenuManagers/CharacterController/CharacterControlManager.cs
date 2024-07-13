using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControlManager : MonoBehaviour
{
    [SerializeField]
    private List<CharacterDataManager> _DataManager;

    public void ResetCharacters()
    {

        foreach (CharacterDataManager controller in _DataManager)
        {
            if (controller.characterMeshObj != null)
            {
                controller.characterMeshObj.HighLightOff();
            }
        }
    }

}

public enum characterName
{
    MannyTheOctoupus,
    TrevorThePenguin,
    WilliamTheSwordFish,
    RuruTheTurtule,
    AllyTheDolphin,
    VillaTheCrab,
    BruceTheShark,
    SolarTheSurfer,
    NuvolaTheMermaid
}