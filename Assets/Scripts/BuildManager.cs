using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }
    public GameObject plantsWarrior;
    public GameObject knightPrefab;
    public GameObject gunSoldier;
    public GameObject missileSoldier;
    public GameObject razerTurretPrefab;
    public GameObject wizzardPrefab;
    public GameObject buildEffect;
    public NodeUI nodeUI;

    public bool CanBuild { get { return itemToBuild != null; } }
    public bool HasMoney { get { return itemToBuild != null && PlayerStats.money >= itemToBuild.cost; } }

    private ItemBlueprint itemToBuild;
    private Node selectedNode;

    public void SelectItemToBuild(ItemBlueprint item)
    {
        itemToBuild = item;
        DeselectNode();
    }

    public ItemBlueprint GetItemToBuild()
    {
        return itemToBuild;
    }

    public void SelectNode(Node node)
    {
        if(selectedNode==node)
        {
            DeselectNode();
            return;
        }

        selectedNode = node;
        itemToBuild = null;

        nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.HideUI();
    }
}
