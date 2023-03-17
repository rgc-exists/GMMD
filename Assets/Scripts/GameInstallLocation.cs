using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
public class GameInstallLocation : MonoBehaviour
{

    public string currentInstallLocation = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Will You Snail";
    public TMP_InputField inputField;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeInstallLocation(){
        if(Directory.Exists(currentInstallLocation)){
            currentInstallLocation = inputField.text;
        } else {
            inputField.text = "Invalid install location!";
        }
    }
}
