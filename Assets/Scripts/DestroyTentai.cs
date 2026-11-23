using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class DestroyTentai : MonoBehaviour
{
    public GameObject gingaP;
    public GameObject ginga1;
    public GameObject ginga2;

    public bool swich;

    public Slider timeBar;

    public AudioSource player;

    string filePath;
    FileStream file;

    string url;
    public InputField urlInput;
    
    void Start()
    {
        filePath = Application.dataPath + "/urltext.txt";
        if (!File.Exists(filePath))
        {
            file = File.Create(filePath);
            file.Close();
        }

        LoadUrl();
        StartIntantiate();
    }
    
    void Update()
    {
        if (swich)
        {
            if (player.clip != null)
            {
                if (timeBar.value > 0.99f)
                {
                    if (!player.isPlaying)
                    {
                        LoadUrl();
                        StartIntantiate();
                    }
                }
            }
        }
    }

    public void LoadUrl()
    {
        url = File.ReadAllText(filePath);
        urlInput.text = url;
    }

    public void StartIntantiate()
    {
        ginga1 = Instantiate(gingaP);
        ginga2 = Instantiate(gingaP);
        ginga2.GetComponent<Spectrum>().rotnum = 1;
        
        player.Play();
    }
}
