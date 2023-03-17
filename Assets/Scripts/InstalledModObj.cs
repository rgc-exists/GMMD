using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class InstalledModObj : MonoBehaviour
{
    public string name = "Loading...";
    public string description = "Loading description...";
    public string authors = "Loading authors...";

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI authorsText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        nameText.text = name;
        descriptionText.text = description;
        authorsText.text = authors;
    }
}
