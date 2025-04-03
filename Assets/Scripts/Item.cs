using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string itemName;
    public Sprite itemIcon;

    public Item(string name, Sprite icon)
    {
        itemName = name;
        itemIcon = icon;
    }
}
