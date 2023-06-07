using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    TOOL,
    QUEST,
    CROP,
    SEED
}
public abstract class InventoryItem : ScriptableObject
{
    public int maxStackSize;
    public string itemName;
    [TextArea(20,15)]
    public string description;
    public ItemType itemType;
    public Sprite icon;
}
