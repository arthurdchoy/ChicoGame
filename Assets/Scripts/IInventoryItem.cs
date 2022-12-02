using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryItem 
{
    int MaxStackSize { get; }
    string ItemName { get; }
    string InternalName { get; }
}
