using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{
    public string easyStart;
    public string normalStart;
    public string hardStart;
    public SceneFader sceneFader;

    public void Easy()
    {
        sceneFader.FadeTo(easyStart);
        SoundManager.Instance.PlaySound("btn_Click");
    }

    public void Normal()
    {
        sceneFader.FadeTo(normalStart);
        SoundManager.Instance.PlaySound("btn_Click");
    }

    public void Hard()
    {
        sceneFader.FadeTo(hardStart);
        SoundManager.Instance.PlaySound("btn_Click");
    }
}
