using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI roundText;
    public string menuSceneName;
    public SceneFader sceneFader;

    private void Start()
    {
        Time.timeScale = 0f;
    }
    private void OnEnable()
    {
        roundText.text = ("Wave : " + PlayerStats.rounds.ToString());
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
        SoundManager.Instance.PlaySound("btn_Click");
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        sceneFader.FadeTo(menuSceneName);
        SoundManager.Instance.PlaySound("btn_Click");
    }
}
