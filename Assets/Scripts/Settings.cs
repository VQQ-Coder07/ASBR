using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using TMPro;
public class Settings : MonoBehaviour
{
    public TMP_Dropdown region;
    public Slider sfx_slider;
    public Slider music_slider;
    public Switch sfx_switch;
    public Switch music_switch;
    public Switch chatcensor_switch;
    public string[] regionids;
    public void UpdateRegion()
    {
        PlayerPrefs.SetString("Region", regionids[region.value]);
        PlayerPrefs.SetInt("Region", region.value);
        //PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = PlayerPrefs.GetString("Region");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private int GetRegionInt(string region)
    {
        int value = 0;
        for(int i=0; i < regionids.Length; i++)
        {
            if(region == regionids[i])
            {
                value = i;
                break;
            }
        }
        return value;
    }
    public void Awake()
    {
        region.value = PlayerPrefs.GetInt("Region");
        PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = PlayerPrefs.GetString("Region");

        if(PlayerPrefs.GetFloat("chatcensoring") == 0)
        {
            PlayerPrefs.SetFloat("chatcensoring", 2);
        }
        chatcensor_switch.value = ToBoolean(PlayerPrefs.GetFloat("chatcensoring"));
        if(PlayerPrefs.GetFloat("sfx_switch") == 0)
        {
            PlayerPrefs.SetFloat("sfx_switch", 2);
        }
        sfx_switch.value = ToBoolean(PlayerPrefs.GetFloat("sfx_switch"));
        if(PlayerPrefs.GetFloat("music_switch") == 0)
        {
            PlayerPrefs.SetFloat("music_switch", 2);
        }
        music_switch.value = ToBoolean(PlayerPrefs.GetFloat("music_switch"));
        if(PlayerPrefs.GetFloat("sfx_slider_value") == 0)
        {
            PlayerPrefs.SetFloat("sfx_slider_value", 100);
        }
        sfx_slider.value = PlayerPrefs.GetFloat("sfx_slider_value");
        if(PlayerPrefs.GetFloat("music_slider_value") == 0)
        {
            PlayerPrefs.SetFloat("music_slider_value", 100);
        }
        music_slider.value = PlayerPrefs.GetFloat("music_slider_value");
        MusicBoolChanged();
        SfxBoolChanged();
        CensorBoolChanged();
    }
    public bool ToBoolean(float i)
    {
        bool val = false;
        if(i > 1)
        {
            val = true;
        }
        return val;
    }
    public void SfxChanged()
    {
        if(!sfxblocked)
        PlayerPrefs.SetFloat("sfx_slider_value", sfx_slider.value);
    }
    public void SfxBoolChanged()
    {
        PlayerPrefs.SetFloat("sfx_switch", ToFloat(sfx_switch.value));
        if(!sfx_switch.value)
        {
            sfxblocked = true;
            sfx_slider.value = 0;
            sfx_slider.interactable = false;
        }
        else
        {
            sfxblocked = false;
            sfx_slider.value = PlayerPrefs.GetFloat("sfx_slider_value");
            sfx_slider.interactable = true;
        }
    }
    public float ToFloat(bool boolean)
    {
        float value;
        if(!boolean)
        {
            value = 1;
        }
        else
        {
            value = 2;
        }
        return value;
    }
    public void MusicChanged()
    {
        if(!musicblocked)
        PlayerPrefs.SetFloat("music_slider_value", music_slider.value);
    }
    bool sfxblocked;
    bool musicblocked;
    public void MusicBoolChanged()
    {
        PlayerPrefs.SetFloat("music_switch", ToFloat(music_switch.value));
        if(!music_switch.value)
        {
            musicblocked = true;
            music_slider.value = 0;
            music_slider.interactable = false;
        }
        else
        {
            musicblocked = false;
            music_slider.value = PlayerPrefs.GetFloat("music_slider_value");
            music_slider.interactable = true;
        }
    }
    public void CensorBoolChanged()
    {
        PlayerPrefs.SetFloat("chatcensoring", ToFloat(chatcensor_switch.value));
    }
}
