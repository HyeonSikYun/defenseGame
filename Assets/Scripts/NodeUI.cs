using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    public GameObject ui;
    public TextMeshProUGUI sellAmount;
    public TextMeshProUGUI upgradeCost;
    public Button upgradeBtn;

    private Node target;

    public void SetTarget(Node _target)
    {
        target = _target;

        transform.position = target.GetBuildPosition();

        if(!target.isUpgraded)
        {
            upgradeCost.text = "$" + target.itemBlueprint.upgradeCost;
            upgradeBtn.interactable = true;
        }
        else
        {
            upgradeCost.text = "¿Ï·á";
            upgradeBtn.interactable = false;
        }

        sellAmount.text = "$" + target.itemBlueprint.GetSellAmount();

        ui.SetActive(true);
    }

    public void HideUI()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeItem();
        BuildManager.instance.DeselectNode();
    }

    public void Sell()
    {
        target.SellItem();
        BuildManager.instance.DeselectNode();
    }
}
