using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioClip[] audioClip;

    public Slider bgmSlider;
    public Slider sfxSlider;

    AudioSource bgm;    //배경음
    AudioSource sfx;    //효과음

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        bgm = GameObject.Find("BGM Player").GetComponent<AudioSource>();
        sfx = GameObject.Find("Sfx Player").GetComponent<AudioSource>();
        bgmSlider = bgmSlider.GetComponent<Slider>();
        sfxSlider = sfxSlider.GetComponent<Slider>();

        bgmSlider.onValueChanged.AddListener(BgmValueChange);
        sfxSlider.onValueChanged.AddListener(SfxValueChange);
    }

    void BgmValueChange(float value)
    {
        bgm.volume = value;
    }
    
    void SfxValueChange(float value)
    {
        sfx.volume = value;
    }

    public void PlayerSound(string type)
    {
        int index = 0;

        switch (type)
        {
            case "Touch":    index = 0; break;
            case "Grow":     index = 1; break;
            case "Sell":     index = 2; break;
            case "Buy":      index = 3; break;
            case "Unlock":   index = 4; break;
            case "Fail":     index = 5; break;
            case "Button":   index = 6; break;
            case "Pause In": index = 7; break;
            case "Pause Out":index = 8; break;
            case "Clear":    index = 9; break;
        }

        sfx.clip = audioClip[index];
        sfx.Play();
    }
}
