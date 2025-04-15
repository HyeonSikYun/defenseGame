using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject ui;
    public SceneFader sceneFader;
    public string menuSceneName;
    public GameObject settingPanel;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Toggle();
            
        }
    }

    public void Toggle()
    {
        ui.SetActive(!ui.activeSelf);

        SoundManager.Instance.PlaySound("btn_Click");
        if(ui.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void  Retry()
    {
        Toggle();
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
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

    public void MainMenu()
    {
        Toggle();
        sceneFader.FadeTo(menuSceneName);
    }
}
