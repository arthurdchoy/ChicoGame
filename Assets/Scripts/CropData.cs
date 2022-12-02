using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CropData", menuName = "New Crop")]

public class CropData : ScriptableObject, IInventoryItem
{
    public int MaxStackSize
    {
        get
        {
            return maxStackSize;
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

    //public int growthTime;
    //public Sprite[] progressSprites;
    //public Sprite cropSprite;
    public Sprite icon;

    public int buyPrice;
    public int sellPrice;

    public string type;
    public string description;
    //public string[] crossbreed;
    
    
}
