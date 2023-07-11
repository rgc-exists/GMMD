using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class BlacklistManager : MonoBehaviour
{

    public List<string> blacklist = new List<string>();
    public string path = "";

    // Start is called before the first frame update
    void Awake()
    {
        path = Path.Combine(FindObjectOfType<MenuManager>().currentInstallLocation, "gmml/mods");
    }

    // Update is called once per frame
    void Update()
    {
        path = Path.Combine(FindObjectOfType<MenuManager>().currentInstallLocation, "gmml/mods");
    }

    public void RecieveBlacklist(){
        blacklist = new List<string>();
        if(File.Exists(path + "/blacklist.txt")){
            string[] blacklistContents = File.ReadAllLines(path + "/blacklist.txt");
            foreach(string b in blacklistContents){
                blacklist.Add(b.Replace("\n", ""));
            }
        } else {
            File.CreateText(path + "/blacklist.txt").Dispose();
        }
    }

    public void UpdateBlacklist(){
        
        if(!File.Exists(path + "/blacklist.txt")){
            File.CreateText(path + "/blacklist.txt").Dispose();

        }
        List<string> linesToWrite = new List<string>();
        foreach(string b in blacklist){
            linesToWrite.Add(b);
        }
        File.WriteAllLines(path + "/blacklist.txt", linesToWrite);

        InstalledModObj[] installedMods = FindObjectsOfType<InstalledModObj>();
        foreach(InstalledModObj m in installedMods){
            if(blacklist.Contains(m.id)){
                m.modEnabled = false;
            } else {
                m.modEnabled = true;
            }
        }
    }

    public void AddBlacklistGame(string id){
        if(!blacklist.Contains(id)){
            blacklist.Add(id);
        }
        UpdateBlacklist();
    }

    public void RemoveBlacklistGame(string id){
        if(blacklist.Contains(id)){
            blacklist.Remove(id);
        }
        UpdateBlacklist();
    }
}
