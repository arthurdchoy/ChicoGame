using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ToolData", menuName = "New Tool")]

public class ToolData : ScriptableObject, InventoryItem
{
    // Start is called before the first frame update
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

    public Sprite icon;
    public int state;

    public string description;
}
