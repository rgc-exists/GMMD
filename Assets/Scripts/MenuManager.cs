using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public ModInfoUI modInfoUI;
    public InstalledModsMenu installedModsUI;
    public GameObject installedModsUIObj;
    public LevelsList installedModsList;
    public GameObject deletionConfirm;
    public BlacklistManager blacklistManager;
    public string currentInstallLocation = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Will You Snail";
    public GameObject deletionConfirmObj;

    // Start is called before the first frame update
    void Awake()
    {
        installedModsUI = FindObjectOfType<InstalledModsMenu>();
        installedModsList = FindObjectOfType<LevelsList>();
        blacklistManager = FindObjectOfType<BlacklistManager>();
        deletionConfirmObj.SetActive(false);
        installedModsUI.gameObject.SetActive(true);
        installedModsList.menuManager = this;
        installedModsList.Reload_Mods();
        modInfoUI.gameObject.SetActive(false);
        blacklistManager.path = currentInstallLocation;
        blacklistManager.RecieveBlacklist();
        blacklistManager.UpdateBlacklist();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToDownloadedMods(){
        FindObjectOfType<MenuManager>().deletionConfirm.gameObject.SetActive(false);
        FindObjectOfType<MenuManager>().installedModsUI.gameObject.SetActive(true);
        gameObject.SetActive(false);
        FindObjectOfType<MenuManager>().installedModsList.Reload_Mods();
    }
}
