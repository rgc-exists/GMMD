using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class InstalledModObj : MonoBehaviour
{

    public string title = "Loading...";
    public string description = "Loading description...";
    public string authors = "Loading authors...";
    public string id = "";
    public string path = "";

    public bool modEnabled = true;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI authorsText;

    public Texture2D icon;
    public Color enabledButtonColor;
    public Color disabledButtonColor;
    public Texture2D enabledSpr;
    public Texture2D disabledSpr;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        nameText.text = title;
        //descriptionText.text = description;
        authorsText.text = authors;
        GetComponentInChildren<ModIcon>().gameObject.GetComponent<RawImage>().texture = icon;
        GameObject activeButton = GetComponentInChildren<ModActiveButton>().gameObject;
        if(modEnabled){
            activeButton.GetComponent<RawImage>().texture = enabledSpr;
            activeButton.GetComponent<RawImage>().color = enabledButtonColor;
        } else {
            activeButton.GetComponent<RawImage>().texture = disabledSpr;
            activeButton.GetComponent<RawImage>().color = disabledButtonColor;
        }

    }

    public void SelectMod(){
        ModInfoUI infoMenu = FindObjectOfType<MenuManager>().modInfoUI;
        infoMenu.gameObject.SetActive(true);
        infoMenu.title = title;
        infoMenu.description = description;
        infoMenu.authors = authors;
        infoMenu.id = id;
        infoMenu.path = path;
        infoMenu.modEnabled = modEnabled;
        FindObjectOfType<MenuManager>().installedModsUI.gameObject.SetActive(false);
    }
    public void ToggleMod(){
        modEnabled = !modEnabled;
        if(!modEnabled){
            FindObjectOfType<MenuManager>().blacklistManager.AddBlacklistGame(id);
        } else{
            FindObjectOfType<MenuManager>().blacklistManager.RemoveBlacklistGame(id);
        }
        FindObjectOfType<MenuManager>().blacklistManager.UpdateBlacklist();
    }
}
