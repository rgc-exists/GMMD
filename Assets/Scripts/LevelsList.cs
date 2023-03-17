//This code was partially stolen from my own existing game...
//Actually it was a MOD, for Dani's Karlson demo. It added a level editor. :P
//Anyways in that game I needed UI for selecting installed levels.
//Also this script is heavily modidifed so it wasn't exactly just CTRL+C CTRL+V lmao

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelsList : MonoBehaviour
{

    public Transform canvas;

    public GameObject installedModPrefab;

    public float startY = 62;
    
    public float yOffset = 75;

    public float yOffsetGlobal = 25;

    public float scrollSpeed = 10f;

    public int modCount = 0;

    public float listCount = 0;

    public GameInstallLocation installLocManager;

    public Color altModPrefabColor = new Color(58, 48, 58);

    // Start is called before the first frame update
    void Start()
    {
        installLocManager = FindObjectOfType<GameInstallLocation>();       

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(0, Input.mouseScrollDelta.y) * scrollSpeed * Time.deltaTime);
        if(transform.localPosition.y > (listCount * yOffsetGlobal) + 110){
            transform.localPosition = new Vector3(0, (listCount * yOffsetGlobal) + 111, 0);
        }
        if(transform.localPosition.y < -1){
            transform.localPosition = new Vector3(0, 0, 0);
        }
    }

    public void Reload_Mods(){
        float y = startY;

        string[] localDirectories = Directory.GetDirectories(Path.Combine(installLocManager.currentInstallLocation, "gmml/mods"));
        List<string> localDirectoriesFiltered = new List<string>();
        foreach(string d in localDirectories){
            Debug.Log("Found: " + d);
            if(File.Exists(d + "/metadata.json")){
                Debug.Log("Found metadata.json in: " + d);
                localDirectoriesFiltered.Add(d);
            }
        }

        if(localDirectoriesFiltered.Count > 0){
            int i = 0;
            foreach(string dir in localDirectoriesFiltered){
                GameObject installedMod = Instantiate(installedModPrefab, new Vector3(0, y, 0), Quaternion.identity, transform);
                InstalledModObj modInfo = installedMod.GetComponent<InstalledModObj>();
                string metadataJson = File.ReadAllText(dir + "/metadata.json");
                MetaDataJson metaData = CreateFromMetadataJson(metadataJson);
                modInfo.name = metaData.name;
                modInfo.description = metaData.description;
                string authors = "by ";
                foreach(string a in metaData.authors){
                    if(authors == "by "){
                        authors += a;
                    } else {
                        authors += ", " + a;
                    }
                }
                modInfo.authors = authors;
                if(i % 2 != 0){
                    installedMod.GetComponent<RawImage>().color = altModPrefabColor;
                }
                y -= yOffset;
                i++;
            }    
        }
        listCount = localDirectoriesFiltered.Count;
    }

    public static MetaDataJson CreateFromMetadataJson(string json){
        return JsonUtility.FromJson<MetaDataJson>(json);
    }

}

    public class MetaDataJson {
        public string id {get; set;}
        public string name {get; set;}
        public string version {get; set;}
        public string[] authors {get; set;}
        public string description {get; set;}
        public Dictionary<string, string>[] dependencies {get; set;}
    }