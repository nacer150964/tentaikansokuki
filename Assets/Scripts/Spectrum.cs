using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Spectrum : MonoBehaviour
{
    float[] spectrum = new float[256];

    public GameObject hoshi;
    GameObject[] hoshitachi = new GameObject[76];

    int i = 0;
    float rotz = 0;

    public float dist;
    public float addist;

    bool swich;
    bool swich2 = true;

    float hoshiSize;
    int colornum;

    Color[] colors = new Color[] {new Color(1, 1, 1), new Color(0.75f, 1, 1), new Color(0.5f, 1, 1), new Color(1, 0.75f, 1)};

    AudioSource player;

    AudioClip clip;

    float xpos;
    float ypos;

    public int rotnum = 0;


    Transform cam;

    Transform gParent;

    

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<AudioSource>();
        cam = GameObject.Find("ui cam").transform;
        gParent = GameObject.Find("Galexy(parent)").transform;

        clip = player.clip;

        xpos = transform.position.x;
        ypos = transform.position.y;
    }

    void FixedUpdate()
    {
        transform.parent = gParent;

        if (7200 < rotz)
        {
            rotz -= 7200;
        }
        
        if (swich)
        {
            AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);
        }

        if (player.isPlaying)
        {
            if (swich2)
            {
                swich = true;

                StartCoroutine(GetSpectrum());

                swich2 = false;
            }
            rotz = rotz * 1.0001f;
        }

        else
        {
            swich = false;
            swich2 = true;
        }
    }

    public IEnumerator GetSpectrum()
    {
        for (i = 0; i < spectrum.Length - 180; i++)
        {
            if (player.isPlaying)
            {
                hoshitachi[i] = Instantiate(hoshi);
                hoshitachi[i].transform.parent = transform;
                hoshitachi[i].transform.position = new Vector3(xpos, ypos, 0);

                //if (rotnum == 2){hoshitachi[i].transform.GetChild(0).position = new Vector3(dist, 0, 0);}

                //else if (rotnum < 2){
                hoshitachi[i].transform.GetChild(0).position = new Vector3(0, dist, 0);

                hoshiSize = Random.Range(0.05f, 0.1f);
                hoshitachi[i].transform.GetChild(0).localScale = new Vector3(hoshiSize, hoshiSize, 0.0001f);

                ParticleSystem.MainModule particle = hoshitachi[i].transform.GetChild(0).GetComponent<ParticleSystem>().main;
                colornum = Random.Range(0, 4);
                particle.startColor = colors[colornum];

                StartCoroutine("Kaiden");

                //hoshitachi[i].transform.rotation = Quaternion.Euler(0, 0, rotz);

                if (spectrum.Length - 182 < i)
                {
                    dist += addist;
                    i = 0;
                }

                yield return new WaitForSeconds((float)clip.length / spectrum.Length * 0.02f);

                yield return null;
            }
        }
    }

    
    IEnumerator Kaiden()
    {
        while (player.isPlaying)
        {
            if (rotnum == 0)
            {
                transform.localRotation = Quaternion.Euler(rotz, 0, 0);
            }

            if (rotnum == 1)
            {
                transform.localRotation = Quaternion.Euler(0, 0, rotz);
            }

            //if (rotnum == 2){transform.localRotation = Quaternion.Euler(0, rotz, 0);}

            gParent.rotation = Quaternion.Euler(0, rotz, 0);
            cam.rotation = Quaternion.Euler(rotz * 0.05f, rotz * 0.05f, rotz * 0.05f);
            
            rotz += 0.006f;

            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
