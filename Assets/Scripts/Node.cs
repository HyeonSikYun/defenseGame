using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color noMoney;
    public Vector3 positionOffset;

    private Renderer rend;
    private Color startColor;


    [HideInInspector]
    public GameObject item;
    [HideInInspector]
    public ItemBlueprint itemBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

    BuildManager buildManager;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.instance;

        if (buildManager == null)
        {
            Debug.LogError("BuildManager is not assigned!");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    void BuildItem(ItemBlueprint blueprint)
    {
        if (PlayerStats.money < blueprint.cost)
        {
            return;
        }
        PlayerStats.money -= blueprint.cost;

        GameObject _item = Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        item = _item;
        SoundManager.Instance.PlaySound("unit_Set");

        itemBlueprint = blueprint;

        GameObject effect = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 3f);
        
    }

    public void UpgradeItem()
    {
        if (PlayerStats.money < itemBlueprint.upgradeCost)
        {
            return;
        }
        PlayerStats.money -= itemBlueprint.upgradeCost;

        Destroy(item);

        GameObject _item = Instantiate(itemBlueprint.upgradePrefab, GetBuildPosition(), Quaternion.identity);
        item = _item;

        GameObject effect = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 3f);

        isUpgraded = true;
        SoundManager.Instance.PlaySound("upgrade_Unit");
    }

    public void SellItem()
    {
        PlayerStats.money += itemBlueprint.GetSellAmount();

        Destroy(item);
        SoundManager.Instance.PlaySound("sell_Sound");
        itemBlueprint = null;
    }

    private void OnMouseDown()
    {
        if (!buildManager.CanBuild)
            return;

        if(item != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        BuildItem(buildManager.GetItemToBuild());
    }

    private void OnMouseEnter()
    {
        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = noMoney;
        }

        if (!buildManager.CanBuild)
            return;
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
