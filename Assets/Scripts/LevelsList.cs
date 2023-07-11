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
using UnityEngine.Networking;
public class LevelsList : MonoBehaviour
{

    public Transform canvas;

    public GameObject installedModPrefab;

    public float startY = 62;
    
    public float yOffset = 75;

    public float yOffsetGlobal = 25;

    public float scrollSpeed = 10f;
    public float scrollEndOffset = 100;

    public int modCount = 0;

    public float listCount = 0;

    public MenuManager menuManager;

    public Color altModPrefabColor = new Color(58, 48, 58);
    public Texture2D defaultIcon;
    public Texture2D icon;
    public GameObject bottomMod;
    public Vector3 position_prev = Vector3.zero;
    public Vector3 startPos = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        position_prev = transform.position;
        startPos = transform.position;
    }

    void Awake(){
        menuManager = FindObjectOfType<MenuManager>();       
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        bottomMod = FindObjectsOfType<InstalledModObj>()[FindObjectsOfType<InstalledModObj>().Length - 1].gameObject;
        transform.Translate(new Vector2(0, Input.mouseScrollDelta.y) * scrollSpeed * Time.deltaTime);
        if(bottomMod.transform.position.y >= scrollEndOffset){
            transform.position = position_prev;
        }
        if(transform.localPosition.y < -1){
            transform.localPosition = new Vector3(0, 0, 0);
        }
        position_prev = transform.position;
    }

    public void Reload_Mods(){
        
        transform.position = startPos;
        InstalledModObj[] existingModObjs = FindObjectsOfType<InstalledModObj>();
        if(existingModObjs.Length > 0){
            foreach(InstalledModObj m in existingModObjs){
                DestroyGameObjectAndChildren(m.gameObject);
            }
        }
        float y = startY;

        string[] localDirectories = Directory.GetDirectories(Path.Combine(menuManager.currentInstallLocation, "gmml/mods"));
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
                modInfo.title = metaData.name;
                modInfo.description = metaData.description;
                modInfo.path = dir;
                modInfo.id = metaData.id;
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
                if(File.Exists(dir + "/icon.png")){
                    GetImageFromPath("file://" + dir + "/icon.png");
                    modInfo.icon = icon;
                } else {
                    modInfo.icon = defaultIcon;
                }
                i++;
                bottomMod = installedMod;
            }    
            scrollEndOffset += i * 1;
        }
        listCount = localDirectoriesFiltered.Count;
    }

    public static MetaDataJson CreateFromMetadataJson(string json){
        return JsonUtility.FromJson<MetaDataJson>(json);
    }


    //https://docs.unity3d.com/ScriptReference/Networking.UnityWebRequestTexture.GetTexture.html
    //Because I am too lazy to write it myself. :D
    IEnumerator GetImageFromPath(string path)
    {
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(path))
        {
            yield return uwr.SendWebRequest();


            if (uwr.result != UnityWebRequest.Result.Success)
            {
                icon = defaultIcon;
            }
            else
            {
                // Get downloaded asset bundle
                icon = DownloadHandlerTexture.GetContent(uwr);
            }
        }
    }

    
    public void DestroyGameObjectAndChildren(GameObject obj){
        foreach(Transform c in obj.transform){
            DestroyGameObjectAndChildren(c.gameObject);
            Destroy(c.gameObject);
        }
        Destroy(obj);
    }

}

    public class MetaDataJson {
        public string id;
        public string name;
        public string version;
        public string[] authors;
        public string description;
        public Dictionary<string, string>[] dependencies {get; set;}
    }