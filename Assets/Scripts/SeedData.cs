using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SeedData", menuName = "New Seed")]

public class SeedData : ScriptableObject, IInventoryItem
{
    public int MaxStackSize
    {
        get
        {
            return MaxStackSize;
        }
    }
    public string ItemName
    {
        get
        {
            return itemName;
        }
    }
    public string InternalName
    {
        get
        {
            return this.name;
        }
    }

    public int maxStackSize;
    public string itemName;

    public Sprite icon;

    public int buyPrice;
    public int sellPrice;

    public string type;
    public string description;

    public int[] growthStageTimes;
    public Sprite[] growthStageSprites;
}
