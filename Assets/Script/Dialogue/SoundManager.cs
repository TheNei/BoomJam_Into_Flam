using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public Dictionary<string, AudioClip> audioDic = new Dictionary<string, AudioClip>();
    public AudioClip[] audioClip;
    public AudioSource audio;
    public AudioSource audio2;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        audioDic["宫殿"] = audioClip[0];
        audioDic["非战斗"] = audioClip[1];
        audioDic["战斗"] = audioClip[2];
        audioDic["非战斗2"] = audioClip[3];
        audioDic["变身"] = audioClip[4];
        audioDic["暴风雪"] = audioClip[5];
        audioDic["小河"] = audioClip[6];
        audioDic["踩雪"] = audioClip[7];
        audioDic["溅血"] = audioClip[8];
    }

    public void SetAudioClip(string name)
    {
        audio.clip = audioDic[name];
        audio.Play();
    }
    public void SetAudioClip2(string name)
    {
        audio2.clip = audioDic[name];
        audio2.Play();
    }
    public void StopAudio()
    {
        audio.Stop();
    }
    public void StopAudio2()
    {
        audio2.Stop();
    }
    void SetSnowAudio()
    {

    }
}
