using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public ModInfoUI modInfoUI;
    public InstalledModsMenu installedModsUI;
    public LevelsList installedModsList;
    public AreYouSureDeletion  deletionConfirm;
    // Start is called before the first frame update
    void Start()
    {
        installedModsUI = FindObjectOfType<InstalledModsMenu>();
        modInfoUI = FindObjectOfType<ModInfoUI>();
        deletionConfirm = FindObjectOfType<AreYouSureDeletion>();
        installedModsList = FindObjectOfType<LevelsList>();
        deletionConfirm.gameObject.SetActive(false);
        installedModsUI.gameObject.SetActive(true);
        modInfoUI.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
