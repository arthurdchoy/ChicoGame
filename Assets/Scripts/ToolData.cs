using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ToolData", menuName = "Item/New Tool")]

public class ToolData : InventoryItem
{
    private void Awake()
    {
        maxStackSize = 1;
        itemType = ItemType.TOOL;
    }
    public int state;
    public ToolType toolType = ToolType.INVALID;
}
public enum ToolType
{
    INVALID,
    AXE,
    HAMMER,
    HANDS,
    HOE,
    SHOVEL,
    WATERINGCAN
}
