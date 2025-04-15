using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad = "WayDefense";
    public SceneFader sceneFader;
    public GameObject settingPanel;

    public void Play()
    {
        sceneFader.FadeTo(levelToLoad);
        SoundManager.Instance.PlaySound("btn_Click");
    }

    public void Setting()
    {
        settingPanel.SetActive(true);
        SoundManager.Instance.PlaySound("btn_Click");
    }

    public void SettingOff()
    {
        settingPanel.SetActive(false);
        SoundManager.Instance.PlaySound("btn_Click");
    }

    public void Quit()
    {
        Application.Quit();
        SoundManager.Instance.PlaySound("btn_Click");
    }
}
