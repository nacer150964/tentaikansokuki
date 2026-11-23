using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Setting : MonoBehaviour
{
    DestroyTentai destroy;

    public Button isntLoop;
    public Button isLoop;
    int loopIndex;
    
    public Button play;
    public Button stop;

    public Slider timeBar;

    public AudioSource player;

    void Start()
    {
        destroy = GameObject.Find("Destroy").GetComponent<DestroyTentai>();

        Application.targetFrameRate = 30;

        loopIndex = PlayerPrefs.GetInt("user-isLoop-index");
        
        if (loopIndex == 0)
        {
            destroy.swich = false;
            isntLoop.gameObject.SetActive(true);
            isLoop.gameObject.SetActive(false);
        }

        if (loopIndex == 1)
        {
            destroy.swich = true;
            isntLoop.gameObject.SetActive(false);
            isLoop.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if (player.isPlaying)
        {
            play.gameObject.SetActive(false);
            stop.gameObject.SetActive(true);

            timeBar.value = (float)player.time / (float)player.clip.length;
        }

        else
        {
            play.gameObject.SetActive(true);
            stop.gameObject.SetActive(false);
        }

        loopIndex = PlayerPrefs.GetInt("user-isLoop-index");
        
        if (loopIndex == 0)
        {
            destroy.swich = false;
        }

        if (loopIndex == 1)
        {
            destroy.swich = true;
        }
    }

    public void Reset()
    {
        if (!destroy.swich)
        {
            if (player.clip != null)
            {
                if (timeBar.value > 0.99f)
                {
                    if (player.isPlaying)
                    {
                        destroy.LoadUrl();
                        destroy.StartIntantiate();
                    }
                }
            }
        }
    }

    public void IsntLoop()
    {
        PlayerPrefs.SetInt("user-isLoop-index", 0);
        destroy.swich = false;
    }

    public void IsLoop()
    {
        PlayerPrefs.SetInt("user-isLoop-index", 1);
        destroy.swich = true;
    }
}
