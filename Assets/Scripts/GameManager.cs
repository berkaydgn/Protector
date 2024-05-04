using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    void Start()
    {
        if (!PlayerPrefs.HasKey("GameStarted"))
        {
            PlayerPrefs.SetInt("ak47_Ammo", 70);
            //PlayerPrefs.SetInt("shotgun_Ammo", 45);
            //PlayerPrefs.SetInt("sniper_Ammo", 30);
            //PlayerPrefs.SetInt("magnum_Ammo", 28);

            PlayerPrefs.SetInt("GameStarted", 1);
        }
        
    }


}
