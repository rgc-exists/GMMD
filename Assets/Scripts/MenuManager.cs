using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public ModInfoUI modInfoUI;
    public InstalledModsMenu installedModsUI;
    public LevelsList installedModsList;
    public AreYouSureDeletion  deletionConfirm;
    public BlacklistManager blacklistManager;
    public string currentInstallLocation = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Will You Snail";

    // Start is called before the first frame update
    void Start()
    {
        installedModsUI = FindObjectOfType<InstalledModsMenu>();
        modInfoUI = FindObjectOfType<ModInfoUI>();
        deletionConfirm = FindObjectOfType<AreYouSureDeletion>();
        installedModsList = FindObjectOfType<LevelsList>();
        blacklistManager = FindObjectOfType<BlacklistManager>();
        deletionConfirm.gameObject.SetActive(false);
        installedModsUI.gameObject.SetActive(true);
        installedModsList.Reload_Mods();
        modInfoUI.gameObject.SetActive(false);
        blacklistManager.RecieveBlacklist();
        blacklistManager.UpdateBlacklist();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
