using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CropData", menuName = "New Crop")]

public class CropData : ScriptableObject, InventoryItem
{
    public bool stackable
    {
        get
        {
            return true;
        }
    }
    public string itemName
    {
        get
        {
            return this.name;
        }
    }

    public int growthTime;
    public Sprite[] progressSprites;
    public Sprite matureSprite;
    public Sprite icon;

    public int buyPrice;
    public int sellPrice;

    public string type;
    public string description;
    public string[] crossbreed;
    
    
}
