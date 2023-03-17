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
    public string path = "";

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI authorsText;

    public Texture2D icon;

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
    }

    public void SelectMod(){
        ModInfoUI infoMenu = FindObjectOfType<MenuManager>().modInfoUI;
        infoMenu.gameObject.SetActive(true);
        infoMenu.title = title;
        infoMenu.description = description;
        infoMenu.authors = authors;
        infoMenu.path = path;
        FindObjectOfType<MenuManager>().installedModsUI.gameObject.SetActive(false);
    }
}
