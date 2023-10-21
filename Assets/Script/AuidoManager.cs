using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuidoManager : MonoBehaviour
{
    private static AuidoManager instance;
   /* private AuidoManager() { }*/
    public static AuidoManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<AuidoManager>();
                if (instance == null)
                {
                    instance = new GameObject("GameManager").AddComponent<AuidoManager>();
                }
            }
            return instance;
        }
    }
    

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(this);
    }
    private AudioSource SfxSource;
    private AudioSource mainSource;

    public AudioClip playerMove;
    public AudioClip buttonClick;

    public AudioClip snowClip;
    private void Start()
    {
        SfxSource = transform.Find("SfxSource").gameObject.GetComponent<AudioSource>();
        mainSource = transform.Find("MainSource").gameObject.GetComponent<AudioSource>();

        PlaySnow();
    }
    public void PlaySnow()
    {
        mainSource.clip = snowClip;
        mainSource.loop = true;
        mainSource.Play();
    }
    public void PlayPlayerMove()
    {

        SfxSource.clip = playerMove;
        SfxSource.loop = false;
        SfxSource.Play();
    }
   public void PlayButtonClick()
    {
        SfxSource.clip = buttonClick;
        SfxSource.loop = false;
        SfxSource.Play();
    }
}
