using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopToggle : MonoBehaviour
{
    bool isOpen = true;

    public void Toggle()
    {
        if(isOpen)
        {
            transform.Translate(new Vector2(295, 0));
            isOpen = false;
        }
        else
        {
            transform.Translate(new Vector2(-295, 0));
            isOpen = true;
        }
    }
}
