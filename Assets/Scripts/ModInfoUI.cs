using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;
public class ModInfoUI : MonoBehaviour
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
    public Button deleteButton;



    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        nameText.text = title;
        descriptionText.text = description;
        authorsText.text = authors;
    }

    public void StartDelete(){
        FindObjectOfType<MenuManager>().deletionConfirmObj.SetActive(true);
    }
    public void CancelDelete(){
        FindObjectOfType<MenuManager>().deletionConfirmObj.SetActive(false);
    }
    public void ConfirmDelete(){
        DeleteAllFilesAndSubdirsInDirectory(path);
        Directory.Delete(path);
        Back();
    }

    public void Back(){
        FindObjectOfType<MenuManager>().deletionConfirmObj.SetActive(false);
        FindObjectOfType<MenuManager>().installedModsUIObj.SetActive(true);
        gameObject.SetActive(false);
        FindObjectOfType<MenuManager>().installedModsList.Reload_Mods();
    }

    public void DeleteAllFilesAndSubdirsInDirectory(string dir){
        string[] files = Directory.GetFiles(dir);
        foreach(string f in files){
            File.Delete(f);
        }
        string[] directories = Directory.GetDirectories(dir);
        foreach(string d in directories){
            DeleteAllFilesAndSubdirsInDirectory(d);
            Directory.Delete(d);
        }
    }



}
