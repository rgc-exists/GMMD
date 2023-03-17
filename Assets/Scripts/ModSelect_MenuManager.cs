using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModSelect_MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<GameInstallLocation>().ChangeInstallLocation();
        FindObjectOfType<LevelsList>().Reload_Mods();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
