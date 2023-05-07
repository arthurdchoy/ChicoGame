using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ToolData", menuName = "New Tool")]

public class ToolData : ScriptableObject, InventoryItem
{
    // Start is called before the first frame update
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

    private int maxStackSize = 1;
    public string itemName;

    public Sprite icon;
    public int state;

    public string description;
}
