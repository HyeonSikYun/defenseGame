using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool isGameOver;

    public GameObject gameoverUI;

    private void Start()
    {
        isGameOver = false;
    }
    void Update()
    {
        if (isGameOver)
            return;

        if (Input.GetKeyDown(KeyCode.E))
            EndGame();

        if(PlayerStats.Lives<=0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        isGameOver = true;
        gameoverUI.SetActive(true);

    }
}
