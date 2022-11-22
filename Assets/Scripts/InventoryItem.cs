using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InventoryItem 
{
    bool stackable { get; }
    string name { get; }
}
