using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
public class GameInstallLocation : MonoBehaviour
{

    public string currentInstallLocation = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Will You Snail";
    public TMP_InputField inputField;

    void Awake(){
        FindObjectOfType<MenuManager>().currentInstallLocation = currentInstallLocation;
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        FindObjectOfType<MenuManager>().currentInstallLocation = currentInstallLocation;
    }

    public void ChangeInstallLocation(){
        if(Directory.Exists(currentInstallLocation)){
            currentInstallLocation = inputField.text;
        } else {
            inputField.text = "Invalid install location!";
        }
    }
}
