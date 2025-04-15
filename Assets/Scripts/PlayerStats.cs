using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int money;
    public static int Lives;
    public static int rounds;
    public int startMoney = 500;
    public int startLives = 20;
    
    private void Start()
    {
        money = startMoney;
        Lives = startLives;

        rounds = 0;
    }
}
