using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public ItemBlueprint plantsWarrior;
    public ItemBlueprint knight;
    public ItemBlueprint gunSoldier;
    public ItemBlueprint missileSoldier;
    public ItemBlueprint razerTurret;
    public ItemBlueprint wizzard;
    

    BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }
    public void SelectLazerTurret()
    {
        Debug.Log("레이저 터렛 구매");
        buildManager.SelectItemToBuild(razerTurret);
        SoundManager.Instance.PlaySound("btn_Click");
    }

    public void SelectKnight()
    {
        Debug.Log("기사 구매");
        buildManager.SelectItemToBuild(knight);
        SoundManager.Instance.PlaySound("btn_Click");
    }

    public void SelectPlantWarrior()
    {
        buildManager.SelectItemToBuild(plantsWarrior);
        SoundManager.Instance.PlaySound("btn_Click");
    }

    public void SelectGunSoldier()
    {
        buildManager.SelectItemToBuild(gunSoldier);
        SoundManager.Instance.PlaySound("btn_Click");
    }

    public void SelectMissileSoldier()
    {
        buildManager.SelectItemToBuild(missileSoldier);
        SoundManager.Instance.PlaySound("btn_Click");
    }

    public void SelectWizzard()
    {
        buildManager.SelectItemToBuild(wizzard);
        SoundManager.Instance.PlaySound("btn_Click");
    }
}
